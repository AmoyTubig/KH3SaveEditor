﻿using KHSave.SaveEditor.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KHSave.SaveEditor.Services
{
    public static class Pcsx2MemoryService
    {
        private const int Pcsx2EmulationBaseAddress = 0x20000000;
        private const int Pcsx2EmulationMemoryLength = 0x2000000;
        private const int PlayStation2BootFile = 0x155D0;
        private const int BootFileMaximumStringLength = 0x20;

        private class GameEntry
        {
            public string BootFile { get; }
            public long Offset { get; }
            public int Length { get; }

            public GameEntry(string titleId, long offset, int length)
            {
                BootFile = titleId;
                Offset = offset;
                Length = length;
            }
        }

        private static GameEntry[] Games => new GameEntry[]
        {
            new GameEntry("SLUS_203.70;1", 0x203F1C90, 0x16c00), // Kingdom Hearts I (US)
            new GameEntry("SLES_542.34;1", 0x2033ED60, 0xb4e0), // Kingdom Hearts II (IT)
            new GameEntry("SLPM_666.75;1", 0x2032BB30, 0x10fc0), // Kingdom Hearts II: Final Mix (Crazycat00 eng patch)
        };

        public static async Task<ProcessStream> CreateStreamFromPcsx2Process(Process process, CancellationToken cancellationToken)
        {
            var data = new byte[BootFileMaximumStringLength];
            using (var searchStream = new ProcessStream(process, Pcsx2EmulationBaseAddress + PlayStation2BootFile, 0x20))
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var byteReadCount = searchStream.Read(data, 0, data.Length);

                    var bootFile = Encoding.ASCII.GetString(data.TakeWhile(b => !b.Equals(0)).ToArray());

                    // Here the situation becomes weird. We can have 5 possible different scenario:
                    // 1. The emulator is not booted. so the selected portion of memory will be undefined.
                    //    The task can wait for the user to load the game.
                    //    We can impose a quite long sleep, since it takes time from user's interaction.
                    if (byteReadCount == 0)
                    {
                        await Task.Delay(1000);
                        continue;
                    }

                    // 2. The emulator is booted to the bios, which will return "SYS"
                    //    The task can wait for the BIOS to boot the game.
                    //    The boot can happen at any time... or not happen at all.
                    if (bootFile == "SYS")
                    {
                        await Task.Delay(200);
                        continue;
                    }

                    // 3. The game is booting, so the length of bootFile will be 0.
                    //    The task can wait for the game to be booted.
                    //    This operation is usually quite fast.
                    if (bootFile.Length == 0)
                    {
                        await Task.Delay(10);
                        continue;
                    }

                    // 4. A non-supported game is booted. It will always start with "SL".
                    //    The task should return null, as the game is not supported.
                    var game = Games.FirstOrDefault(x => x.BootFile == bootFile);
                    if (game == null)
                        return null;

                    // 5. A supported game is booted.
                    //    Returns a valid Stream.
                    return new ProcessStream(process, game.Offset, (uint)game.Length);
                }

                return null;
            }
        }
    }
}
