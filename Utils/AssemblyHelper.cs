/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
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
