using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using log4net;
using System.Net;


namespace Marb.Logger4net
{
    
    /// <summary>
    ///http://stackoverflow.com/questions/9903894/how-to-configure-nhibernate-logging-with-log4net-in-code-not-in-xml-file
    ///http://stackoverflow.com/questions/16336917/can-you-configure-log4net-in-code-instead-of-using-a-config-file
    ///http://stackoverflow.com/questions/885378/log4net-pure-code-configuration-with-filter-in-c-sharp
    ///https://logging.apache.org/log4net/release/config-examples.html    
    /// </summary>
    public class Logger4net 
    {
        public static log4net.Repository.Hierarchy.Hierarchy _HierarchyLogger;
        private static bool _isconfigured = false;
        private List<Logger> _Loggers = new List<Logger>();

        public List<Logger> Loggers
        {
            get { return _Loggers; }
            set { _Loggers = value; }
        }

        public Logger4net()
        {
            Create_HierarchyLogger();            
        }

        private void Create_HierarchyLogger()
        {
            if (_isconfigured == false)
            {
                _HierarchyLogger = (Hierarchy)LogManager.GetRepository();
                // Remove any other appenders
                _HierarchyLogger.Root.RemoveAllAppenders();
                // define some basic settings for the root                
                _HierarchyLogger.Root.Level = Level.All;
                _HierarchyLogger.Configured = true;                
            }
        }

        /// <summary>
        /// Adding a logger
        /// </summary>
        /// <param name="Name">Namespace name of teh logger to be found</param>
        /// <param name="appender">Appender</param>
        /// <param name="level">Logging level
        ///Log level priority in descending order:
        ///FATAL = 1 show  log -> FATAL 
        ///ERROR = 2 show  log -> FATAL ERROR 
        ///WARN =  3 show  log -> FATAL ERROR WARN 
        ///INFO =  4 show  log -> FATAL ERROR WARN INFO 
        ///DEBUG = 5 show  log -> FATAL ERROR WARN INFO DEBUG
        /// </param>
        public void AddLogger(string Name, IAppender appender, Level level)
        {
            try
            {
                _HierarchyLogger.Configured = false;
                Logger L = _HierarchyLogger.GetLogger(Name) as Logger;
                _Loggers.Add(L);
                L.Level = level;
                L.AddAppender(appender);
                _HierarchyLogger.Configured = true;
                _HierarchyLogger.RaiseConfigurationChanged(EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.WriteLine("appender error : " + e);
            }
        }


        public void AddRootLogAppender (IAppender Appender, Level level)
        {
            _HierarchyLogger.Configured = false;

            _HierarchyLogger.Root.AddAppender(Appender);
            _HierarchyLogger.Root.Level = level;
            
            _HierarchyLogger.Configured = true;
            _HierarchyLogger.RaiseConfigurationChanged(EventArgs.Empty);
        }
    }

    public class Predef_PatternLayout : PatternLayout
    {
        public const string MinimalPattern = "%message  %newline";
        public const string SimplePattern = "%date{dd MMM HH:mm:ss} - %logger - %message - %newline";
        public const string StandardPattern = "%date{dd MMM HH:mm:ss}  %-5level - %logger - %message - %newline";
        public const string FullPattern = "%date - [%thread] %-5level - %logger - %location - %line - %message - %newline";
        
        public Predef_PatternLayout()
            : base()
        {
            this.ConversionPattern = StandardPattern;
            this.ActivateOptions();
        }

        public Predef_PatternLayout(string ConversionPattern)
            : base()
        {
            this.ConversionPattern = ConversionPattern;
            this.ActivateOptions();
        }
    }


    public class LogUDPAppender : UdpAppender
    {
        public LogUDPAppender()
            :base()
        {
            try
            {
                log4net.Layout.XmlLayout xmllayout = new log4net.Layout.XmlLayout();
                xmllayout.Prefix = "log4net";
                xmllayout.ActivateOptions();
                this.Name = "UDPappender";
                this.Layout = xmllayout;
                this.RemoteAddress = IPAddress.Parse("127.0.0.1");
                this.RemotePort = 9999;
                this.ActivateOptions();
            }
            catch (Exception e)
            {
                Console.WriteLine("LogUDPAppender error " + e);
            }
        }

        public LogUDPAppender(string IPAddres = "127.0.0.2", int PortNr = 9998)
            : this()
        {
            try
           {
                this.RemoteAddress = IPAddress.Parse(IPAddres);
                this.RemotePort = PortNr;
                this.ActivateOptions();
            }
            catch(Exception e)
            {
                Console.WriteLine("LogUDPAppender error " + e);
            }


        }
    }



    public class LogRollingFileAppender : RollingFileAppender
    {
        public LogRollingFileAppender()
            : base()
        {
            this.AppendToFile = true;
            this.File = @"Logs\EventLog.txt";
            this.Layout = new Predef_PatternLayout();
            this.MaxSizeRollBackups = 5;
            this.MaximumFileSize = "5MB";
            this.RollingStyle = RollingFileAppender.RollingMode.Size;
            this.StaticLogFileName = true;
            this.ActivateOptions();                        
        }

        public LogRollingFileAppender(string filename)
            : base()
        {
            this.AppendToFile = true;
            this.File = filename;
            this.Layout = new Predef_PatternLayout();
            this.MaxSizeRollBackups = 5;
            this.MaximumFileSize = "5MB";
            this.RollingStyle = RollingFileAppender.RollingMode.Size;
            this.StaticLogFileName = true;
            this.ActivateOptions();
        }

        public LogRollingFileAppender(string filename, string ConversionPattern)
            : base()
        {           
            this.AppendToFile = true;
            this.File = filename;
            this.Layout = new Predef_PatternLayout(ConversionPattern);
            this.MaxSizeRollBackups = 5;
            this.MaximumFileSize = "5MB";
            this.RollingStyle = RollingFileAppender.RollingMode.Size;
            this.StaticLogFileName = true;
            this.ActivateOptions();
        }

        public override ILayout Layout
        {
            get
            {
                return base.Layout;
            }
            set
            {
                base.Layout = value;
                base.ActivateOptions();               
            }
        }
    }

    public class LogMemoryAppender : MemoryAppender
    {
        public LogMemoryAppender()
            : base()
        {
            //this.ActivateOptions();
        }
    }


    public class LogOutputDebugWindow : OutputDebugStringAppender
    {
        public LogOutputDebugWindow() : base() 
        {
            //this.ActivateOptions();          
        }
    }
}

