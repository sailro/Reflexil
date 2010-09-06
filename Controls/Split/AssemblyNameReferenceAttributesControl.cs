/* Reflexil Copyright (c) 2007-2010 Sebastien LEBRETON

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
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Assembly Name reference attributes editor (all object readable/writeable non indexed properties)
    /// </summary>
    public partial class AssemblyNameReferenceAttributesControl : BaseAssemblyNameReferenceAttributesControl
    {

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public AssemblyNameReferenceAttributesControl()
        {
            InitializeComponent();
            Algorithm.DataSource = System.Enum.GetValues(typeof(AssemblyHashAlgorithm));
        }

        /// <summary>
        /// Convert an array of byte to a hex-string
        /// </summary>
        /// <param name="input">array to convert</param>
        /// <returns>resulting hex-string</returns>
        private string ByteToString(Byte[] input)
        {
            if (input != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    sb.Append(input[i].ToString("x2"));
                }
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert a hex-string to an array of byte
        /// </summary>
        /// <param name="input">hex-string to convert</param>
        /// <returns>resulting array</returns>
        private byte[] StringToByte(string input)
        {
            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Byte.Parse(input.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return result;
        }

        /// <summary>
        /// Bind an assembly name reference this control
        /// </summary>
        /// <param name="anref">Assembly name reference to bind</param>
        public override void Bind(AssemblyNameReference anref)
        {
            base.Bind(anref);
            if (anref != null)
            {
                AssemblyName.Text = anref.Name;
                AssemblyCulture.Text = anref.Culture;
                Major.Value = anref.Version.Major;
                Minor.Value = anref.Version.Minor;
                Build.Value = anref.Version.Build;
                Revision.Value = anref.Version.Revision;
                PublicKey.Text = ByteToString(anref.PublicKey);
                PublicKeyToken.Text = ByteToString(anref.PublicKeyToken);
                Hash.Text = ByteToString(anref.Hash);
                Algorithm.SelectedItem = anref.HashAlgorithm;
            }
            else
            {
                AssemblyName.Text = string.Empty;
                AssemblyCulture.Text = string.Empty;
                Major.Value = 0;
                Minor.Value = 0;
                Build.Value = 0;
                Revision.Value = 0;
                PublicKey.Text = string.Empty;
                PublicKeyToken.Text = string.Empty;
                Hash.Text = string.Empty;
                Algorithm.SelectedIndex = -1;
            }
        }
        #endregion

        #region " Events "
        /// <summary>
        /// Name validation
        /// </summary>
        /// <param name="sender">object to validate</param>
        /// <param name="e">parameters</param>
        private void AssemblyName_Validating(object sender, CancelEventArgs e)
        {
            if (AssemblyName.Text.Length == 0)
            {
                ErrorProvider.SetError(AssemblyName, "Name is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(AssemblyName, string.Empty);
            }
        }

        /// <summary>
        /// PublicKey, PublicKeyToken, Hash validation
        /// </summary>
        /// <param name="sender">object to validate</param>
        /// <param name="e">parameters</param>
        private void StringToByte_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string input = (sender as TextBox).Text;
                if (input.Length % 2 == 0)
                {
                    byte[] test = StringToByte(input);
                    ErrorProvider.SetError(sender as Control, string.Empty);
                    return;
                }
            }
            catch (Exception) { }
            ErrorProvider.SetError(sender as Control, "Incorrect byte sequence");
            e.Cancel = true;
        }

        /// <summary>
        /// Name update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void AssemblyName_Validated(object sender, EventArgs e)
        {
            Item.Name = AssemblyName.Text;
        }

        /// <summary>
        /// Culture update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void AssemblyCulture_Validated(object sender, EventArgs e)
        {
            Item.Culture = AssemblyCulture.Text;
        }

        /// <summary>
        /// Version update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void Version_Validated(object sender, EventArgs e)
        {
            Item.Version = new Version(Convert.ToInt32(Major.Value), Convert.ToInt32(Minor.Value), Convert.ToInt32(Build.Value), Convert.ToInt32(Revision.Value));
        }

        /// <summary>
        /// PublicKey update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void PublicKey_Validated(object sender, EventArgs e)
        {
            if (ByteToString(Item.PublicKey) != PublicKey.Text)
            {
                Item.PublicKey = StringToByte(PublicKey.Text);
                Bind(Item);
            }
        }

        /// <summary>
        /// PublicKeyToken update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void PublicKeyToken_Validated(object sender, EventArgs e)
        {
            Item.PublicKeyToken = StringToByte(PublicKeyToken.Text);
        }

        /// <summary>
        /// Algorithm update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void Algorithm_Validated(object sender, EventArgs e)
        {
            Item.HashAlgorithm = (AssemblyHashAlgorithm)Algorithm.SelectedItem;
        }

        /// <summary>
        /// Hash update
        /// </summary>
        /// <param name="sender">Updater object</param>
        /// <param name="e">parameters</param>
        private void Hash_Validated(object sender, EventArgs e)
        {
            Item.Hash = StringToByte(Hash.Text);
        }
        #endregion
        
	}

    #region " VS Designer generic support "
    public class BaseAssemblyNameReferenceAttributesControl : SplitAttributesControl<AssemblyNameReference>
    {
    }
    #endregion
}
