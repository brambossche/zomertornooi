using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using log4net.Appender;

namespace OutputLogging
{
    public class OutputController : IOutputController
    {

        public static OutputController instance = null;

        private UcConsoleWindow ucCons;
        private UcErrorWindow ucErr;
        private UcLogWindow ucLog;
        private UcLogWindow templog;
        private UcLogWindow ucLog4net;
        private UcSQLLogger ucSQLLogger;
        private ListView ListLog4Net;

         Tuple<UcLogWindow, StreamWriter> tmpLogs;// = Tuple<UcLogWindow, StreamWriter>();
      

        List<DockContent> docklist = new List<DockContent>();

        private string logfilename = System.Windows.Forms.Application.StartupPath + "\\log.log";

        private StreamWriter stwrt;

        private bool logging = true;


        public event Createreport Requestreport;
        Dictionary<string, System.IO.StreamWriter> lstextralogs = new Dictionary<string, StreamWriter>();
        #region "constructor"


        public void StopExtraLogFile(string Fname)
        {

            if (lstextralogs.ContainsKey(Fname))
            {
                lstextralogs[Fname].Flush();
                lstextralogs[Fname].Close();
                lstextralogs.Remove(Fname);
            }
        }


        public void addExtraLogFile(string Fname)
        {
            try
            {
                StreamWriter strwrt = new StreamWriter(Fname,false);
                if (!lstextralogs.ContainsKey(Fname))
                {
                    lstextralogs.Add(Fname, strwrt);
                }

            }
            catch (Exception e)
            { 
            
            }
        
        
        }

        public OutputController()
        {

            ucCons = new UcConsoleWindow();
            ucErr = new UcErrorWindow();
            ucLog = new UcLogWindow();
            ucLog4net = new UcLogWindow();
            templog = new UcLogWindow();
            ucSQLLogger = new UcSQLLogger();


            if (OutputController.instance == null)
            {
                OutputController.instance = this;
            }
            templog.Name = "TempLog";
            templog.Text = "TempLog";
            templog.diagmode = false;

            ListLog4Net = ucSQLLogger.GetListviewHandler();

            docklist.Add(ucErr);
            docklist.Add(ucSQLLogger);
            //docklist.Add(ucCons);
            //docklist.Add(templog);
            docklist.Add(ucLog);
            //docklist.Add(ucLog4net);


            try
            {
                stwrt = new StreamWriter(logfilename, false);
                ucErr.Requestreport += new UcErrorWindow.Createreport(ucErr_Requestreport);
            }
            catch
            {
            }

            
        }


        void ucErr_Requestreport()
        {
            Requestreport();
        }

        bool templogging = false;

        public void CLearLogFile()
        {
            stwrt.Close();
            System.IO.File.Delete(logfilename);
            stwrt = new StreamWriter(logfilename, false);

        }

        public void startTempLog(string fname)
        {

            if (!(Directory.Exists(Application.StartupPath + "//LOG//")))
            {
                Directory.CreateDirectory(Application.StartupPath + "//LOG//");
            }
            try
            {
                templog.writeLine("Start logging of " + fname);

                StreamWriter strwrt = new StreamWriter(Application.StartupPath + "//LOG//" + fname + ".csv");

                tmpLogs = new Tuple<UcLogWindow, StreamWriter>(templog, strwrt);
                templogging = true;
                WriteTempLog("sep=;");
            
            }
            catch(Exception e)
            {
                this.writeError("File write error", e, "Log file error");
            }  
        }

        public void stopTempLog(string fname)
        {

            try
            {
                tmpLogs.Item1.writeLine("Stop Logging of " + fname);
             //   tmpLogs.Item1.Hide();
                tmpLogs.Item2.Flush();
                tmpLogs.Item2.Close();
                templogging = false;


            }
            catch(Exception e)
            {
                this.writeError("File write error", e, "Log file error");
            }        
        }

        public delegate void writelinedel(string msg);
        public void WriteTempLog(string logline)
        {
            if (!templogging)
            {
                return;
            }

            if (tmpLogs.Item1.InvokeRequired)
            {
                tmpLogs.Item1.Invoke(new writelinedel(WriteTempLog), new object[] { logline });

            }
            else
            {

                tmpLogs.Item1.writeLine(logline);
                tmpLogs.Item2.WriteLine(logline);
            }
        
        
        }

        public void releaseRessources()
        {
            try
            {
                stwrt.Close();
            }
            catch
            {
            }
        }

        public void test()
        {
            object test = null;
            try
            {
                test.ToString();
            }
            catch (Exception e)
            {

                writeError("a test", e, e.GetType().ToString());

            }

            try
            {
                int lezero = 1 - 1;

                int haha = 100 / (lezero);

            }
            catch (Exception e)
            {

                writeError("a test", e, e.GetType().ToString());

            }
        
        }

        #endregion

        #region "DockContent"

        public List<DockContent> getOutputControls()
        {        
            return docklist;
        }

        #endregion

        /*public void write(OutputType type, string textdata, object extradata)
        { 
                
        }*/

        #region consolewindow implementation

        public void clear()
        {
            ucLog.writeLine("clear called");

        }


        public void write(string Message)
        {
            ucLog.writeLine("normal writeline called with :: " + Message); 
        }

        public void writeCommand(string command)
        {
            ucLog.writeLine("writecommand called with :: " + command);
        }

        public string GetLogText()
        { 
            return ucLog.GetLogText();
        }

        public void ClearLogwindow()
        {
            ucLog.clear();
            ucLog.Activate();
        }

        #endregion

        #region "iOutputcontroller implementation"

        
        public void writeToConsole(string txt)
        {

            if (ucCons.InvokeRequired)
            {
                ucCons.Invoke(new writelinedel(writeToConsole), new object[] { txt });
            }
            else
            {
                
                ucCons.writeLine(/*("<" + getStackTrace(3) + ">").PadRight(30)  +*/ txt);
                LogLine l = new LogLine(OutputType.console, txt);
                log(l);
            }
        
        }

        public delegate void del_WriteToLogWindowType(string Message, LogwindowType logwindowType);
        public void WriteToLog(string Message, LogwindowType logwindowType)
        {
            if (ucLog.InvokeRequired)
            {
                ucLog.Invoke(new del_WriteToLogWindowType(WriteToLog), new object[] { Message, logwindowType });
            }
            else
            {
                if (logwindowType == LogwindowType.UserLog)
                {
                    ucLog.writeLine(Message);
                    LogLine l = new LogLine(OutputType.log, Message);
                    log(l);
                }
                else if (logwindowType == LogwindowType.Log4Net)
                {
                    ListLog4Net.Items.Add(new ListViewItem(Message));
                }
            }

        }


        public delegate void writedel(string msg);
        public void writenoline(string Message)
        {
            if (ucLog.InvokeRequired)
            {
                ucLog.Invoke(new writedel(writenoline), new object[] { Message });
            }
            else
            {

                ucLog.write(Message);
                LogLine l = new LogLine(OutputType.log, Message);
                log(l);
            }

        }


        public delegate void writeerrordel(string msg,object err,string errtype);

        public void writeError(string message, object Error, string errortype)
        {
          /*  if (Debugger.IsAttached)
            {
                throw (Exception) Error;
            }
            */

            if (ucErr == null)
            {
                return; 
            }

            if (ucErr.InvokeRequired)
            {
                ucErr.Invoke(new writeerrordel(writeError), new object[] { message, Error, errortype });
            }
            else
            {

                LogLine l = new LogLine(OutputType.error, message, Error, errortype);
                string result = l.ToString();
                log(l);
                ucErr.logError(l);
            }
        
        }


        public ListView GetListviewHandler()
        {
            return ucSQLLogger.GetListviewHandler();
        }

        #endregion

        private void log(LogLine l)
        {
            if (logging)
            {
                try
                {
                    stwrt.WriteLine(l.ToString());
                    stwrt.Flush();
                }
                catch
                {
                }

                foreach (var item in lstextralogs)
                {
                    item.Value.WriteLine(l.ToString());
                }


            }
        
        }


        public string currentLine
        {
            get { return ""; }
        }


        #region "helpers"

        private string getStackTrace(int level)
        {
            StackTrace st = new StackTrace(true);

            StackFrame sf = st.GetFrame(level);

            return sf.GetMethod().ReflectedType.Name ;


        }

        #endregion




        public bool LogwindowWriteToFileOnly
        {
            get
            {
                return ucLog.LowgiwndowWriteToFileOnly;
            }
            set
            {
                ucLog.LowgiwndowWriteToFileOnly = value;
            }
        }

        #region IOutputController Members


        public bool LogwindowEnableTimestamp
        {
            get
            {
                return ucLog.LogwindowEnableTimestamp;
            }
            set
            {
                ucLog.LogwindowEnableTimestamp = value;
            }
        }

        public bool LogwindowEanableCaller
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IOutputController Members


        public void ClearErrorwindow()
        {
            ucErr.ClearErrorWindow();
            ucErr.Activate();
        }

        #endregion

    }
}
