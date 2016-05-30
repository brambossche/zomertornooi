using System;

namespace OutputLogging
{

    public enum LogwindowType
    {
        UserLog, 
        Log4Net
    }
    /// <summary>
    /// The outputcontroller interface allows for ful scripting control of the 4 output windows ( error , log , console , warning )
    /// </summary>
    public interface IOutputController
    {   


        /// <summary>
        /// Copy of writetoconsole
        /// </summary>
        /// <param name="command">Copy of writetoconsole</param>
        void WriteToLog(string Message, LogwindowType logwindowType = LogwindowType.UserLog);
        /// <summary>
        /// Get the logging text
        /// </summary>
        /// <returns></returns>
        string GetLogText();
        /// <summary>
        /// clearing the logging window
        /// </summary>
        void ClearLogwindow();
        /// <summary>
        /// clearing the error window
        /// </summary>
        void ClearErrorwindow();
        /// <summary>
        /// only write to logfile, not the window
        /// </summary>
        bool LogwindowWriteToFileOnly { set; get; }
        /// <summary>
        /// enable or disable timestamp
        /// </summary>
        bool LogwindowEnableTimestamp { set; get; }
        /// <summary>
        /// enable call trace 
        /// </summary>
        bool LogwindowEanableCaller { set; get; }
        /// <summary>
        /// Writes an error to the error logwindow
        /// </summary>
        /// <param name="message">the message of the error</param>
        /// <param name="Error">an object of type Exception</param>
        /// <param name="errortype">the subtype of the exception in the error object ( subtype of exception ) eg nullException</param>
        void writeError(string message, object Error, string errortype);
        /// <summary>
        /// Writes a message straight to the log ( message does not appear in console )
        /// </summary>
        /// <param name="Message">string containing the message to be written</param>
        void writenoline(string Message);
        /// <summary>
        /// helper function to unlock the lock file ( not to be used via scripting )
        /// </summary>
        void releaseRessources();
        /// <summary>
        /// Gets the listviewhandler ( not to be used via scriptin )
        /// </summary>
        /// <returns>Listview</returns>
        System.Windows.Forms.ListView GetListviewHandler();
        /// <summary>
        /// Gives you a list of the 3 output controls ( error , warning , console )
        /// This function is not recommended for use in scripting
        /// </summary>
        /// <returns>collection of outputcontrols</returns>
        System.Collections.Generic.List<WeifenLuo.WinFormsUI.Docking.DockContent> getOutputControls();


        
    }

    /// <summary>
    /// eventhandling used to send to the Output controller
    /// http://beqbrgbrg1nb763:1024/redmine/issues/142
    /// </summary>
    public delegate void Createreport(); 
    public delegate void delI2ClogEnabled();

    public enum OutputType
    {
        error,
        warning,
        console,
        log,
        log4net,
        messagebox
    }

}
