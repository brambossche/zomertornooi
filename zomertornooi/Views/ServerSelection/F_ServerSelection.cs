using FluentNHibernate.Cfg.Db;
using ProgramDefinitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Views;

namespace structures.Views.ServerSelection
{
    public partial class F_ServerSelection : Form
    {
        
        public F_ServerSelection()
        {
            InitializeComponent();
        }

        private void F_ServerSelection_Load(object sender, EventArgs e)
        {
            cmb_UserType.Items.Clear();
            cmb_UserType.DataSource = Enum.GetValues(typeof(UserLevel));
        }

        private void cmb_server_Enter(object sender, EventArgs e)
        {
            cmb_server.Items.Clear();
            string[] _servers = SqlLocator.GetServers();
            cmb_server.Items.AddRange(_servers);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if(cmb_server.Text == "")
            {
                this.Dispose();
                BaseForm Tornooi = new BaseForm((UserLevel)cmb_UserType.SelectedItem);
                Tornooi.ShowDialog();
            }
            else
            {
                Databaseconfig.DB_UnitHibernateTest = MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=" + cmb_server.Text + "\\SQLEXPRESS,2016;Initial Catalog=ZomerTornooi2016;User ID=TornooiUser;Password=Zomertornooi2016");
                this.Dispose();
                BaseForm Tornooi = new BaseForm((UserLevel)cmb_UserType.SelectedItem);
                Tornooi.ShowDialog();
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }







    }
}
