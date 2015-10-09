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
using dnlib.DotNet;

namespace de4dot.code.renamer.asmmodules {
	public class MGenericParamDef : Ref
    {
		public GenericParam GenericParam {
			get { return (GenericParam)memberRef; }
		}

		public MGenericParamDef(GenericParam genericParameter, int index)
			: base(genericParameter, null, index) {
		}

		public static List<MGenericParamDef> CreateGenericParamDefList(IEnumerable<GenericParam> parameters) {
			var list = new List<MGenericParamDef>();
			if (parameters == null)
				return list;
			int i = 0;
			foreach (var param in parameters)
				list.Add(new MGenericParamDef(param, i++));
			return list;
		}
	}
}
