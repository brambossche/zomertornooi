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

        //Input lists
        private ActiveBindingList<Wedstrijd> _WedstrijdList;
        private ActiveBindingList<Terrein> _TerreinList;

        //Bindinglists for update
        protected BindingListRefresh<Wedstrijd> _BindingListRefreshWedstrijd;
        protected BindingListRefresh<Terrein> _BindingListRefreshTerrein;


        //Reeks (ploegen + terreinen)
        private AdministratieReeks Reeks;

        //Datetimepicker
        private DateTimePicker dtp;



        public UC_TornooiAdministratie(ActiveBindingList<Wedstrijd> WedstrijdList, ActiveBindingList<Terrein> TerreinList)
        {
            InitializeComponent();
            //input Lists
            _WedstrijdList = WedstrijdList;
            _TerreinList = TerreinList;

            //Refresh lists
            _BindingListRefreshWedstrijd = new BindingListRefresh<Wedstrijd>(_WedstrijdList);
            _BindingListRefreshTerrein = new BindingListRefresh<Terrein>(_TerreinList);

            //Input list change events
            _WedstrijdList.ListChanged += _WedstrijdList_ListChanged;
            _TerreinList.ListChanged += _TerreinList_ListChanged;

            //Refreshlist events
            //_BindingListRefreshWedstrijd.ListRefreshed += _BindingListRefreshWedstrijd_ListRefreshed;
            //_BindingListRefreshTerrein.ListRefreshed += _BindingListRefreshTerrein_ListRefreshed;

            if (_WedstrijdList.Count > 0 && _TerreinList.Count > 0)
            {
                InitTornooiAdministratie();
            }

           
        }

        private void UC_TornooiAdministratie_Load(object sender, EventArgs e)
        {
            dgv_Terreinen.DoubleBuffered(true);
            dgv_Klassement.DoubleBuffered(true);
            dgv_Wedstrijden.DoubleBuffered(true);

            _BindingListRefreshTerrein.StartRefreshing();
            _BindingListRefreshWedstrijd.StartRefreshing();

            if (_WedstrijdList.Count > 0 && _TerreinList.Count > 0)
            {
                UpdateTerreinDGV();
                UpdateTerreinColors();
                UpdateWedstrijdColors();
            }
            



        }

        private void InitTornooiAdministratie()
        {
            //Init Datetimpicker
            dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Time;
            //dtp.ShowUpDown = true;
            dtp.Visible = false;
            dtp.VisibleChanged += dtp_VisibleChanged;
            panel1.Controls.Add(dtp);
            dtp.KeyPress += dtp_KeyPress;

            //Assign Datasource of datagridviews
            dgv_Terreinen.DataSource = _TerreinList;
            dgv_Terreinen.Columns["ReeksNaam"].Visible = false;
            dgv_Wedstrijden.DataSource = _WedstrijdList;

            //Populate cmb_reeksnaam
            PopulateReeksCombobox();
        }

        void dtp_VisibleChanged(object sender, EventArgs e)
        {
            if (dtp.Visible == true)
            {
                _BindingListRefreshTerrein.StopRefreshing();
                _BindingListRefreshWedstrijd.StopRefreshing();
            }
            else
            {
                _BindingListRefreshTerrein.StartRefreshing();
                _BindingListRefreshWedstrijd.StartRefreshing();
            }
        }

        void _TerreinList_ListChanged(object sender, ListChangedEventArgs e)
        {

            if (_TerreinList.Count > 0 & _TerreinList != null)
            {
               // UpdateTerreinColors();
            }

            



        }


        private void UpdateTerreinColors()
        {
            for(int i=0;i<dgv_Terreinen.Rows.Count;i++)
            {
                Terrein t = dgv_Terreinen.Rows[i].DataBoundItem as Terrein;
                if (t.Status == true)
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

        private void UpdateWedstrijdColors()
        {
            for (int i = 0; i < dgv_Wedstrijden.Rows.Count; i++)
            {
                Wedstrijd w = dgv_Wedstrijden.Rows[i].DataBoundItem as Wedstrijd;


                if (w.Isplayed == true)
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                }
                else if (w.IsStarted == true)
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (w.IsBusy == true)
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.Coral;
                }
                else
                {
                    dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }

            //UpdateWedstrijdStatus();

        }

        private void UpdateWedstrijdStatus()
        {
            CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
            WedstrijdManager.SuspendBinding();
            foreach (DataGridViewRow r in dgv_Wedstrijden.Rows)
            {
                Wedstrijd w = r.DataBoundItem as Wedstrijd;
                if (!w.Terrein.Status && w.IsBusy)
                {
                    r.ReadOnly = true;
                }
                else
                {
                    r.ReadOnly = false;
                }
            }
            WedstrijdManager.ResumeBinding();
        }

        void _WedstrijdList_ListChanged(object sender, ListChangedEventArgs e)
        {

            if (_WedstrijdList.Count > 0 & _WedstrijdList != null && _TerreinList.Count !=0 && _TerreinList != null)
            {                
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    UpdateKlassement();
                    UpdateWedstrijdColors();
                }
                else if (e.ListChangedType == ListChangedType.Reset)
                {
                    InitTornooiAdministratie();
                    cmb_ReeksNaam.SelectedIndex = 0;
                }
            }
            else
            {
                dgv_Klassement.DataSource = null;
                dgv_Terreinen.DataSource = null;
                dgv_Wedstrijden.DataSource = null;
                cmb_Aanvangsuur.Items.Clear();
                cmb_Aanvangsuur.Text = "";
                cmb_ReeksNaam.Items.Clear();
                cmb_ReeksNaam.Text = "";
            }


            //Reeks = new AdministratieReeks(cmb_ReeksNaam.Text, _WedstrijdList);
            //_BindingListRefreshTerrein.RefreshList();


        }

        #region Comboboxes

        private void PopulateReeksCombobox()
        {
            //Populate cmb_reeksnaam
            List<string> Reeksen = new List<string>();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.RoundRobin && !Reeksen.Contains(w.ReeksNaam))
                {
                    Reeksen.Add(w.ReeksNaam);
                }
            }
            cmb_ReeksNaam.Items.Clear();
            cmb_ReeksNaam.Items.AddRange(Reeksen.ToArray());
            cmb_ReeksNaam.Width = DropDownWidth(cmb_ReeksNaam);
            cmb_ReeksNaam.SelectedIndex = 0;
        } 
    
        private void PopulateUurCombobox()
        {
            int currentIndex = cmb_Aanvangsuur.SelectedIndex;
            List<string> Aanvangsuren = new List<string>();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if (!Aanvangsuren.Contains(w.Aanvangsuur.ToString()) && w.ReeksNaam == cmb_ReeksNaam.Text)
                {
                    Aanvangsuren.Add(w.Aanvangsuur.ToString());
                }
            }
            cmb_Aanvangsuur.Items.Clear();
            cmb_Aanvangsuur.Items.Add("Alle wedstrijden");
            cmb_Aanvangsuur.Items.AddRange(Aanvangsuren.ToArray());
            cmb_Aanvangsuur.SelectedIndex = currentIndex;
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
            CurrencyManager TerreinManager = (CurrencyManager)dgv_Terreinen.BindingContext[dgv_Terreinen.DataSource];
            TerreinManager.SuspendBinding();
            for (int i = 0; i < _TerreinList.Count; i++)
            {
                if (_TerreinList[i].ReeksNaam == cmb_ReeksNaam.Text)
                {
                    dgv_Terreinen.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Terreinen.Rows[i].Visible = false;
                }
            }
            TerreinManager.ResumeBinding();

            List<string> Aanvangsuren = new List<string>();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if (!Aanvangsuren.Contains(w.Aanvangsuur.ToString()) && w.ReeksNaam == cmb_ReeksNaam.Text)
                {
                    Aanvangsuren.Add(w.Aanvangsuur.ToString());
                }
            }
            cmb_Aanvangsuur.Items.Clear();
            cmb_Aanvangsuur.Items.Add("Alle wedstrijden");
            cmb_Aanvangsuur.Items.AddRange(Aanvangsuren.ToArray());
            cmb_Aanvangsuur.Width = DropDownWidth(cmb_Aanvangsuur);
            cmb_Aanvangsuur.SelectedIndex = 0;
        }

        private void cmb_Aanvangsuur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Aanvangsuur.SelectedIndex == 0)
            {
                CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
                WedstrijdManager.SuspendBinding();
                for (int i = 0; i < _WedstrijdList.Count; i++)
                {
                    if (_WedstrijdList[i].ReeksNaam == cmb_ReeksNaam.Text)
                    {
                        dgv_Wedstrijden.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Wedstrijden.Rows[i].Visible = false;
                    }
                }
                WedstrijdManager.ResumeBinding();
            }
            else
            {
                CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
                WedstrijdManager.SuspendBinding();
                for (int i = 0; i < _WedstrijdList.Count; i++)
                {
                    if (_WedstrijdList[i].ReeksNaam == cmb_ReeksNaam.Text && _WedstrijdList[i].Aanvangsuur.ToString() == cmb_Aanvangsuur.Text)
                    {
                        dgv_Wedstrijden.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Wedstrijden.Rows[i].Visible = false;
                    }
                }
                WedstrijdManager.ResumeBinding();
            }
            UpdateKlassement();
            btn_status();
        }

        #endregion

        #region Buttons
        private void btn_lock_Click(object sender, EventArgs e)
        {
            cmb_ReeksNaam.Enabled = false;
            InitTerreinen();
            InitWedstrijden();
        }

        private void btn_nextRound_Click(object sender, EventArgs e)
        {
            _WedstrijdList[0].Terrein.Status = false;


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

        private void btn_previousRound_Click(object sender, EventArgs e)
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

        private void btn_status()
        {
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

        #endregion

        #region Update routines

        private void InitWedstrijden()
        {
            CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
            WedstrijdManager.SuspendBinding();
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
            WedstrijdManager.ResumeBinding();
        }

        private void InitTerreinen()
        {
            if (_TerreinList != null)
            {
                CurrencyManager TerreinManager = (CurrencyManager)dgv_Terreinen.BindingContext[dgv_Terreinen.DataSource];
                TerreinManager.SuspendBinding();
                foreach (DataGridViewColumn column in dgv_Terreinen.Columns)
                {

                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
                dgv_Terreinen.Columns["ReeksNaam"].Visible = false;

                TerreinManager.ResumeBinding();

                /*
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

                }*/
            }

        }

        private void UpdateKlassement()
        {
            Reeks = new AdministratieReeks(cmb_ReeksNaam.Text, _WedstrijdList);
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

        private void UpdateAllDataGrids()
        {
            dgv_Terreinen.Refresh();
            dgv_Terreinen.Update();
            dgv_Wedstrijden.Refresh();
            dgv_Wedstrijden.Update();
            dgv_Klassement.Refresh();
            dgv_Klassement.Update();
        }

        #endregion


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
                Wedstrijd w = _WedstrijdList[index];

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
        {/*
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
            }*/
            dtp.Visible = false;
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

        void dtp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DateTime OldValue = (DateTime)dgv_Wedstrijden.CurrentCell.Value;
                DateTime NewValue = dtp.Value;
                TimeSpan delta = NewValue.Subtract(OldValue);
                dgv_Wedstrijden.CurrentCell.Value = dtp.Value;

                for (int i = 0; i < dgv_Wedstrijden.Rows.Count; i++)
                {
                    Wedstrijd w = dgv_Wedstrijden.Rows[i].DataBoundItem as Wedstrijd;
                    if (w.ReeksNaam == cmb_ReeksNaam.Text && w.Aanvangsuur > dtp.Value)
                    {
                        DateTime currentValue = (DateTime)dgv_Wedstrijden.Rows[i].Cells[0].Value;
                        dgv_Wedstrijden.Rows[i].Cells[0].Value = currentValue.Add(delta);
                    }

                }

                PopulateUurCombobox();

                /*
                    foreach (DataGridViewRow r in dgv_Wedstrijden.Rows)
                    {
                        Wedstrijd w = r.DataBoundItem as Wedstrijd;
                        if (r.Visible == true && (DateTime)r.Cells[0].Value > dtp.Value)
                        {
                            DateTime currentValue = (DateTime)r.Cells[0].Value;
                            r.Cells[0].Value = currentValue.Add(delta);
                        }
                    }*/


                dtp.Visible = false;
            }
        }



        #endregion

        #region refresh data

        void _BindingListRefreshTerrein_ListRefreshed()
        {
            RefreshList();
            UpdateTerreinColors();
        }

        void _BindingListRefreshWedstrijd_ListRefreshed()
        {
            RefreshList();
            UpdateKlassement();
            UpdateWedstrijdColors();
        }

        private void RefreshList()
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.RefreshList();
            }

            if (_BindingListRefreshTerrein != null)
            {
                _BindingListRefreshTerrein.RefreshList();
            }
            UpdateAllDataGrids();
            UpdateWedstrijdColors();
            UpdateTerreinColors();
        }

        private void UC_TornooiAdministratie_Enter(object sender, EventArgs e)
        {
            RefreshList();
            if (dgv_Terreinen.DataSource != null)
            {
                CurrencyManager TerreinManager = (CurrencyManager)dgv_Terreinen.BindingContext[dgv_Terreinen.DataSource];
                TerreinManager.SuspendBinding();
                for (int i = 0; i < _TerreinList.Count; i++)
                {
                    if (_TerreinList[i].ReeksNaam == cmb_ReeksNaam.Text)
                    {
                        dgv_Terreinen.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Terreinen.Rows[i].Visible = false;
                    }
                }
                TerreinManager.ResumeBinding();

            }

            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.StartRefreshing();

            }

            if (_BindingListRefreshTerrein != null)
            {
                _BindingListRefreshTerrein.StartRefreshing();
            }
        }

        private void UC_TornooiAdministratie_Leave(object sender, EventArgs e)
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.StopRefreshing();
            }

            if (_BindingListRefreshTerrein != null)
            {
                _BindingListRefreshTerrein.StopRefreshing();
            }
        }

        private void dgv_Wedstrijden_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _BindingListRefreshTerrein.StopRefreshing();
            _BindingListRefreshWedstrijd.StopRefreshing();
        }

        private void dgv_Wedstrijden_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _BindingListRefreshTerrein.StartRefreshing();
            _BindingListRefreshWedstrijd.StartRefreshing();
        }

        private void dgv_Terreinen_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _BindingListRefreshTerrein.StopRefreshing();
            _BindingListRefreshWedstrijd.StopRefreshing();
        }

        private void dgv_Terreinen_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _BindingListRefreshTerrein.StartRefreshing();
            _BindingListRefreshWedstrijd.StartRefreshing();
        }

        private void dgv_Wedstrijden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 12)
            {
                dgv_Wedstrijden.EndEdit();
                //RefreshList();
            }

        }


        #endregion



        private void UpdateTerreinDGV()
        {
            CurrencyManager TerreinManager = (CurrencyManager)dgv_Terreinen.BindingContext[dgv_Terreinen.DataSource];
            TerreinManager.SuspendBinding();
            for (int i = 0; i < _TerreinList.Count; i++)
            {
                if (_TerreinList[i].ReeksNaam == cmb_ReeksNaam.Text)
                {
                    dgv_Terreinen.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Terreinen.Rows[i].Visible = false;
                }
            }
            TerreinManager.ResumeBinding();

            List<string> Aanvangsuren = new List<string>();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if (!Aanvangsuren.Contains(w.Aanvangsuur.ToString()) && w.ReeksNaam == cmb_ReeksNaam.Text)
                {
                    Aanvangsuren.Add(w.Aanvangsuur.ToString());
                }
            }
            cmb_Aanvangsuur.Items.Clear();
            cmb_Aanvangsuur.Items.Add("Alle wedstrijden");
            cmb_Aanvangsuur.Items.AddRange(Aanvangsuren.ToArray());
            cmb_Aanvangsuur.Width = DropDownWidth(cmb_Aanvangsuur);
            cmb_Aanvangsuur.SelectedIndex = 0;
        }




    }
}
