/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System;
using System.IO;
using System.Windows.Forms;
using Be.Windows.Forms;
using Mono.Cecil;
using Reflexil.Properties;
using Reflexil.Utils;
using Reflexil.Wrappers;

namespace Be.HexEditor
{
    public partial class HexEditorControl : UserControl
    {
        readonly HexFindForm _formFind = new HexFindForm();
        HexFindCancelForm _formFindCancel;
        HexGotoForm _formGoto = new HexGotoForm();
        byte[] _findBuffer = new byte[0];
        private EmbeddedResource _resource;

        public HexEditorControl()
        {
            InitializeComponent();

            ManageAbility();
        }

        public override void Refresh()
        {
            if (_formGoto != null)
            {
                _formGoto.Dispose();
                _formGoto = null;
            }
            _formGoto = new HexGotoForm();
            base.Refresh();
            Position_Changed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the size status label
        /// </summary>
        void UpdateSizeStatus()
        {
            if (this.hexBox.ByteProvider == null)
                this.sizeLabel.Text = string.Empty;
            else
                this.sizeLabel.Text = ByteHelper.GetDisplayBytes(this.hexBox.ByteProvider.Length);
        }

        /// <summary>
        /// Manages enabling or disabling of menu items and toolstrip buttons.
        /// </summary>
        void ManageAbility()
        {
            if (hexBox.ByteProvider == null)
            {
                openToolStripMenuItem.Enabled = openToolStripButton.Enabled = false;
                saveToolStripMenuItem.Enabled = saveToolStripButton.Enabled = false;

                findToolStripMenuItem.Enabled = false;
                findNextToolStripMenuItem.Enabled = false;
                goToToolStripMenuItem.Enabled = false;

                selectAllToolStripMenuItem.Enabled = false;
            }
            else
            {
                openToolStripMenuItem.Enabled = openToolStripButton.Enabled = true;
                saveToolStripMenuItem.Enabled = saveToolStripButton.Enabled = true;

                findToolStripMenuItem.Enabled = true;
                findNextToolStripMenuItem.Enabled = true;
                goToToolStripMenuItem.Enabled = true;

                selectAllToolStripMenuItem.Enabled = true;
            }

            ManageAbilityForCopyAndPaste();
        }

        /// <summary>
        /// Manages enabling or disabling of menustrip items and toolstrip buttons for copy and paste
        /// </summary>
        void ManageAbilityForCopyAndPaste()
        {
            copyHexStringToolStripMenuItem.Enabled = 
                copyToolStripSplitButton.Enabled = copyToolStripMenuItem.Enabled = hexBox.CanCopy();

            cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled = hexBox.CanCut();
            pasteToolStripSplitButton.Enabled = pasteToolStripMenuItem.Enabled = hexBox.CanPaste();
            pasteHexToolStripMenuItem.Enabled = pasteHexToolStripMenuItem1.Enabled = hexBox.CanPasteHex();
        }

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        void OpenFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Bind a ressource.
        /// </summary>
        /// <param name="resource">resource to bind</param>
        public void Bind(EmbeddedResource resource)
        {
            CleanUp();
            
            if (resource != null)
            {
                IByteProvider provider = new DynamicByteProvider(resource.Data);
                provider.Changed += new EventHandler(byteProvider_Changed);
                provider.LengthChanged += new EventHandler(byteProvider_LengthChanged);

                hexBox.ByteProvider = provider;
                _resource = resource;
                hexBox.ReadOnly = false;
            } else
            {
                hexBox.ReadOnly = true;
            }

            UpdateSizeStatus();
            ManageAbility();
        }


        /// <summary>
        /// Opens a file.
        /// </summary>
        /// <param name="fileName">the file name of the file to open</param>
        public void OpenFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Unable to find file");
                return;
            }


            if (hexBox.ByteProvider == null)
                return;

            try
            {
                try
                {
                    hexBox.ByteProvider.DeleteBytes(0, hexBox.ByteProvider.Length);
                    hexBox.ByteProvider.InsertBytes(0, File.ReadAllBytes(fileName));
                    Bind(_resource); // force refresh
                }
                catch (IOException) 
                {
                    // file cannot be opened
                    MessageBox.Show("Unable to open file (locked by another process ?)");
                    return;
                }

                UpdateSizeStatus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            finally
            {

                ManageAbility();
            }
        }

        /// <summary>
        /// Saves to file
        /// </summary>
        void SaveFile()
        {
            if (_resource != null)
                saveFileDialog.FileName = _resource.Name;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                SaveFile(saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Saves to file
        /// </summary>
        void SaveFile(string fileName)
        {
            if (hexBox.ByteProvider == null)
                return;

            try
            {
                if (_resource != null)
                    File.WriteAllBytes(fileName, _resource.Data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ManageAbility();
            }
        }

        void CleanUp()
        {
            if (hexBox.ByteProvider != null)
            {
                IDisposable byteProvider = hexBox.ByteProvider as IDisposable;
                if (byteProvider != null)
                    byteProvider.Dispose();
                hexBox.ByteProvider = null;
            }

            _resource = null;
        }

        /// <summary>
        /// Opens the Find dialog
        /// </summary>
        void Find()
        {
            if (_formFind.ShowDialog() == DialogResult.OK)
            {
                _findBuffer = _formFind.GetFindBytes();
                FindNext();
            }
        }

        /// <summary>
        /// Find next match
        /// </summary>
        void FindNext()
        {
            if (_findBuffer.Length == 0)
            {
                Find();
                return;
            }

            // show cancel dialog
            _formFindCancel = new HexFindCancelForm();
            _formFindCancel.SetHexBox(hexBox);
            _formFindCancel.Closed += new EventHandler(FormFindCancel_Closed);
            _formFindCancel.Show();

            // block activation of main form
            //Activated += new EventHandler(FocusToFormFindCancel);

            // start find process
            long res = hexBox.Find(_findBuffer, hexBox.SelectionStart + hexBox.SelectionLength);

            _formFindCancel.Dispose();

            // unblock activation of main form
            //Activated -= new EventHandler(FocusToFormFindCancel);

            if (res == -1) // -1 = no match
            {
                MessageBox.Show("End of data reached", string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (res == -2) // -2 = find was aborted
            {
                return;
            }
            else // something was found
            {
                if (!hexBox.Focused)
                    hexBox.Focus();
            }

            ManageAbility();
        }

        /// <summary>
        /// Aborts the current find process
        /// </summary>
        void FormFindCancel_Closed(object sender, EventArgs e)
        {
            hexBox.AbortFind();
        }

        /// <summary>
        /// Put focus back to the cancel form.
        /// </summary>
        void FocusToFormFindCancel(object sender, EventArgs e)
        {
            _formFindCancel.Focus();
        }

        /// <summary>
        /// Displays the goto byte dialog.
        /// </summary>
        void Goto()
        {
            _formGoto.SetDefaultValue(hexBox.SelectionStart);
            if (_formGoto.ShowDialog() == DialogResult.OK)
            {
                hexBox.SelectionStart = Math.Min(_formGoto.GetByteIndex(), hexBox.ByteProvider.Length - 1);
                hexBox.SelectionLength = 1;
                hexBox.Focus();
            }
        }

        void hexBox_Copied(object sender, EventArgs e)
        {
            ManageAbilityForCopyAndPaste();
        }

        void hexBox_CopiedHex(object sender, EventArgs e)
        {
            ManageAbilityForCopyAndPaste();
        }

        void hexBox_SelectionLengthChanged(object sender, System.EventArgs e)
        {
            ManageAbilityForCopyAndPaste();
        }

        void hexBox_SelectionStartChanged(object sender, System.EventArgs e)
        {
            ManageAbilityForCopyAndPaste();
        }

        void Position_Changed(object sender, EventArgs e)
        {
            this.offsetLabel.Text = string.Format("Offset {0}", 
                OperandDisplayHelper.Changebase(Math.Max(0, (hexBox.CurrentLine-1) * hexBox.BytesPerLine + hexBox.CurrentPositionInLine - 1).ToString(), ENumericBase.Dec, Settings.Default.OperandDisplayBase));
        }

        void byteProvider_Changed(object sender, EventArgs e)
        {
            ManageAbility();

            if (_resource != null)
                _resource.Data = (hexBox.ByteProvider as DynamicByteProvider).Bytes.ToArray();
        }

        void byteProvider_LengthChanged(object sender, EventArgs e)
        {
            UpdateSizeStatus();
        }

        void open_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        void save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        void cut_Click(object sender, EventArgs e)
        {
            this.hexBox.Cut();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            this.hexBox.Copy();
        }

        void paste_Click(object sender, EventArgs e)
        {
            this.hexBox.Paste();
        }

        private void copyHex_Click(object sender, EventArgs e)
        {
            this.hexBox.CopyHex();
        }

        private void pasteHex_Click(object sender, EventArgs e)
        {
            this.hexBox.PasteHex();
        }

        void find_Click(object sender, EventArgs e)
        {
            this.Find();
        }

        void findNext_Click(object sender, EventArgs e)
        {
            this.FindNext();
        }

        void goTo_Click(object sender, EventArgs e)
        {
            this.Goto();
        }

        void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hexBox.SelectAll();
        }
    }
}