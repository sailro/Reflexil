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

using dnlib.DotNet;

namespace de4dot.code.renamer.asmmodules {
	public abstract class Ref {
		public readonly IMemberRef memberRef;
		public int Index { get; set; }
		public MTypeDef Owner { get; set; }

		protected Ref(IMemberRef memberRef, MTypeDef owner, int index) {
			this.memberRef = memberRef;
			Owner = owner;
			Index = index;
		}

		public override string ToString() {
			return memberRef != null ? memberRef.ToString() : null;
		}
	}
}
