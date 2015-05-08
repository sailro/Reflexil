﻿/*
    Copyright (C) 2011-2014 de4dot@gmail.com

    This file is part of de4dot.

    de4dot is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    de4dot is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with de4dot.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;

namespace de4dot.blocks.cflow {
	static class CflowUtils {
		public static Block GetSwitchTarget(IList<Block> targets, Block fallThrough, Int32Value intValue) {
			if (!intValue.AllBitsValid())
				return null;

			int index = intValue.Value;
			if (targets == null || index < 0 || index >= targets.Count)
				return fallThrough;
			else
				return targets[index];
		}
	}
}
