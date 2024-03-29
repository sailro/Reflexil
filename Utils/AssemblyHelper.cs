﻿/* Reflexil Copyright (c) 2007-2021 Sebastien Lebreton

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

using System;
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Forms;
using Reflexil.Plugins;
using Reflexil.Verifier;

namespace Reflexil.Utils
{
	public static class AssemblyHelper
	{
		private const string AssemblyFilter = @"Assembly files (*.exe, *.dll)|*.exe;*.dll";

		public static void VerifyAssembly(AssemblyDefinition adef)
		{
			if (adef != null)
			{
				var originalLocation = adef.MainModule.Image.FileName;

				if (PEVerifyUtility.PEVerifyToolPresent)
				{
					// We must create a temporary filename in the same path, so PEVerify can resolve dependencies
					var tempDirectory = Path.GetDirectoryName(originalLocation) ?? string.Empty;
					var tempFilename = Path.Combine(tempDirectory, Path.GetRandomFileName());
					try
					{
						adef.Write(tempFilename);
						AssemblyVerification.Verify(tempFilename);
						MessageBox.Show(@"All Classes and Methods Verified.");
					}
					catch (VerificationException ex)
					{
						using (var form = new VerifierForm())
							form.ShowDialog(ex.Errors);
					}
					catch (Exception ex)
					{
						MessageBox.Show(string.Format("Reflexil is unable to verify this assembly: {0}", ex.Message));
					}
					finally
					{
						File.Delete(tempFilename);
					}
				}
				else
				{
					MessageBox.Show(@"Warning, PEVerify Utility (peverify.exe) not found. Update your PATH environment variable or install .NET SDK");
				}
			}
			else
			{
				MessageBox.Show(@"Assembly definition is not loaded (not a CLI image?)");
			}
		}

		public static AssemblyDefinition ReloadAssembly(string originalLocation)
		{
			if (MessageBox.Show(@"Are you sure to reload assembly, discarding all changes?", @"Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				var context = PluginFactory.GetInstance().ReloadAssemblyContext(originalLocation);
				if (context != null)
					return context.AssemblyDefinition;
			}

			return null;
		}

		public static void SearchObfuscator(string location, bool silentifnone = false)
		{
			var ofile = De4DotHelper.SearchDeobfuscator(location);

			if (!De4DotHelper.IsUnknownDeobfuscator(ofile))
			{
				using (var form = new ObfuscatorForm())
				{
					if (form.ShowDialog(location, ofile) != DialogResult.OK)
						return;

					using (var dialog = new SaveFileDialog())
					{
						dialog.Filter = AssemblyFilter;
						dialog.InitialDirectory = Path.GetDirectoryName(location);
						dialog.FileName = Path.GetFileNameWithoutExtension(location) + ".Cleaned" + Path.GetExtension(location);

						if (dialog.ShowDialog() != DialogResult.OK)
							return;

						using (var cleanform = new AssemblyCleanerForm())
						{
							if (cleanform.ShowDialog(ofile, dialog.FileName) == DialogResult.OK)
								MessageBox.Show(@"Assembly cleaned");
						}
					}
				}
			}
			else
			{
				if (!silentifnone)
					MessageBox.Show(@"No obfuscator found (or unknown)");
			}
		}

		public static void SaveAssembly(AssemblyDefinition adef)
		{
			if (adef != null)
			{
				var location = adef.MainModule.Image.FileName;

				using (var dialog = new SaveFileDialog())
				{
					dialog.Filter = AssemblyFilter;
					dialog.InitialDirectory = Path.GetDirectoryName(location);
					dialog.FileName = Path.GetFileNameWithoutExtension(location) + ".Patched" +
					                  Path.GetExtension(location);
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							adef.Write(dialog.FileName);

							if (!adef.Name.HasPublicKey)
								return;

							// Reload the assembly to have a proper Image.Filename
							adef = PluginFactory.GetInstance().LoadAssembly(dialog.FileName, false);

							using (var snform = new StrongNameForm())
							{
								snform.AssemblyDefinition = adef;
								snform.DelaySignedFileName = dialog.FileName;
								snform.ShowDialog();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("Reflexil is unable to save this assembly: {0}", ex.Message));
						}
					}
				}
			}
			else
			{
				MessageBox.Show(@"Assembly definition is not loaded (not a CLI image?)");
			}
		}
	}
}
