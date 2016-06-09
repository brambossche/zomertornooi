using System;
using System.Diagnostics;
using System.Net;
using log4net.Appender;
using log4net.Core;
using System.Linq;
using Marb.Logger4net;
using Marb.Helper;
using System.Threading;

namespace structures
{


    /// <summary>
    /// class which sets all loggers to be enabled
    /// </summary>
    public class ProgramLogger
    {

        private Logger4net _Logger4net;
        private LogRollingFileAppender _LRFA_NHIbernateLogger;
        private LogRollingFileAppender _LRFA_NHIbernateLogger_SQL;
        private LogRollingFileAppender _LRFA_NHIbernateLogger_Cfg;
        private LogRollingFileAppender _LRFA_ProgramLogger;

        private ConsoleAppender _Con_RoundRobin;


        private LogUDPAppender _LUDP_NHIbernateLogger;

        private bool _LogToUDP = true;
        public bool LogToUDP
        {
            get { return _LogToUDP; }
            set { _LogToUDP = value; }
        }

        private bool _LoggerEnabled = true;

        public bool LoggerEnabled
        {
            get { return _LoggerEnabled; }
            set { _LoggerEnabled = value; }
        }


        public ProgramLogger()
        {
            if (_LoggerEnabled || Debugger.IsAttached || UnitTestDetector.IsInUnitTest)
            {
                Thread LogThread = new Thread(() =>
                {
                    try
                    {

                        _Logger4net = new Logger4net();


                        if (_LogToUDP)
                        {
                            _LUDP_NHIbernateLogger = new LogUDPAppender("127.0.0.2", 9998);

                            _Logger4net.AddLogger("NHibernate.SQL", _LUDP_NHIbernateLogger, Level.All);

                            //_LRFA_NHIbernateLogger_SQL = new LogRollingFileAppender(@"Logs\NHibernate_SQL.txt", new log4net.Layout.XmlLayout(false));
                            //_Logger4net.AddLogger("NHibernate.SQL", _LRFA_NHIbernateLogger_SQL, Level.All);

                            //_Logger4net.AddLogger("NHibernate.SQL", new AsyncFileAppender(), Level.Debug);
                        }

                        else
                        {
                            //full logging of all loggers = Rootlogger
                            _LRFA_NHIbernateLogger = new LogRollingFileAppender(@"Logs\Rootlog_Debug.txt", Predef_PatternLayout.FullPattern);
                            _Logger4net.AddRootLogAppender(_LRFA_NHIbernateLogger, Level.Info);


                            ///simple logging of sql data
                            _LRFA_NHIbernateLogger_SQL = new LogRollingFileAppender(@"Logs\NHibernate_SQL.txt", Predef_PatternLayout.SimplePattern);
                            _Logger4net.AddLogger("NHibernate.SQL", _LRFA_NHIbernateLogger_SQL, Level.Info);

                            ///print to console for test output
                            _Con_RoundRobin = new ConsoleAppender(new Predef_PatternLayout(Predef_PatternLayout.MinimalPattern));
                            _Logger4net.AddLogger("TournamentCalculation", _Con_RoundRobin, Level.All);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error " + e);
                    }
                });
                LogThread.IsBackground = true;
                LogThread.Start();
            }
        }
    }
}


                    /*
                    else
                    {
                        if (_LogToUDP)
                        {
                            _LUDP_NHIbernateLogger = new LogUDPAppender("127.0.0.2", 9998);
                            _Logger4net.AddRootLogAppender(_LUDP_NHIbernateLogger, Level.Warn);
                        }
                        else
                        {
                            _LRFA_NHIbernateLogger = new LogRollingFileAppender(@"Logs\Rootlog_Warning.txt", Predef_PatternLayout.FullPattern);
                            _Logger4net.AddRootLogAppender(_LRFA_NHIbernateLogger, Level.Warn);

                            _LRFA_NHIbernateLogger_SQL = new LogRollingFileAppender(@"Logs\NHibernate_SQL.txt", Predef_PatternLayout.SimplePattern);
                            _Logger4net.AddLogger("NHibernate.SQL", _LRFA_NHIbernateLogger_SQL, Level.All);
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("excepion" + e);

                }*/