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
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Properties;
using Reflexil.Utils;
using Reflexil.Wrappers;
using Reflexil.Plugins;

#endregion

namespace Reflexil.Editors
{
	public partial class GridControl<T, TD> : UserControl
	{
		#region Fields

		private Rectangle _dragBox;
		private int _dragIndex;
		private int _firstRowIndex;
		private int _hScrollOffset;

		#endregion

		#region Properties

		public bool ReadOnly { get; set; }
		public TD OwnerDefinition { get; protected set; }

		public T FirstSelectedItem
		{
			get
			{
				var selections = SelectedItems;
				return selections.Length > 0 ? selections[0] : default(T);
			}
		}

		public T[] SelectedItems
		{
			get
			{
				var result = new List<T>();
				if (Grid.SelectedRows.Count > 0)
				{
					for (var i = 0; i < Grid.SelectedRows.Count; i++)
					{
						result.Add((T) (Grid.SelectedRows[i].DataBoundItem));
					}
				}
				return result.ToArray();
			}
		}

		#endregion

		#region Methods

		#region Scrolling

		public void SaveScrollBarPositions()
		{
			_firstRowIndex = Grid.FirstDisplayedScrollingRowIndex;
			_hScrollOffset = Grid.HorizontalScrollingOffset;
		}

		public void RestoreScrollBarPositions()
		{
			if (_firstRowIndex < Grid.RowCount && _firstRowIndex >= 0)
			{
				Grid.FirstDisplayedScrollingRowIndex = _firstRowIndex;
			}
			Grid.HorizontalScrollingOffset = _hScrollOffset;
		}

		#endregion

		#region Drag&Drop

		private void Grid_MouseDown(object sender, MouseEventArgs e)
		{
			var dataGridView = sender as DataGridView;
			if (dataGridView != null)
				_dragIndex = dataGridView.HitTest(e.X, e.Y).RowIndex;

			if (_dragIndex != -1)
			{
				var dragSize = SystemInformation.DragSize;
				_dragBox = new Rectangle(new Point(e.X - (dragSize.Width/2), e.Y - (dragSize.Height/2)), dragSize);
			}
			else
			{
				_dragBox = Rectangle.Empty;
			}
		}

		private void Grid_MouseMove(object sender, MouseEventArgs e)
		{
			var grid = sender as DataGridView;
			if (grid == null)
				return;

			if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
				return;

			if (_dragBox != Rectangle.Empty &&
			    !_dragBox.Contains(e.X, e.Y))
			{
				grid.DoDragDrop(grid.Rows[_dragIndex], DragDropEffects.Move);
			}
		}

		private void Grid_DragDrop(object sender, DragEventArgs e)
		{
			var grid = sender as DataGridView;
			if (grid == null)
				return;

			var clientPoint = grid.PointToClient(new Point(e.X, e.Y));
			var rowindex = grid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

			if (e.Effect != DragDropEffects.Move || rowindex < 0 || rowindex >= grid.Rows.Count)
				return;

			var sourceRow = e.Data.GetData(typeof (DataGridViewRow)) as DataGridViewRow;
			var targetRow = grid.Rows[rowindex];

			DoDragDrop(sender, sourceRow, targetRow, e);
		}

		private void Grid_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = (ReadOnly) ? DragDropEffects.None : DragDropEffects.Move;
		}

		protected virtual void DoDragDrop(object sender, DataGridViewRow sourceRow, DataGridViewRow targetRow, DragEventArgs e)
		{
		}

		#endregion

		public GridControl()
		{
			InitializeComponent();
		}

		public virtual void Bind(TD odef)
		{
			OwnerDefinition = odef;
		}

		public virtual void Rehash()
		{
			SaveScrollBarPositions();
			ResetBindingSourceBindings();
			RestoreScrollBarPositions();
		}

		public virtual void ResetBindingSourceBindings()
		{
			BindingSource.ResetBindings(false);
		}

		#endregion

		#region Events

		public delegate void GridUpdatedEventHandler(object sender, EventArgs e);

		public event GridUpdatedEventHandler GridUpdated;

		protected virtual void RaiseGridUpdated()
		{
			if (GridUpdated != null) GridUpdated(this, EventArgs.Empty);
		}

		protected virtual void MenCreate_Click(object sender, EventArgs e)
		{
		}

		protected virtual void MenEdit_Click(object sender, EventArgs e)
		{
		}

		protected virtual void MenDelete_Click(object sender, EventArgs e)
		{
		}

		protected virtual void MenDeleteAll_Click(object sender, EventArgs e)
		{
		}

		protected virtual void GridContextMenuStrip_Opened(object sender, EventArgs e)
		{
		}

		#endregion

		#region Grid

		private void Grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			var grid = (DataGridView) sender;
			string strRowNumber = OperandDisplayHelper.Changebase(e.RowIndex.ToString(CultureInfo.InvariantCulture),
				ENumericBase.Dec, Settings.Default.RowIndexDisplayBase);
			string strRowCount = OperandDisplayHelper.Changebase(grid.RowCount.ToString(CultureInfo.InvariantCulture),
				ENumericBase.Dec, Settings.Default.RowIndexDisplayBase);

			while (strRowNumber.Length < strRowCount.Length)
			{
				strRowNumber = "0" + strRowNumber;
			}

			var size = e.Graphics.MeasureString(strRowNumber, grid.Font);

			if (grid.RowHeadersWidth < (size.Width + 20))
			{
				grid.RowHeadersWidth = Convert.ToInt32(size.Width + 20);
			}

			var b = SystemBrushes.ControlText;
			e.Graphics.DrawString(strRowNumber, grid.Font, b, e.RowBounds.Location.X + 15,
				e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height)/2));
		}

		protected virtual void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if ((e.Value) is OpCode)
			{
				Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = PluginFactory.GetInstance().GetOpcodeDesc((OpCode) e.Value);
			}
			else if (e.Value is MethodDefinition)
			{
				var mdef = e.Value as MethodDefinition;
				Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = String.Format("RVA: {0}", mdef.RVA);
			}
			else if (e.Value is TypeReference && Grid.Rows[e.RowIndex].DataBoundItem is CustomAttributeArgument)
			{
				// Hack to display terminal attribute type (can be wrapped)
				var argument = (CustomAttributeArgument) Grid.Rows[e.RowIndex].DataBoundItem;
				if (!(argument.Value is CustomAttributeArgument))
					return;

				var wrappedargument = (CustomAttributeArgument) argument.Value;
				e.Value = wrappedargument.Type;
			}
			else if (e.Value is CustomAttributeArgument)
			{
				e.Value = OperandDisplayHelper.ToString((CustomAttributeArgument) e.Value);
			}
			else if (e.Value is CustomAttributeArgument[])
			{
				e.Value = OperandDisplayHelper.ToString(e.Value as CustomAttributeArgument[]);
			}
			else if (OwnerDefinition is MethodDefinition)
			{
				if ((e.Value is Int16 || e.Value is Int32 || e.Value is Int64 || e.Value is SByte)
				    || (e.Value is UInt16 || e.Value is UInt32 || e.Value is UInt64 || e.Value is Byte))
				{
					var tipbuilder = new StringBuilder();
					var values = Enum.GetValues(typeof (ENumericBase));
					for (var i = 0; i < values.Length; i++)
					{
						if (i > 0)
						{
							tipbuilder.AppendLine();
						}
						var item = (ENumericBase) values.GetValue(i);
						tipbuilder.Append(item);
						tipbuilder.Append(": ");
						tipbuilder.Append(OperandDisplayHelper.Changebase(e.Value.ToString(), ENumericBase.Dec, item));
					}
					Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tipbuilder.ToString();
				}
				e.Value = OperandDisplayHelper.ToString(OwnerDefinition as MethodDefinition, e.Value);
			}
		}

		#endregion
	}
}