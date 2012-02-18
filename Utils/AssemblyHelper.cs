/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Forms;
using Reflexil.Plugins;
using Reflexil.Verifier;
using de4dot.code;
using de4dot.code.renamer;

#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Common assembly tasks
    /// </summary>
	public static class AssemblyHelper
    {

        #region " Methods "
        /// <summary>
        /// Verify an assembly with peverify
        /// </summary>
        /// <param name="adef">Assembly definition</param>
        /// <param name="originallocation">Original location</param>
        public static void VerifyAssembly(AssemblyDefinition adef, string originallocation)
        {
            if (adef != null)
            {
                if (PEVerifyUtility.PEVerifyToolPresent)
                {
                    //String tempfilename = Path.GetTempFileName();
                    // We must create a temporary filename in the same path, so PEVerify can resolve dependencies
                    String tempfilename = Path.Combine(Path.GetDirectoryName(originallocation), Path.GetRandomFileName());
                    try
                    {

                        adef.Write(tempfilename);
                        AssemblyVerification.Verify(adef.MainModule.Image.FileName);
                        MessageBox.Show("All Classes and Methods Verified.");
                    }
                    catch (VerificationException ex)
                    {
                        using (VerifierForm form = new VerifierForm())
                        {
                            form.ShowDialog(ex.Errors);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Reflexil is unable to verify this assembly: {0}", ex.Message));
                    }
                    finally
                    {
                        File.Delete(tempfilename);
                    }
                }
                else
                {
                    MessageBox.Show("Warning, PEVerify Utility (peverify.exe) not found. Update your PATH environment variable or install .NET SDK");
                }
            }
            else
            {
                MessageBox.Show("Assembly definition is not loaded (not a CLI image?)");
            }
        }

        /// <summary>
        /// Reload an assembly (cecil object model)
        /// </summary>
        /// <param name="originallocation">Original location</param>
        /// <returns>the new assembly or null if failed</returns>
        public static AssemblyDefinition ReloadAssembly(string originallocation)
        {
            if (MessageBox.Show("Are you sure to reload assembly, discarding all changes?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                IAssemblyContext context = PluginFactory.GetInstance().ReloadAssemblyContext(originallocation);
                if (context != null)
                {
                    return context.AssemblyDefinition;
                }
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
            var ofile = De4dotHelper.SearchDeobfuscator(location);

            if (!De4dotHelper.IsUnknownDeobfuscator(ofile))
            {
                using (var form = new ObfuscatorForm())
                {
                    if (form.ShowDialog(location, ofile) == DialogResult.OK)
                    {
                        using (var SaveFileDialog = new SaveFileDialog())
                        {
                            SaveFileDialog.Filter = "Assembly files (*.exe, *.dll)|*.exe;*.dll";
                            SaveFileDialog.InitialDirectory = Path.GetDirectoryName(location);
                            SaveFileDialog.FileName = Path.GetFileNameWithoutExtension(location) + ".Cleaned" + Path.GetExtension(location);
                            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                using(var cleanform = new AssemblyCleanerForm())
                                {
                                    if (cleanform.ShowDialog(ofile, SaveFileDialog.FileName) == DialogResult.OK)
                                    {
                                        MessageBox.Show("Assembly cleaned");
                                    }
                                }
                                
                            }
                        }
                    }
                }
            } else
            {
                if (!silentifnone)
                    MessageBox.Show("No obfuscator found (or unknown)");
            }
        }

        /// <summary>
        /// Save an assembly
        /// </summary>
        /// <param name="adef">Assembly definition</param>
        /// <param name="originallocation">Original location</param>
        public static void SaveAssembly(AssemblyDefinition adef, string originallocation)
        {
            if (adef != null)
            {
                using (var SaveFileDialog = new SaveFileDialog())
                {
                    SaveFileDialog.Filter = "Assembly files (*.exe, *.dll)|*.exe;*.dll";
                    SaveFileDialog.InitialDirectory = Path.GetDirectoryName(originallocation);
                    SaveFileDialog.FileName = Path.GetFileNameWithoutExtension(originallocation) + ".Patched" + Path.GetExtension(originallocation);
                    if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            adef.Write(SaveFileDialog.FileName);
                            if (adef.Name.HasPublicKey)
                            {
                                using (StrongNameForm snform = new StrongNameForm())
                                {
                                    snform.AssemblyDefinition = adef;
                                    snform.DelaySignedFileName = SaveFileDialog.FileName;
                                    snform.ShowDialog();
                                }
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
                MessageBox.Show("Assembly definition is not loaded (not a CLI image?)");
            }
        }
        #endregion

    }
}
