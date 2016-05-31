using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Factory;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Marb.Bindinglist;
using log4net;
using structures.Views.ListRefreshEngine;
using Marb.Extender.Datgridview;

namespace structures.Views
{
    public partial class UC_TornooiAdministratie : UserControl
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(UC_TornooiAdministratie));
        protected BindingListRefresh<Wedstrijd> _BindingListRefreshWedstrijd;
        //protected BindingList<Wedstrijd> _inputlist;
        

        //Wedstrijden
        private ActiveBindingList<Wedstrijd> _WedstrijdList;
        private ActiveBindingList<Terrein> _TerreinList;

        private BindingList<Wedstrijd> _fileteredWedstrijdReekslist;
        private BindingList<Wedstrijd> _fileteredWedstrijdUurlist;
        private BindingList<Terrein> _TerreinReeksList;

        //Reeks (ploegen + terreinen)
        private AdministratieReeks Reeks;

        //Datetimepicker 
        private DateTimePicker dtp;
        #region Constructor & load

        public UC_TornooiAdministratie(ActiveBindingList<Wedstrijd> WedstrijdList, ActiveBindingList<Terrein> TerreinList)
        {
            InitializeComponent();
            _WedstrijdList = WedstrijdList;
            _TerreinList = TerreinList;
            _WedstrijdList.ListChanged += _WedstrijdList_ListChanged;
            _TerreinList.ListChanged += _TerreinList_ListChanged;


            _BindingListRefreshWedstrijd = new BindingListRefresh<Wedstrijd>(_WedstrijdList);
            _BindingListRefreshWedstrijd.ListRefreshed += _BindingListRefreshWedstrijd_ListRefreshed;



            if (_WedstrijdList.Count > 0)
            {
                PopulateReeksCombobox();
                cmb_ReeksNaam.SelectedIndex = 0;
            }
            
            dtp = new DateTimePicker();
            
            dtp.Validated += dtp_ValueChanged;
            dtp.KeyPress += dtp_KeyPress;


            dtp.Format = DateTimePickerFormat.Time;
            //dtp.ShowUpDown = true;
            dtp.Visible = false;
            panel1.Controls.Add(dtp);
        }

        void _BindingListRefreshWedstrijd_ListRefreshed()
        {
            RefreshList();
        }

        void dtp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DateTime OldValue = (DateTime)dgv_Wedstrijden.CurrentCell.Value;
                DateTime NewValue = dtp.Value;
                TimeSpan delta = NewValue.Subtract(OldValue);

                dgv_Wedstrijden.CurrentCell.Value = dtp.Value;
                dtp.Visible = false;

                //Change time of all upcoming games
                for (int i = dgv_Wedstrijden.CurrentCell.RowIndex + 1; i < dgv_Wedstrijden.Rows.Count; i++)
                {
                    DateTime time = (DateTime)dgv_Wedstrijden.Rows[i].Cells[0].Value;
                    time = time.Add(delta);
                    dgv_Wedstrijden.Rows[i].Cells[0].Value = time;

                }

                PopulateUurCombobox();

            }
        }

        void _TerreinList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                for (int i = 0; i < _TerreinReeksList.Count; i++)
                {

                    if (_TerreinReeksList[i].Status)
                    {
                        dgv_Terreinen.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Green;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                    else
                    {
                        dgv_Terreinen.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Red;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionForeColor = Color.White;
                    }

                }





            }




        }

        void _WedstrijdList_ListChanged(object sender, ListChangedEventArgs e)
        {

            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                ChangeRowColor();

            }
            else
            {
                if (_WedstrijdList.Count == 0)
                {
                    dgv_Terreinen.DataSource = null;
                    dgv_Klassement.DataSource = null;
                    dgv_Wedstrijden.DataSource = null;
                }

                
                cmb_ReeksNaam.Items.Clear();
                cmb_ReeksNaam.Text = "";
                PopulateReeksCombobox();

            }


            
          
        }






        private void UC_TornooiAdministratie_Load(object sender, EventArgs e)
        {
            if (_WedstrijdList.Count > 0)
            {
                DisableRows(0);
                ChangeRowColor();
                UpdateTerreinen();
            }
            else
            {
                dgv_Terreinen.DataSource = null;
            }

            dgv_Terreinen.DoubleBuffered(true);
            dgv_Klassement.DoubleBuffered(true);
            dgv_Wedstrijden.DoubleBuffered(true);

            dgv_Wedstrijden.CellBeginEdit += dgv_Wedstrijden_CellBeginEdit;
            dgv_Wedstrijden.CellEndEdit += dgv_Wedstrijden_CellEndEdit;

            _BindingListRefreshWedstrijd.StartRefreshing();
        }


        #region refresh data

        void extendDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _BindingListRefreshWedstrijd.StopRefreshing();
        }

        void extendDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _BindingListRefreshWedstrijd.StartRefreshing();
        }

        void _BindingListRefresh_ListRefreshed()
        {
            RefreshList();
        }

        private void btn_refreshlist_Click(object sender, EventArgs e)
        {
            RefreshList();

        }
        private void RefreshList()
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                //_BindingListRefreshWedstrijd.RefreshList();
                dgv_Wedstrijden.Refresh();
                dgv_Wedstrijden.Update();
            }
        }


        #endregion

        void UC_TornooiAdministratie_Leave(object sender, EventArgs e)
        {
           
        }

        void UC_TornooiAdministratie_Enter(object sender, EventArgs e)
        {
            
        }


        #endregion

        #region Comboboxes

        private void PopulateReeksCombobox()
        {
            //List<string> ReeksNamen = new List<string>();
            List<string> ReeksNamen = new List<string>();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if (!ReeksNamen.Contains(w.ReeksNaam))
                {
                    ReeksNamen.Add(w.ReeksNaam);
                }

            }
            cmb_ReeksNaam.Items.Clear();
            foreach (string s in ReeksNamen)
            {
                cmb_ReeksNaam.Items.Add(s);
            }

            cmb_ReeksNaam.Width = DropDownWidth(cmb_ReeksNaam);




        }

        private void PopulateUurCombobox()
        {
            List<string> Aanvangsuren = new List<string>();
            Aanvangsuren.Clear();
            foreach (Wedstrijd w in _fileteredWedstrijdReekslist)
            {
                if (!Aanvangsuren.Contains(w.Aanvangsuur.ToString()))
                {
                    Aanvangsuren.Add(w.Aanvangsuur.ToString());
                }
            }

            cmb_Aanvangsuur.Items.Clear();
            cmb_Aanvangsuur.Items.Add("Alle wedstrijden");
            foreach (string s in Aanvangsuren)
            {
                cmb_Aanvangsuur.Items.Add(s);
            }

            cmb_Aanvangsuur.Width = DropDownWidth(cmb_Aanvangsuur);

        }

        private int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            return maxWidth + 20;
        }

        //Events
        private void cmb_ReeksNaam_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fileteredWedstrijdReekslist = new BindingList<Wedstrijd>(_WedstrijdList.Where(x => x.ReeksNaam == cmb_ReeksNaam.Text).ToList());
            _TerreinReeksList = new BindingList<Terrein>(_TerreinList.Where(x => x.ReeksNaam == cmb_ReeksNaam.Text).ToList());
            PopulateUurCombobox();

            //
            cmb_Aanvangsuur.SelectedIndex = 0;
            CreateAdministratieReeks();
            UpdateKlassement();
            UpdateTerreinen();

            DisableRows(0);
            ChangeRowColor();


        }

        private void cmb_Aanvangsuur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Aanvangsuur.Text == "Alle wedstrijden")
            {
                _fileteredWedstrijdUurlist = _fileteredWedstrijdReekslist;
            }
            else
            {
                _fileteredWedstrijdUurlist = new BindingList<Wedstrijd>(_fileteredWedstrijdReekslist.Where(x => x.Aanvangsuur.ToString() == cmb_Aanvangsuur.Text).ToList());
            }
            UpdateWedstrijden();
            ChangeRowColor();
            DisableRows(0);
        }

        #endregion

        #region Datagridviews

        private void dgv_Wedstrijden_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Wedstrijden.CurrentCell.ColumnIndex != 0)
            {
                //Update het klassement
                UpdateKlassement();

                //Disablez row als terrein niet vrij is
                DisableRows(0);

                //Update alledatagrids
                UpdateAllDataGrids();
            }
        }

        private void dgv_Wedstrijden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Make changes in database
            dgv_Wedstrijden.EndEdit();


            //Print wedstrijdblad indien wedstrijd gestart wordt
            //int index = dgv_Wedstrijden.Columns["IsBusy"].Index;
            //if (e.ColumnIndex == index && _fileteredWedstrijdUurlist[e.RowIndex].IsBusy == true && _fileteredWedstrijdUurlist[e.RowIndex].Isplayed == false)
            //{
            //    CreateGameSheet(e.RowIndex);
            //}

            
            ChangeRowColor();

        }

        private void dgv_Terreinen_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            int index = dgv_Terreinen.Columns["Status"].Index;
            DataGridViewCheckBoxCell c = (DataGridViewCheckBoxCell)dgv_Terreinen.Rows[e.RowIndex].Cells[index];
            if ((bool)c.FormattedValue)
            {
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Green;
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                dgv_Terreinen.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
            }



            UpdateAllDataGrids();
        }

        #endregion

        #region Buttons
        private void btn_lock_Click(object sender, EventArgs e)
        {
            cmb_ReeksNaam.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (cmb_Aanvangsuur.SelectedIndex < cmb_Aanvangsuur.Items.Count - 1)
            {
                cmb_Aanvangsuur.SelectedIndex++;
                if (cmb_Aanvangsuur.SelectedIndex == 1)
                {
                    btn_previousRound.Text = "Alle Wedstrijden";
                }
                else if (cmb_Aanvangsuur.SelectedIndex == cmb_Aanvangsuur.Items.Count - 1)
                {
                    btn_nextRound.Text = "Einde";
                }
                else
                {
                    btn_previousRound.Text = "Vorige Ronde";
                    btn_nextRound.Text = "Volgende Ronde";
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (cmb_Aanvangsuur.SelectedIndex > 0)
            {
                cmb_Aanvangsuur.SelectedIndex--;
                if (cmb_Aanvangsuur.SelectedIndex == 1 || cmb_Aanvangsuur.SelectedIndex == 0)
                {
                    btn_previousRound.Text = "Alle Wedstrijden";
                }
                else
                {
                    btn_previousRound.Text = "Vorige Ronde";
                    btn_nextRound.Text = "Volgende Ronde";
                }
            }
        }



        #endregion

        #region Update routines

        private void UpdateWedstrijden()
        {

            dgv_Wedstrijden.DataSource = _fileteredWedstrijdUurlist;
            foreach (DataGridViewColumn column in dgv_Wedstrijden.Columns)
            {
                if (column.Index <= 5)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            dgv_Wedstrijden.Columns["IsStarted"].ReadOnly = true;

            if (cmb_Aanvangsuur.SelectedIndex == 0)
            {
                btn_previousRound.Enabled = false;
                btn_nextRound.Enabled = true;
            }
            else if (cmb_Aanvangsuur.SelectedIndex == cmb_Aanvangsuur.Items.Count - 1)
            {
                btn_nextRound.Enabled = false;
                btn_previousRound.Enabled = true;
            }
            else
            {
                btn_previousRound.Enabled = true;
                btn_nextRound.Enabled = true;
            }



        }

        private void UpdateKlassement()
        {
            Reeks.CalculateRankings();
            dgv_Klassement.DataSource = Reeks.Klassement.Ranking;
            foreach (DataGridViewColumn column in dgv_Klassement.Columns)
            {
                if (column.Index == 0)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }


            foreach (DataGridViewRow row in dgv_Klassement.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString() + ".";
            }
            dgv_Klassement.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;


        }

        private void UpdateTerreinen()
        {
            if (_TerreinReeksList != null)
            {
                dgv_Terreinen.DataSource = _TerreinReeksList;
                foreach (DataGridViewColumn column in dgv_Terreinen.Columns)
                {

                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }


                for (int i = 0; i < _TerreinReeksList.Count; i++)
                {
                    if (_TerreinReeksList[i].Status == true)
                    {
                        dgv_Terreinen.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Green;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                    else
                    {
                        dgv_Terreinen.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Red;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionForeColor = Color.White;

                    }

                }
            }

        }

        private void UpdateAllDataGrids()
        {
            //dgv_Klassement.EndEdit();
            //dgv_Terreinen.EndEdit();
            //dgv_Wedstrijden.EndEdit();

            dgv_Terreinen.Refresh();
            dgv_Wedstrijden.Refresh();
            dgv_Klassement.Refresh();
        }

        #endregion



        #region Methods

        private void CreateAdministratieReeks()
        {
            Reeks = new AdministratieReeks(cmb_ReeksNaam.Text, _fileteredWedstrijdReekslist);

            foreach (Wedstrijd w in _fileteredWedstrijdReekslist)
            {
                if (!Reeks.ReeksTerreinen.Contains(w.Terrein))
                {
                    Reeks.ReeksTerreinen.Add(w.Terrein);
                }
            }

            foreach (Wedstrijd w in _fileteredWedstrijdReekslist)
            {
                if (!Reeks.ReeksPloegen.Contains(w.Home))
                {
                    Reeks.ReeksPloegen.Add(w.Home);
                }
                if (!Reeks.ReeksPloegen.Contains(w.Away))
                {
                    Reeks.ReeksPloegen.Add(w.Away);
                }
            }


        }

        private void DisableRows(int p)
        {
            for (int i = p; i < dgv_Wedstrijden.Rows.Count; i++)
            {
                if (!_fileteredWedstrijdUurlist[i].Terrein.Status && !_fileteredWedstrijdUurlist[i].IsBusy)
                {
                    dgv_Wedstrijden.Rows[i].ReadOnly = true;
                }
                else if (_fileteredWedstrijdUurlist[i].Isplayed)
                {
                    dgv_Wedstrijden.Rows[i].Cells["IsBusy"].ReadOnly = true;
                    dgv_Wedstrijden.Rows[i].Cells["Isplayed"].ReadOnly = true;
                }
                else
                {
                    dgv_Wedstrijden.Rows[i].ReadOnly = false;
                }
            }
        }

        private void ChangeRowColor()
        {
            //Change color of played games
            for (int i = 0; i < _fileteredWedstrijdUurlist.Count; i++)
            {
                if (_fileteredWedstrijdUurlist[i].Isplayed == true)
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                }
                else if (_fileteredWedstrijdUurlist[i].IsStarted == true)
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (_fileteredWedstrijdUurlist[i].IsBusy == true)
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.Coral;
                }
                else
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

            }
        }


        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       




        









        #region Printing

        private void CreateGameSheet(int index)
        {
            try
            {
                Assembly _assembly;
                StreamReader objReader;

                //Read the complete file and replace the text
                _assembly = Assembly.GetExecutingAssembly();
                //string[] rel = _assembly.GetManifestResourceNames();

                objReader = new StreamReader(_assembly.GetManifestResourceStream("structures.Printing.gameSheet.html"));




                string content = objReader.ReadToEnd();
                objReader.Close();

                //Replace the text
                Wedstrijd w = _fileteredWedstrijdUurlist[index];

                string home = w.Home.ToString();
                content = content.Replace("[HOME]", home);

                string away = w.Away.ToString();
                content = content.Replace("[AWAY]", away);

                string title = home + " - " + away;
                content = content.Replace("[TITLE]", title);



                string terrein = w.Terrein.ToString();
                content = content.Replace("[TERREIN]", terrein);

                string scheidsrechter = w.Scheidsrechter.ToString();
                content = content.Replace("[SCHEIDSRECHTER]", scheidsrechter);

                string reeks = w.ReeksNaam;
                content = content.Replace("[REEKS]", reeks);

                string uur = w.Aanvangsuur.ToShortTimeString();
                content = content.Replace("[AANVANGSUUR]", uur);





                //Write content to new html-file
                StreamWriter writer = new StreamWriter(("gameSheet_adj.html"));
                writer.Write(content);
                writer.Close();

                //Open HTML file 
                Process.Start("gameSheet_adj.html");



            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void dgv_Wedstrijden_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CreateGameSheet(e.RowIndex);
        }

        private void dgv_Wedstrijden_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
            Assembly _assembly;

            //Read the complete file and replace the text
            _assembly = Assembly.GetExecutingAssembly();
            //string[] rel = _assembly.GetManifestResourceNames();
            Bitmap myBitmap = new Bitmap(_assembly.GetManifestResourceStream("structures.Printing.printerIcon.png"));
            Icon myIcon = Icon.FromHandle(myBitmap.GetHicon());

            Graphics graphics = e.Graphics;

            //Set Image dimension - User's choice
            int iconHeight = 14;
            int iconWidth = 14;

            //Set x/y position - As the center of the RowHeaderCell
            int xPosition = e.RowBounds.X + (dgv_Wedstrijden.RowHeadersWidth / 2);
            int yPosition = e.RowBounds.Y +
            ((dgv_Wedstrijden.Rows[e.RowIndex].Height - iconHeight) / 2);

            Rectangle rectangle = new Rectangle(xPosition, yPosition, iconWidth, iconHeight);
            graphics.DrawIcon(myIcon, rectangle);
            }
            catch (Exception ee)
            {
                
            }


        }

        #endregion

        #region Change Aanvangsuur

        private void dgv_Wedstrijden_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Wedstrijden.CurrentCell.ColumnIndex == 0 && e.RowIndex !=-1 && e.ColumnIndex!=-1)
            {
                dtp.Location = dgv_Wedstrijden.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                dtp.Width = dgv_Wedstrijden.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                dtp.Visible = true;
                dtp.BringToFront();
                DateTime date = (DateTime)dgv_Wedstrijden.CurrentCell.Value;
                dtp.Value = date;
            }
            else
            {
                dtp.Visible = false;
            }
        }

        private void dgv_Wedstrijden_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Wedstrijden.CurrentCell.ColumnIndex == 0)
            {
                dtp.Location = dgv_Wedstrijden.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                dtp.Width = dgv_Wedstrijden.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                dtp.Visible = true;
                dtp.BringToFront();
                DateTime date = (DateTime)dgv_Wedstrijden.CurrentCell.Value;
                dtp.Value = date;
            }
        }

        void dtp_ValueChanged(object sender, EventArgs e)
        {
            DateTime OldValue = (DateTime)dgv_Wedstrijden.CurrentCell.Value;
            DateTime NewValue = dtp.Value;
            TimeSpan delta = NewValue.Subtract(OldValue);

            dgv_Wedstrijden.CurrentCell.Value = dtp.Value;
            dtp.Visible = false;

            //Change time of all upcoming games
            for (int i = dgv_Wedstrijden.CurrentCell.RowIndex + 1; i < dgv_Wedstrijden.Rows.Count; i++)
            {
                DateTime time = (DateTime) dgv_Wedstrijden.Rows[i].Cells[0].Value;
                time = time.Add(delta);
                dgv_Wedstrijden.Rows[i].Cells[0].Value = time;

            }

            PopulateUurCombobox();


        }
        #endregion





        #region Refresh data
        private void UC_TornooiAdministratie_Enter_1(object sender, EventArgs e)
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                if (_BindingListRefreshWedstrijd.AllowDataRefresh)
                {
                    _BindingListRefreshWedstrijd.StartRefreshing();
                }
            }
        }

        private void UC_TornooiAdministratie_Leave_1(object sender, EventArgs e)
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.StopRefreshing();
            }
        }

        void dgv_Wedstrijden_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _BindingListRefreshWedstrijd.StartRefreshing();
        }

        void dgv_Wedstrijden_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _BindingListRefreshWedstrijd.StopRefreshing();
        }








        #endregion


        /*
        private void dgv_Terreinen_DataSourceChanged(object sender, EventArgs e)
        {
            int index = dgv_Terreinen.Columns["Status"].Index;
            DataGridViewCheckBoxCell c = (DataGridViewCheckBoxCell)dgv_Terreinen.Rows[0].Cells[index];

            for (int i = 0; i < dgv_Terreinen.Rows.Count; i++)
            {
                if ((bool)c.FormattedValue)
                {
                    dgv_Terreinen.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Green;
                    dgv_Terreinen.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else
                {
                    dgv_Terreinen.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgv_Terreinen.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    dgv_Terreinen.Rows[i].DefaultCellStyle.SelectionForeColor = Color.White;
                }

            }








        }

        */



















    }
}
