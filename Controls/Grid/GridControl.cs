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
using System.Collections.Generic;
using System.Drawing;
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
	public partial class GridControl<T, TD>: UserControl
	{

        #region " Fields "
        private Rectangle m_dragbox;
        private int m_dragindex;
        private int m_firstrowindex;
        private int m_hscrolloffset;
        private bool m_readonly;
        private TD m_odef;
        #endregion

        #region " Properties "
        public bool ReadOnly
        {
            get
            {
                return m_readonly;
            }
            set
            {
                m_readonly = value;
            }
        }

        public TD OwnerDefinition
        {
            get
            {
                return m_odef;
            }
            protected set
            {
                m_odef = value;
            }
        }

        public T FirstSelectedItem
        {
            get
            {
                T[] selections = SelectedItems;
                if (selections.Length > 0)
                {
                    return selections[0];
                }
                return default(T);
            }
        }

        public T[] SelectedItems
        {
            get
            {
                List<T> result = new List<T>();
                if (Grid.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < Grid.SelectedRows.Count; i++)
                    {
                        result.Add((T)(Grid.SelectedRows[i].DataBoundItem));
                    }
                }
                return result.ToArray();
            }
        }
        #endregion

        #region " Methods "

        #region " Scrolling "
        public void SaveScrollBarPositions()
        {
            m_firstrowindex = Grid.FirstDisplayedScrollingRowIndex;
            m_hscrolloffset = Grid.HorizontalScrollingOffset;
        }

        public void RestoreScrollBarPositions()
        {
            if (m_firstrowindex < Grid.RowCount && m_firstrowindex >= 0)
            {
                Grid.FirstDisplayedScrollingRowIndex = m_firstrowindex;
            }
            Grid.HorizontalScrollingOffset = m_hscrolloffset;
        }
        #endregion

        #region " Drag&Drop "
        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            m_dragindex = (sender as DataGridView).HitTest(e.X, e.Y).RowIndex;
            if (m_dragindex != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                m_dragbox = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                m_dragbox = Rectangle.Empty;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (m_dragbox != Rectangle.Empty &&
                !m_dragbox.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = grid.DoDragDrop(grid.Rows[m_dragindex], DragDropEffects.Move);
                }
            }
        }

        private void Grid_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            Point clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            int rowindex = grid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (e.Effect == DragDropEffects.Move && rowindex >= 0 && rowindex < grid.Rows.Count)
            {
                DataGridViewRow sourceRow = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                DataGridViewRow targetRow = grid.Rows[rowindex];

                DoDragDrop(sender, sourceRow, targetRow, e);
            }
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

        #region " Events "
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

        #region " Grid "
        private void Grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            string strRowNumber = OperandDisplayHelper.Changebase(e.RowIndex.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase);
            string strRowCount = OperandDisplayHelper.Changebase(grid.RowCount.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase);

            while (strRowNumber.Length < strRowCount.Length)
            {
                strRowNumber = "0" + strRowNumber;
            }

            SizeF size = e.Graphics.MeasureString(strRowNumber, grid.Font);

            if (grid.RowHeadersWidth < (size.Width + 20))
            {
                grid.RowHeadersWidth = Convert.ToInt32(size.Width + 20);
            }

            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, grid.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        protected virtual void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value) is OpCode)
            {
                Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = PluginFactory.GetInstance().GetOpcodeDesc((OpCode)e.Value);
            }
            else if (e.Value is MethodDefinition)
            {
                MethodDefinition mdef = e.Value as MethodDefinition;
                Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = String.Format("RVA: {0}", mdef.RVA);
            }
            else if (e.Value is TypeReference && Grid.Rows[e.RowIndex].DataBoundItem is CustomAttributeArgument)
            {
                // Hack to display terminal attribute type (can be wrapped)
                var argument = (CustomAttributeArgument) Grid.Rows[e.RowIndex].DataBoundItem;
                if (argument.Value is CustomAttributeArgument)
                {
                    var wrappedargument = (CustomAttributeArgument) argument.Value;
                    e.Value = wrappedargument.Type;
                }

            }
            else if (e.Value is CustomAttributeArgument)
            {
                e.Value = Wrappers.OperandDisplayHelper.ToString((CustomAttributeArgument)e.Value);
            }
            else if (e.Value is CustomAttributeArgument[])
            {
                e.Value = Wrappers.OperandDisplayHelper.ToString(e.Value as CustomAttributeArgument[]);
            }
            else if (OwnerDefinition is MethodDefinition)
            {
                if ((e.Value is Int16 || e.Value is Int32 || e.Value is Int64 || e.Value is SByte)
                    || (e.Value is UInt16 || e.Value is UInt32 || e.Value is UInt64 || e.Value is Byte))
                {
                    StringBuilder tipbuilder = new StringBuilder();
                    Array values = System.Enum.GetValues(typeof(ENumericBase));
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (i > 0)
                        {
                            tipbuilder.AppendLine();
                        }
                        ENumericBase item = (ENumericBase)values.GetValue(i);
                        tipbuilder.Append(item.ToString());
                        tipbuilder.Append(": ");
                        tipbuilder.Append(OperandDisplayHelper.Changebase(e.Value.ToString(), ENumericBase.Dec, item));
                    }
                    Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tipbuilder.ToString();
                }
                e.Value = Wrappers.OperandDisplayHelper.ToString(OwnerDefinition as MethodDefinition, e.Value);
            }
        }
        #endregion

	}
}
