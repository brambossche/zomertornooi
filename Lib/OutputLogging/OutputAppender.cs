using System;
using System.Globalization;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using OutputLogging;

namespace PCL.OutputLogging
{
    public class OutputAppender : AppenderSkeleton
    {

        private OutputController _OutputController =null;

		#region Public Instance Constructors
        
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputAppender" /> class.
		/// </summary>
		/// <remarks>
		/// The instance of the <see cref="OutputAppender" /> class is set up to write 
		/// to the standard output stream.
		/// </remarks>
		public OutputAppender() 
		{
            _OutputController = OutputController.instance;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleAppender" /> class
		/// with the specified layout.
		/// </summary>
		/// <param name="layout">the layout to use for this appender</param>
		/// <param name="writeToErrorStream">flag set to <c>true</c> to write to the console error stream</param>
		/// <remarks>
		/// When <paramref name="writeToErrorStream" /> is set to <c>true</c>, output is written to
		/// the standard error output stream.  Otherwise, output is written to the standard
		/// output stream.
		/// </remarks>
		[Obsolete("Instead use the default constructor and set the Layout & Target properties")]
        public OutputAppender(ILayout layout) 
		{
			Layout = layout;
			//m_writeToErrorStream = writeToErrorStream;
		}
        
		#endregion Public Instance Constructors

		#region Public Instance Properties



		#endregion Public Instance Properties

		#region Override implementation of AppenderSkeleton

		/// <summary>
		/// This method is called by the <see cref="M:AppenderSkeleton.DoAppend(LoggingEvent)"/> method.
		/// </summary>
		/// <param name="loggingEvent">The event to log.</param>
		/// <remarks>
		/// <para>
		/// Writes the event to the console.
		/// </para>
		/// <para>
		/// The format of the output will depend on the appender's layout.
		/// </para>
		/// </remarks>
		override protected void Append(LoggingEvent loggingEvent) 
		{

            switch (loggingEvent.Level.Name)
            {
                /// <summary>
                /// The <see cref="Emergency" /> level designates very severe error events. 
                /// System unusable, emergencies.
                /// </summary>
                case "Off":
                case "log4net:DEBUG":
                case "Emergency":
                    { }break;
                /// <summary>
                /// The <see cref="Fatal" /> level designates very severe error events 
                /// that will presumably lead the application to abort.
                /// </summary>
                case "FATAL":
                    { } break;
                /// <summary>
                /// The <see cref="Alert" /> level designates very severe error events. 
                /// Take immediate action, alerts.
                /// </summary>
                case "ALERT":
                    { 
                        //_OutputController.writeError (loggingEvent.)
                    }
                    break;
                /// <summary>
                /// The <see cref="Critical" /> level designates very severe error events. 
                /// Critical condition, critical.
                /// </summary>
                case "CRITICAL":
                    { } break;
                /// <summary>
                /// The <see cref="Severe" /> level designates very severe error events.
                /// </summary>
                case "SEVERE":
                    { } break;
                /// <summary>
                /// The <see cref="Error" /> level designates error events that might 
                /// still allow the application to continue running.
                /// </summary>
                case "ERROR":
                    { } break;
                /// <summary>
                /// The <see cref="Warn" /> level designates potentially harmful 
                /// situations.
                /// </summary>
                case "WARN":
                    { } break;
                /// <summary>
                /// The <see cref="Notice" /> level designates informational messages 
                /// that highlight the progress of the application at the highest level.
                /// </summary>
                case "NOTICE":
                    { } break;
                /// <summary>
                /// The <see cref="Info" /> level designates informational messages that 
                /// highlight the progress of the application at coarse-grained level.
                /// </summary>
                case "INFO":
                    { } break;
                /// <summary>
                /// The <see cref="Debug" /> level designates fine-grained informational 
                /// events that are most useful to debug an application.
                /// The <see cref="Fine" /> level designates fine-grained informational 
                /// events that are most useful to debug an application.
                /// </summary>
                case "DEBUG":
                case "FINE":
                    { } break;
                /// <summary>
                /// The <see cref="Trace" /> level designates fine-grained informational 
                /// events that are most useful to debug an application.
                /// The <see cref="Finer" /> level designates fine-grained informational 
                /// events that are most useful to debug an application.
                /// </summary>
                case "TRACE":
                case "Finer":
                    { } break;
                /// <summary>
                /// The <see cref="Verbose" /> level designates fine-grained informational 
                /// events that are most useful to debug an application.
                /// The <see cref="Finest" /> level designates fine-grained informational 
                /// events that are most useful to debug an application.
                /// </summary>
                case "VERBOSE":
                case "FINEST":
                    { } break;
                /// <summary>
                /// The <see cref="All" /> level designates the lowest level possible.
                /// </summary>
                case "ALL":
                    { } break;

            }




                // Write to the output stream
                Console.Write(RenderLoggingEvent(loggingEvent));
            
		}

		/// <summary>
		/// This appender requires a <see cref="Layout"/> to be set.
		/// </summary>
		/// <value><c>true</c></value>
		/// <remarks>
		/// <para>
		/// This appender requires a <see cref="Layout"/> to be set.
		/// </para>
		/// </remarks>
		override protected bool RequiresLayout
		{
			get { return true; }
		}

		#endregion Override implementation of AppenderSkeleton

		#region Public Static Fields


		#endregion Public Static Fields

		#region Private Instances Fields

		#endregion Private Instances Fields





































        /*protected override void Append(LoggingEvent loggingEvent)
        {
            Level logLevel = Level.Error;
            switch (loggingEvent.Level.Name)
            {
                case "DEBUG":
                    logLevel = Level.Debug;
                    break;
                case "WARN":
                case "INFO":
                    logLevel = Level.Info;
                    break;
                case "ERROR":
                    logLevel = Level.Error;
                    break;
                case "FATAL":
                    logLevel = Level.Critical;
                    break;
            }*/




    }
}
