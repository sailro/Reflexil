using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Reflexil.Plugins
{
	public class BaseAssemblyContext : IAssemblyContext
	{
		protected TCecilDef TryGetOrAdd<TCecilDef, TExternalDef>(Dictionary<TExternalDef, TCecilDef> cache, TExternalDef item, Func<TExternalDef, TCecilDef> finder) where TCecilDef : class
		{
			TCecilDef result;

			if (cache.TryGetValue(item, out result))
				return result;

			result = finder(item);
			if (result == null)
				return null;

			cache.Add(item, result);
			return result;
		}

		public virtual AssemblyDefinition AssemblyDefinition
		{
			get;
			set;
		}
	}
}
