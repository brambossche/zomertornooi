using System;
using System.Windows.Forms;
using Factory;
using NHibernate;
using ProgramDefinitions;
using WeifenLuo.WinFormsUI.Docking;
using log4net;
using structures;
using System.IO;
using structures.Views;
using Marb.ExternalProcesses;
using Marb.Bindinglist;
using structures.structures;
using NhibernateIntf;

namespace Views
{

    public enum UserLevel
    {
        Admin, 
        User,
        Reader
    }

    

    public partial class BaseForm : Form
    {
        private NHibernateSessionManager<BaseForm> _NHibernateSessionManager;
        private StructureSetup _StructureSetup;
        private ActiveBindingList<Ploeg> _PloegList;
        private ExtBindingList<Terrein> _TerreinList;
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseForm));
        public static UserLevel _userlevel = UserLevel.Admin;
        private QueStatus _QueStatus;
        public BaseForm(UserLevel userlevel = UserLevel.Admin)
        {

            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                logger.Fatal(this.Name, e);
            }
            logger.Info(this.Name + " passed");
            _userlevel = userlevel;

            _QueStatus = new QueStatus();
            splitContainer1.Panel2.Controls.Add(_QueStatus);
            _QueStatus.Questatus = false;
        }
        



        private void BaseForm_Load(object sender, EventArgs e)
        {

            try
            {

                //connect to the database and get the Factory session
                _NHibernateSessionManager = new NHibernateSessionManager<BaseForm>(Databaseconfig.DB_UnitHibernateTest);
                //setup the structures of the program
                _StructureSetup = new StructureSetup(_NHibernateSessionManager.SessionFactory);

                _StructureSetup.Que_HasItems += _StructureSetup_Que_HasItems;
                _StructureSetup.Que_IsEmpty += _StructureSetup_Que_IsEmpty;

                if (_userlevel == UserLevel.Admin)
                {
                    _PloegList = _StructureSetup.PloegList;
                    _TerreinList = _StructureSetup.TerreinList;

                    Userview<Persoon> _persoonview = new Userview<Persoon>(_StructureSetup.PersoonList,true) { Name = "Persoonview" };
                    Userview<Terrein> _terreinview = new Userview<Terrein>(_StructureSetup.TerreinList,false) { Name = "Terreinen" };
                    //Userview<Reeks> _reeksen = new Userview<Reeks>(_StructureSetup.ReeksList) { Name = "Reeksen" };
                    UC_reeksAssignment _reeksAssignment = new UC_reeksAssignment(_StructureSetup.PloegList) { Name = "Reeks assignment" };
                    UC_PloegOverView _PloegOverView = new UC_PloegOverView(_PloegList);
                    UC_RoundRobinSetup _RoundRobinSetup = new UC_RoundRobinSetup(_PloegList, _StructureSetup.WedstrijdList, _StructureSetup.TerreinList);
                    UC_wedstrijdViewer _WedstrijdViewer = new UC_wedstrijdViewer(_StructureSetup.WedstrijdList);
                    UC_TornooiAdministratie _TornooiAdministratie = new UC_TornooiAdministratie(_StructureSetup.WedstrijdList, _StructureSetup.TerreinList);

                    CreateDockContent(_persoonview, MainDocking);
                    CreateDockContent(_terreinview, MainDocking);
                    CreateDockContent(_reeksAssignment, MainDocking);
                    CreateDockContent(_PloegOverView, MainDocking);
                    CreateDockContent(_RoundRobinSetup, MainDocking);
                    CreateDockContent(_WedstrijdViewer, MainDocking);
                    CreateDockContent(_TornooiAdministratie, MainDocking);
                }
                else if (_userlevel == UserLevel.User)
                {
                    UC_wedstrijdViewer _WedstrijdViewer = new UC_wedstrijdViewer(_StructureSetup.WedstrijdList);
                    CreateDockContent(_WedstrijdViewer, MainDocking);
                }
            }
            catch (Exception ee)
            {                                
                logger.Error(this.Name + " Database loading error", ee);
            }
        }

        void _StructureSetup_Que_IsEmpty()
        {
            _QueStatus.Questatus = false;
        }

        void _StructureSetup_Que_HasItems()
        {
            _QueStatus.Questatus = true;   
        }


        private DockContent CreateDockContent(UserControl usercontrol, DockPanel dockPanel, DockState dockState = DockState.Document)
        {
            try
            {
                usercontrol.Dock = DockStyle.Fill;
                DockContent _DockContent = new DockContent();
                _DockContent.Controls.Add(usercontrol);                
                _DockContent.TabText = usercontrol.Name;
                _DockContent.Show(dockPanel);
                return _DockContent;
            }
            catch (Exception e)
            {
                logger.Error(this.Name + " DockContent loading error", e);
                return null;
            }
        }

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchDebugger();
        }

        private AsynchronusProcess _AsynchronusProcess;
        private void LaunchDebugger()
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\sentinel\sentinel.exe"))
                {
                    _AsynchronusProcess = new AsynchronusProcess();
                    _AsynchronusProcess.ProcessingEvent += _AsynchronusProcess_ProcessingEvent;
                    _AsynchronusProcess.runCommand(Application.StartupPath + @"\sentinel\sentinel.exe log", "log4net");
                }
            }
            catch (Exception e)
            {
                logger.Error("AsynchronusProcess issue", e);
            }
        }

        void _AsynchronusProcess_ProcessingEvent(AsynchronusProcess.ProcessEvent ProcessEvent)
        {
            switch (ProcessEvent)
            {
                case AsynchronusProcess.ProcessEvent.MessageRecieved :                
                    {
                        if (_AsynchronusProcess != null)
                        {
                            if (_AsynchronusProcess.Logginginfo != null)
                            {
                                foreach (string msg in _AsynchronusProcess.Logginginfo)
                                {
                                    logger.Debug(msg);
                                }
                            }
                        }
                    }break;
                case AsynchronusProcess.ProcessEvent.ProcessExit:
                    {
                        logger.Debug("Process " +  _AsynchronusProcess.ProcessName  +"exited");
                    }break;
                case AsynchronusProcess.ProcessEvent.PrcossExitWithFail:
                    {
                        logger.Warn("Process " + _AsynchronusProcess.ProcessName + "exited", _AsynchronusProcess.LastException);
                    }break;
                case AsynchronusProcess.ProcessEvent.ProcessError:
                    {
                        logger.Warn("Process " + _AsynchronusProcess.ProcessName + "error", _AsynchronusProcess.LastException);
                    }break;

            }
        }
    }
}
