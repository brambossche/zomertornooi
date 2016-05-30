using System;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;


namespace OutputLogging
{
    public class LogLine
    {
        private DateTime _DT;

        public string DT
        {
            get { return _DT.ToString(); }
        }

        private OutputType _LogType;

        public OutputType LogType
        {
            get { return _LogType; }
            set { _LogType = value; }
        }


        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }


        private string _Source;

        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }


        private string _ObjectType;

        public string ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }

        private object _Data;

        /* public string Data
         {
             get { return BitConverter.ToString(tobytes(_Data)); }
         
         }*/


        public LogLine(OutputType logType, string message, object data, string objecttype)
        {
            _DT = DateTime.Now;
            _Message = message;
            _Data = data;
            _ObjectType = objecttype;
            _LogType = logType;
            Source = "ERROR";


        }

        public LogLine(OutputType logType, string message)
        {
            _DT = DateTime.Now;
            _Message = message;

            _LogType = logType;
            getStackinfo(3);



        }

        private void getStackinfo(int level)
        {
            try
            {
                StackTrace st = new StackTrace(true);

                Source = st.GetFrame(level).GetMethod().ReflectedType.Name;
                //stackinfo();
            }
            catch (Exception e)
            {
                Source = "UNABLE TO GET STACK INFO";

            }
        }

        public void stackinfo()
        {
            try
            {
                throw new Exception("A problem was encountered.");
            }
            catch (Exception e)
            {
                // Create a StackTrace that captures filename, 
                // line number and column information.
                StackTrace st = new StackTrace(true);
                string stackIndent = "";
                for (int i = 0; i < st.FrameCount; i++)
                {
                    // Note that at this level, there are four 
                    // stack frames, one for each method invocation.
                    StackFrame sf = st.GetFrame(i);
                    Console.WriteLine();
                    Console.WriteLine(stackIndent + " Method: {0}",
                        sf.GetMethod());
                    Console.WriteLine(stackIndent + " File: {0}",
                        sf.GetFileName());
                    Console.WriteLine(stackIndent + " Line Number: {0}",
                        sf.GetFileLineNumber());
                    stackIndent += "  ";
                }
                throw e;
            }
        }



        public LogLine(string serializedlogstream)
        {
            //  throw new Exception("dd");
            string[] splitline = serializedlogstream.Split(';');
            _DT = DateTime.Parse(splitline[0]);
            _LogType = (OutputType)Enum.Parse(typeof(OutputType), splitline[1]);
            _Message = splitline[3];
            _Source = splitline[2];

            switch (_LogType)
            {
                case OutputType.error:

                    _ObjectType = splitline[4];
                    _Data = toobject(Convert.FromBase64String(splitline[5]));
                    break;

            }



        }

        // 0 date
        // 1 type
        // 2 source
        // 3 msg
        // 4 obj type
        // 5 obj

        public string ToString()
        {
            string resultline = _DT.ToString();
            resultline += ";" + _LogType.ToString();
            resultline += ";" + _Source;
            resultline += ";" + _Message;

            switch (_LogType)
            {
                case OutputType.console:
                    break;
                case OutputType.error:

                    resultline += ";" + _ObjectType;

                    resultline += ";" + Convert.ToBase64String(tobytes(_Data));
                    break;
                case OutputType.log:
                    break;
                case OutputType.messagebox:
                    break;
                case OutputType.warning:
                    break;
                default:
                    break;
            }


            return resultline;



        }

        public Exception getExecption()
        {
            if (_Data.GetType() == typeof(string))
            {
                return (new Exception((string)_Data));
            }
            else
            {
                return (Exception)_Data;
            }
        }

        public byte[] tobytes(object data)
        {
            byte[] inMemoryBytes;
            using (MemoryStream inMemoryData = new MemoryStream())
            {
                new NetDataContractSerializer().Serialize(inMemoryData, data);
                inMemoryBytes = inMemoryData.ToArray();
            }

            return inMemoryBytes;

        }

        public object toobject(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            using (MemoryStream dataInMemory = new MemoryStream(data))
            {
                return new NetDataContractSerializer().Deserialize(dataInMemory);
            }
        }
    }
}



