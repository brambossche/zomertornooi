using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Marb.ExternalProcesses
{
    public class SynchronusProcess
    {
        /// <summary>
        /// Blocking function - returns when the process is done
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="arguments"></param>
        public void runCommand(string filename, string arguments = null)
        {
            Process process = new Process();
            process.StartInfo.FileName = filename;
            if (arguments != null)
            {
                process.StartInfo.Arguments = arguments; 
            }
            process.StartInfo.Verb = "runas";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            _output = process.StandardOutput.ReadToEnd();
            _error = process.StandardError.ReadToEnd();            
            process.WaitForExit();
        }

        private string _error = "";
        public string Error
        {
            get { return _error; }
        }
        private string _output = "";
        public string Output
        {
            get { return _output; }
        }
    }


    public class AsynchronusProcess
    {
        public enum ProcessEvent
        {
            MessageRecieved,
            ProcessExit,
            PrcossExitWithFail,
            ProcessError,
        }

        private object locker;

        private List<string> _logging = new List<string>();        
        public List<string> Logginginfo
        {
            get 
            {
                lock (locker)
                {
                    List<string> returnlist = _logging.ToList();
                    _logging.Clear();
                    return returnlist;
                }                
            }            
        }

        private Exception _LastException;

        public Exception LastException
        {
            get { return _LastException; }
            set { _LastException = value; }
        }

        private string _ProcessName = "";

        public string ProcessName
        {
            get 
            {
                if (_ProcessName != "")
                {
                    return _ProcessName; 
                }
                else
                {
                    if (process != null)
                    {
                        return process.ProcessName;
                    }
                    else
                    {
                        return "";
                    }
                }                
            }
            set { _ProcessName = value; }
        }

        private Process process;
        public void runCommand(string filename, string arguments = null)
        {
            try
            {
                //* Create your Process
                process = new Process();
                process.StartInfo.FileName = filename;
                if (arguments != null)
                {
                    process.StartInfo.Arguments = arguments;
                }
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                //* Set your output and error (asynchronous) handlers
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
                //* Set your output and error (asynchronous) handlers
                process.Exited += process_Exited;

                // enable raising events because Process does not raise events by default
                process.EnableRaisingEvents = true;
                //* Start process and handlers
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                //process.WaitForExit();
            }
            catch (Exception e)
            {
                ProcessFail(e);
            }
        }

        public delegate void del_ProcessEvent(ProcessEvent ProcessEvent);
        public event del_ProcessEvent ProcessingEvent;
        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            _logging.Add( outLine.Data);
            if (ProcessingEvent != null)
            {
                ProcessingEvent.Invoke(ProcessEvent.MessageRecieved);
            }
        }

        private void process_Exited(object sender, EventArgs e)
        {            
            if (ProcessingEvent != null)
            {
                ProcessingEvent.Invoke(ProcessEvent.ProcessExit);
            }
        }

        private void ProcessFail(Exception e)
        {
            _LastException = e;
            if (ProcessingEvent != null)
            {
                ProcessingEvent.Invoke(ProcessEvent.PrcossExitWithFail);
            }
        }
    }
}
