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

using System;
using AssemblyData;

namespace de4dot.code.AssemblyClient {
	// Starts the server in the current app domain.
	public class SameAppDomainAssemblyServerLoader : IAssemblyServerLoader {
		IAssemblyService service;
		AssemblyServiceType serviceType;

		public SameAppDomainAssemblyServerLoader(AssemblyServiceType serviceType) {
			this.serviceType = serviceType;
		}

		public void LoadServer() {
			if (service != null)
				throw new ApplicationException("Server already loaded");
			service = AssemblyService.Create(serviceType);
		}

		public IAssemblyService CreateService() {
			return service;
		}

		public void Dispose() {
		}
	}
}
