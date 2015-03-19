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
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Forms;
using Reflexil.Plugins;
using Reflexil.Verifier;

#endregion

namespace Reflexil.Utils
{
	/// <summary>
	/// Common assembly tasks
	/// </summary>
	public static class AssemblyHelper
	{
		#region Methods

		/// <summary>
		/// Verify an assembly with peverify
		/// </summary>
		/// <param name="adef">Assembly definition</param>
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
						MessageBox.Show(String.Format("Reflexil is unable to verify this assembly: {0}", ex.Message));
					}
					finally
					{
						File.Delete(tempFilename);
					}
				}
				else
				{
					MessageBox.Show(
						@"Warning, PEVerify Utility (peverify.exe) not found. Update your PATH environment variable or install .NET SDK");
				}
			}
			else
			{
				MessageBox.Show(@"Assembly definition is not loaded (not a CLI image?)");
			}
		}

		/// <summary>
		/// Reload an assembly (cecil object model)
		/// </summary>
		/// <param name="originalLocation">Original location</param>
		/// <returns>the new assembly or null if failed</returns>
		public static AssemblyDefinition ReloadAssembly(string originalLocation)
		{
			if (
				MessageBox.Show(@"Are you sure to reload assembly, discarding all changes?", @"Confirmation",
					MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				var context = PluginFactory.GetInstance().ReloadAssemblyContext(originalLocation);
				if (context != null)
					return context.AssemblyDefinition;
			}
			return null;
		}

		/// <summary>
		/// Search for an obfuscator
		/// </summary>
		/// <param name="location">location</param>
		/// <param name="silentifnone">stay silent if none</param>
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
						dialog.Filter = @"Assembly files (*.exe, *.dll)|*.exe;*.dll";
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

		/// <summary>
		/// Save an assembly
		/// </summary>
		/// <param name="adef">Assembly definition</param>
		public static void SaveAssembly(AssemblyDefinition adef)
		{
			if (adef != null)
			{
				var location = adef.MainModule.Image.FileName;

				using (var dialog = new SaveFileDialog())
				{
					dialog.Filter = @"Assembly files (*.exe, *.dll)|*.exe;*.dll";
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
							var plugin = PluginFactory.GetInstance() as BasePlugin;
							if (plugin != null)
								adef = plugin.LoadAssembly(dialog.FileName, false);

							using (var snform = new StrongNameForm())
							{
								snform.AssemblyDefinition = adef;
								snform.DelaySignedFileName = dialog.FileName;
								snform.ShowDialog();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(String.Format("Reflexil is unable to save this assembly: {0}", ex.Message));
						}
					}
				}
			}
			else
			{
				MessageBox.Show(@"Assembly definition is not loaded (not a CLI image?)");
			}
		}

		#endregion
	}
}