using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



namespace OutputLogging
{
    public class LogFile
    {

       public List<LogLine> logLineList = new List<LogLine>();

        public LogFile(string fname)
        {
            Load(fname);
        }
        
        private void Load(string fname) 
        {
            StreamReader strdr = new StreamReader(fname);
            while (!strdr.EndOfStream)
            {
                string line = strdr.ReadLine();
                LogLine l = new LogLine(line);
                logLineList.Add(l);

            
            }
            strdr.Close();

        }

    }
}
