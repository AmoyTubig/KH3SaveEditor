﻿/*
    Kingdom Hearts 0.2 and 3 Save Editor
    Copyright (C) 2019  Luciano Ciccariello

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using KHSave.Attributes;

namespace KHSave.Types
{
	public enum WorldType : byte
	{
		[WorldInfo("bt", "Scala Ad Caelum")]
		ScalaAdCaelum = 1,

		[WorldInfo("dw", "Dark World")]
		DarkWorld = 3,

		[WorldInfo("he", "Olympus")]
		Olympus = 4,

		[WorldInfo("ts", "Toy Box")]
		ToyBox = 5,

		[WorldInfo("ra", "Kindom of Corona")]
		KingdomOfCorona = 7,

		[WorldInfo("fz", "Arendelle")]
		Arendelle = 8,

		[WorldInfo("ca", "Caribbean")]
		Caribbean = 9,

		[WorldInfo("po", "100 Acre Wood")]
		AcreWood = 10,

		[WorldInfo("mi", "Monstropolis")]
		Monstropolis = 11,

		[WorldInfo("tt", "Twilight Town")]
		TwilightTown = 12,

		[WorldInfo("??", "The Mysterious Tower")]
		MysteriousTower = 13,

		[WorldInfo("kg", "Keyblade Graveyard")]
		KeybladeGraveyard = 14,

		[WorldInfo("bx", "San Fransokyo")]
		SanFransokyo = 19,

		[WorldInfo("dw", "The Final World")]
		FinalWorld = 22,

		[WorldInfo("dp", "Land of Departure")]
		LandOfDeparture = 25
	}
}