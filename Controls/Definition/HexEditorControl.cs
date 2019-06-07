/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Be.Windows.Forms;
using Mono.Cecil;
using Reflexil.Properties;
using Reflexil.Utils;
using Reflexil.Wrappers;

namespace Reflexil.Editors
{
	public partial class HexEditorControl : UserControl
	{
		private readonly HexFindForm _formFind = new HexFindForm();
		private HexFindCancelForm _formFindCancel;
		private HexGotoForm _formGoto = new HexGotoForm();
		private byte[] _findBuffer = new byte[0];
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

		private void UpdateSizeStatus()
		{
			sizeLabel.Text = hexBox.ByteProvider == null ? string.Empty : ByteHelper.GetDisplayBytes(hexBox.ByteProvider.Length);
		}

		private void ManageAbility()
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

		private void ManageAbilityForCopyAndPaste()
		{
			copyHexStringToolStripMenuItem.Enabled =
				copyToolStripSplitButton.Enabled = copyToolStripMenuItem.Enabled = hexBox.CanCopy();

			cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled = hexBox.CanCut();
			pasteToolStripSplitButton.Enabled = pasteToolStripMenuItem.Enabled = hexBox.CanPaste();
			pasteHexToolStripMenuItem.Enabled = pasteHexToolStripMenuItem1.Enabled = hexBox.CanPasteHex();
		}

		private void OpenFile()
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				OpenFile(openFileDialog.FileName);
			}
		}

		public void Bind(EmbeddedResource resource)
		{
			CleanUp();

			if (resource != null)
			{
				IByteProvider provider = new DynamicByteProvider(resource.Data);
				provider.Changed += byteProvider_Changed;
				provider.LengthChanged += byteProvider_LengthChanged;

				hexBox.ByteProvider = provider;
				_resource = resource;
				hexBox.ReadOnly = false;
			}
			else
			{
				hexBox.ReadOnly = true;
			}

			UpdateSizeStatus();
			ManageAbility();
		}


		public void OpenFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				MessageBox.Show(@"Unable to find file");
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
					MessageBox.Show(@"Unable to open file (locked by another process ?)");
					return;
				}

				UpdateSizeStatus();
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

		private void SaveFile()
		{
			if (_resource != null)
				saveFileDialog.FileName = _resource.Name;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				SaveFile(saveFileDialog.FileName);
			}
		}

		private void SaveFile(string fileName)
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

		private void CleanUp()
		{
			if (hexBox.ByteProvider != null)
			{
				var byteProvider = hexBox.ByteProvider as IDisposable;
				if (byteProvider != null)
					byteProvider.Dispose();
				hexBox.ByteProvider = null;
			}

			_resource = null;
		}

		private void Find()
		{
			if (_formFind.ShowDialog() != DialogResult.OK)
				return;

			_findBuffer = _formFind.GetFindBytes();
			FindNext();
		}

		private void FindNext()
		{
			if (_findBuffer.Length == 0)
			{
				Find();
				return;
			}

			// show cancel dialog
			_formFindCancel = new HexFindCancelForm();
			_formFindCancel.SetHexBox(hexBox);
			_formFindCancel.Closed += FormFindCancel_Closed;
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
				MessageBox.Show(@"End of data reached", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private void FormFindCancel_Closed(object sender, EventArgs e)
		{
			hexBox.AbortFind();
		}

		private void Goto()
		{
			_formGoto.SetDefaultValue(hexBox.SelectionStart);
			if (_formGoto.ShowDialog() != DialogResult.OK)
				return;

			hexBox.SelectionStart = Math.Min(_formGoto.GetByteIndex(), hexBox.ByteProvider.Length - 1);
			hexBox.SelectionLength = 1;
			hexBox.Focus();
		}

		private void hexBox_Copied(object sender, EventArgs e)
		{
			ManageAbilityForCopyAndPaste();
		}

		private void hexBox_CopiedHex(object sender, EventArgs e)
		{
			ManageAbilityForCopyAndPaste();
		}

		private void hexBox_SelectionLengthChanged(object sender, EventArgs e)
		{
			ManageAbilityForCopyAndPaste();
		}

		private void hexBox_SelectionStartChanged(object sender, EventArgs e)
		{
			ManageAbilityForCopyAndPaste();
		}

		private void Position_Changed(object sender, EventArgs e)
		{
			offsetLabel.Text = string.Format("Offset {0}",
				OperandDisplayHelper.Changebase(
					Math.Max(0, (hexBox.CurrentLine - 1) * hexBox.BytesPerLine + hexBox.CurrentPositionInLine - 1)
						.ToString(CultureInfo.InvariantCulture), ENumericBase.Dec, Settings.Default.OperandDisplayBase));
		}

		private void byteProvider_Changed(object sender, EventArgs e)
		{
			ManageAbility();

			var provider = hexBox.ByteProvider as DynamicByteProvider;
			if (_resource != null && provider != null)
				_resource.Data = provider.Bytes.ToArray();
		}

		private void byteProvider_LengthChanged(object sender, EventArgs e)
		{
			UpdateSizeStatus();
		}

		private void open_Click(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void save_Click(object sender, EventArgs e)
		{
			SaveFile();
		}

		private void cut_Click(object sender, EventArgs e)
		{
			hexBox.Cut();
		}

		private void copy_Click(object sender, EventArgs e)
		{
			hexBox.Copy();
		}

		private void paste_Click(object sender, EventArgs e)
		{
			hexBox.Paste();
		}

		private void copyHex_Click(object sender, EventArgs e)
		{
			hexBox.CopyHex();
		}

		private void pasteHex_Click(object sender, EventArgs e)
		{
			hexBox.PasteHex();
		}

		private void find_Click(object sender, EventArgs e)
		{
			Find();
		}

		private void findNext_Click(object sender, EventArgs e)
		{
			FindNext();
		}

		private void goTo_Click(object sender, EventArgs e)
		{
			Goto();
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			hexBox.SelectAll();
		}
	}
}
