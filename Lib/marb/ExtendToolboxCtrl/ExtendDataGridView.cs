using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Lib.ExtendToolboxCtrl;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Data;

namespace Marb.ExtendToolboxCtrl
{
    [Serializable]
    [Designer(typeof(ControlDesigner))]
    public class ExtendDataGridView : DataGridView
    {

        private ContextMenuStrip _ContextMenuStrip = new ContextMenuStrip();
        public ExtendDataGridView()
        {
            _ContextMenuStrip.Items.Add("Delete row");
            _ContextMenuStrip.ItemClicked += _ContextMenuStrip_ItemClicked;
            //attach error handle exception when bad parsing occurs
            this.DataError += ExtendDataGridView_DataError;

            
            this.CellMouseUp += ExtendDataGridView_CellMouseUp;
        }




        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<DGV_ListBox> _DropDownBoxes = new List<DGV_ListBox>();


        public void AddDropDownBox(IDGV_ListBoxItem DGV_ListBoxItem)
        {
            DGV_ListBox newDGV_ListBox = new DGV_ListBox(this, DGV_ListBoxItem);
            _DropDownBoxes.Add(newDGV_ListBox);           
        }

        /// <summary>
        /// happens when values are entered which are impossible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="anError"></param>
        void ExtendDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            //MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }

            this.CancelEdit();
        }

        #region delete row
        private int rowIndex = 0;
        void ExtendDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                this.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;
                this.CurrentCell = this.Rows[e.RowIndex].Cells[1];
                this._ContextMenuStrip.BringToFront();
                this._ContextMenuStrip.Show(Cursor.Position);
            }

        }
        void _ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (!this.Rows[this.rowIndex].IsNewRow)
            {

                this.Rows.RemoveAt(this.rowIndex);
               
            }
        }
        #endregion



        private class DGV_ListBox
        {

            private ListBox _ListBox = null;
            private DataGridView _parentDGV = null;
            private IDGV_ListBoxItem _DGV_ListBoxItem = null;
            public DGV_ListBox(DataGridView parentDGV, IDGV_ListBoxItem DGV_ListBoxItem)
            {
                _ListBox = new ListBox();
                _DGV_ListBoxItem = DGV_ListBoxItem;
                this._ListBox.Visible = false;
                this._ListBox.DataSource = DGV_ListBoxItem.Items;
                _parentDGV = parentDGV;
                _parentDGV.Controls.Add(this._ListBox);
                _parentDGV.CellClick += _parentDGV_CellClick;
                this._ListBox.SelectedIndexChanged += DGV_ListBox_SelectedIndexChanged;
            }

            void _parentDGV_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                SetVisibility(sender, e);
            }

            void DGV_ListBox_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    this._ListBox.Visible = false;
                    if ((_parentDGV.CurrentCell.ColumnIndex > 0) && (_parentDGV.CurrentCell.RowIndex >-1))
                    {
                        _parentDGV.CurrentCell.Value = (_DGV_ListBoxItem.CastToType(this._ListBox.SelectedItem.ToString()));
                    }
                }
                catch
                {
                }
            }


            public enum DGV_ListBox_AssignMethod
            {
                Automatic,
                ColumnNr,
                ColumnType
            }


            private DGV_ListBox_AssignMethod _ListBox_AssignMethod = DGV_ListBox_AssignMethod.Automatic;

            public DGV_ListBox_AssignMethod ListBox_AssignMethod
            {
                get { return _ListBox_AssignMethod; }
                set { _ListBox_AssignMethod = value; }
            }


            private int _AssigntoColumnNumber = 0;

            /// <summary>
            /// assign on which column the listbbox must appear
            /// </summary>
            public int AssigntoColumnNumber
            {
                get { return _AssigntoColumnNumber; }
                set { _AssigntoColumnNumber = value; }
            }

            private string _AssignToColumnType = "";

            public string AssignToColumnType
            {
                get { return _AssignToColumnType; }
                set { _AssignToColumnType = value; }
            }


            public void SetVisibility(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    this._ListBox.Visible = false;
                    switch (ListBox_AssignMethod)
                    {
                        case DGV_ListBox_AssignMethod.Automatic:
                            {

                                if (((DataGridView)sender).Columns[e.ColumnIndex].ValueType.Name == _DGV_ListBoxItem.GetType().Name)
                                {
                                    CreateBox(sender, e);
                                }
                            }
                            break;

                        case DGV_ListBox_AssignMethod.ColumnNr:
                            {
                                if (e.ColumnIndex == _AssigntoColumnNumber)
                                {
                                    CreateBox(sender, e);
                                }
                            }
                            break;
                        case DGV_ListBox_AssignMethod.ColumnType:
                            {

                                if (((DataGridView)sender).Columns[e.ColumnIndex].ValueType.Name == _AssignToColumnType)
                                {
                                    CreateBox(sender, e);
                                }
                            }
                            break;
                    }
                }
                catch
                { }
            }


            private void CreateBox(object sender, DataGridViewCellEventArgs e)
            {
                int RowHeight1 = ((DataGridView)sender).Rows[e.RowIndex].Height;
                Rectangle CellRectangle1 = ((DataGridView)sender).GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                CellRectangle1.X += ((DataGridView)sender).Left;
                CellRectangle1.Y += ((DataGridView)sender).Top + RowHeight1;

                Point DisplayPoint1 = _ListBox.PointToScreen(new Point(CellRectangle1.X, CellRectangle1.Y));
                this._ListBox.Left = CellRectangle1.X;
                this._ListBox.Top = CellRectangle1.Y;
                this._ListBox.Width = CellRectangle1.Width;
                this._ListBox.BringToFront();
                this._ListBox.AutoSize = true;
                this._ListBox.Visible = true;
            }
        }

    }

    
}
