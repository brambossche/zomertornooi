using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory;
using log4net;
using NHibernate;
using NhibernateIntf;
using ProgramDefinitions;
using structures;
using TestProjectNhibernate.TestStructure;

namespace TestProjectNhibernate
{
    public partial class TestForm : Form
    {

        private DatabaseSetup<TestForm> _DatabaseSetup;
        private ISession _Session;
        private DataAccessLayer _DataAccessLayer = null;
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestForm));
        private ActiveBindingList<TopClass> _TopClassList;
        private ActiveBindingList<SubClass> _SubClassList;
        private ProgramLogger _ProgramLogger;
        public TestForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _ProgramLogger = new ProgramLogger();
            _DatabaseSetup = new DatabaseSetup<TestForm>(Databaseconfig.DB_UnitHibernateTest);
            //_Session = _DatabaseSetup.session;
            _DataAccessLayer = new DataAccessLayer(_Session);
            _TopClassList = new ActiveBindingList<TopClass>(_DataAccessLayer);
            dataGridView1.DataSource = _TopClassList;
        }
    }
}
