/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using de4dot.code;
using de4dot.code.AssemblyClient;
using de4dot.code.deobfuscators;
using dnlib.DotNet;

#endregion

namespace Reflexil.Utils
{
	/// <summary>
	/// Deobfuscator stuff.
	/// </summary>
	public static class De4DotHelper
	{
		#region Fields

		private static IList<IDeobfuscatorInfo> _deobfuscatorinfos;
		private static IList<IDeobfuscator> _deobfuscators;

		#endregion

		#region Properties

		public static IList<IDeobfuscatorInfo> DeobfuscatorInfos
		{
			get { return _deobfuscatorinfos ?? (_deobfuscatorinfos = CreateDeobfuscatorInfos()); }
		}

		public static IList<IDeobfuscator> Deobfuscators
		{
			get { return _deobfuscators ?? (_deobfuscators = CreateDeobfuscators()); }
		}

		#endregion

		#region Methods

		private static IList<IDeobfuscatorInfo> CreateDeobfuscatorInfos()
		{
			return new List<IDeobfuscatorInfo>
			{
				new de4dot.code.deobfuscators.Unknown.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Agile_NET.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Babel_NET.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.CodeFort.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.CodeVeil.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.CodeWall.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.CryptoObfuscator.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.DeepSea.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Dotfuscator.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.dotNET_Reactor.v3.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.dotNET_Reactor.v4.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Eazfuscator_NET.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Goliath_NET.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.ILProtector.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.MaxtoCode.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.MPRESS.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Rummage.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Skater_NET.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.SmartAssembly.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Spices_Net.DeobfuscatorInfo(),
				new de4dot.code.deobfuscators.Xenocode.DeobfuscatorInfo(),
			};
		}

		private static IList<IDeobfuscator> CreateDeobfuscators()
		{
			return CreateDeobfuscatorInfos().Select(di => di.CreateDeobfuscator()).ToList();
		}

		public static bool IsUnknownDeobfuscator(IObfuscatedFile file)
		{
			return (file == null || file.Deobfuscator == null ||
			        file.Deobfuscator is de4dot.code.deobfuscators.Unknown.Deobfuscator);
		}

		public static IObfuscatedFile SearchDeobfuscator(string filename)
		{
			TheAssemblyResolver.Instance.ClearAll();

			var fileOptions = new ObfuscatedFile.Options {Filename = filename};
			var moduleContext = new ModuleContext(TheAssemblyResolver.Instance);

			var ofile = new ObfuscatedFile(fileOptions, moduleContext, new NewAppDomainAssemblyClientFactory())
			{
				DeobfuscatorContext = new DeobfuscatorContext(),
			};

			try
			{
				ofile.Load(CreateDeobfuscatorInfos().Select(di => di.CreateDeobfuscator()).ToList());
			}
			catch (Exception)
			{
				return null;
			}

			return ofile;
		}

		#endregion
	}
}