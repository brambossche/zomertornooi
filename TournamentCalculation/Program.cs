using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using log4net;
using structures;
using structures.Views.ServerSelection;
using structures.Views;

namespace Views
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyMain UserMain = new MyMain();
        }
    }




    public class MyMain
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MyMain));
        public MyMain()
        {
            UserMain();
        }
        private void UserMain()
        {

            ProgramLogger _ProgramLogger = new ProgramLogger();
            new F_ServerSelection().ShowDialog();
            //new _Splashscreen(UserLevel.Admin).ShowDialog();

            //Don't start GUI from a different thread as is causes big issues for drag and drop which must be 
            //in as single state appartment and must run on the main UI thread!!!!
            //oherwise <AllowDrop = true> will cause citical error
            //[STAThread] must be assigned when you want somewhere drag and drop
            //http://stackoverflow.com/questions/3185861/system-invalidoperationexception-in-vs-2005
            /*    
            Thread ProgramThread = new Thread(() =>
                {
                    try
                    {
                        new BaseForm().ShowDialog();
                    }
                    catch (Exception e)
                    {
                        logger.Error("Main load exception", e);
                    }
                });
            ProgramThread.Start();*/
        }
    }

}
