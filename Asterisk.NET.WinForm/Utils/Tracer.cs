

using System;
using Microsoft.Win32;
using System.IO;
namespace LogUtils
{
    public class Tracer
    {
        private static volatile Tracer _instance;        
        private static object _syncRoot = new Object();
        private static string LogFileName { get; set; }
        public int StackLevel { get; set; }
        private bool _stackTracerCall;

        private Tracer()
        {
            Initialize();
        }

        public static Tracer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new Tracer();
                    }
                }
         
                return _instance;
            }
        }

        bool GetLogFileName()
        {
            try
            {
                string LogDirName = Path.Combine(Directory.GetCurrentDirectory(), "Log");
                LogFileName = Path.Combine(LogDirName, "asterisk.log");
                if (!Directory.Exists(LogDirName))
                {
                    Directory.CreateDirectory(LogDirName);
                }
                if (!File.Exists(LogFileName))
                {
                    File.Create(LogFileName);   
                }
                else if (new FileInfo(LogFileName).Length > 1024*1024)
                {
                    File.Move(LogFileName, LogFileName + "_" + DateTime.Now.ToString("dd-MM-yy_HH-mm") + ".log");
                    //File.Delete(LogFileName);
                    //File.WriteAllText(LogFileName, string.Empty); 
                }
                return !string.IsNullOrEmpty(LogFileName);
            }
            catch (Exception exception)
            {
                Trace(exception.ToString());                
            }
            return false;
        }

        void CreateFolderStructure()
        {
            try
            {
                if (!string.IsNullOrEmpty(Path.GetFileName(LogFileName)))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(LogFileName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(LogFileName));
                    }
                }
            }
            catch (Exception exception)
            {
                Trace(exception.ToString());
            }            
        }

        void Initialize()
        {
            StackLevel = -1;
            if (GetLogFileName())
            {
                CreateFolderStructure();
                //CheckLogFileAge();
            }
        }

        private void CheckLogFileAge()
        {
            throw new NotImplementedException();
        }

        void Trace(string loggerMessage)
        {
            string wrappedMessage = AddDateTimeStamp(loggerMessage);
            WriteToFile(wrappedMessage);
        }

        private void WriteToFile(string wrappedMessage)
        {
#if LIGHT
            Console.WriteLine(wrappedMessage);
#else
            StreamWriter file = null;
            try
            {
                file = new StreamWriter(LogFileName, true);
                file.WriteLine(wrappedMessage);
            }
            catch
            {
            	
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
#endif
        }

        string AddDateTimeStamp(string loggerMessage)
        {
            DateTime currentDareTime = DateTime.Now;
            
            int tabCount = StackLevel;
            if (!_stackTracerCall)
            {
                tabCount++;
            }

            string tabSequence = tabCount > 0 ? new string('\t', tabCount) : string.Empty;

            return string.Format("[{0}]: {2} {1}\n", currentDareTime.ToString(@"M/d/yyyy HH:mm:ss.fff"), loggerMessage, tabSequence);
        }

        public void TraceWL3(string format)
        {
            TraceWL3(format, null, false);
        }

        public void TraceWL3(string format, object arg1)
        {
            TraceWL3(format, new object[] {arg1}, false);
        }

        public void TraceWL3(string format, object arg1, object arg2)
        {
            TraceWL3(format, new object[] { arg1, arg2 }, false);
        }

        public void TraceWL3(string format, object arg1, object arg2, object arg3)
        {
            TraceWL3(format, new object[] { arg1, arg2, arg3 }, false);
        }

        public void TraceWL3(string format, object[] args)
        {
            TraceWL3(format, args, false);
        }

        public void TraceWL3(string format, bool stackTracerCall)
        {
            TraceWL3(format, null, stackTracerCall);
        }

        public void TraceWL3(string format, object arg1, bool stackTracerCall)
        {
            TraceWL3(format, new object[] { arg1 }, stackTracerCall);
        }

        public void TraceWL3(string format, object arg1, object arg2, bool stackTracerCall)
        {
            TraceWL3(format, new object[] { arg1, arg2 }, stackTracerCall);
        }

        public void TraceWL3(string format, object arg1, object arg2, object arg3, bool stackTracerCall)
        {
            TraceWL3(format, new object[] { arg1, arg2, arg3 }, stackTracerCall);
        }

        public void TraceWL3(string format, object[] args, bool stackTracerCall)
        {
            _stackTracerCall = stackTracerCall; 
            string loggerMessage;
            try
            {
                if (!CanITrace())
                {
                    return;
                }

                if (string.IsNullOrEmpty(format))
                {
                    Trace("TraceWL3: format string Is Null Or Empty");
                    return;
                }

                if (args != null
                    && args.Length > 0)
                {
                    loggerMessage = string.Format(format, args);
                    Trace(loggerMessage);
                }
                else
                {
                    Trace(format);
                }
            }
            catch (System.Exception exception)
            {
                Trace(format);
                Trace(exception.ToString());
            }
        }

        public bool CanITrace()
        {
            try
            {
                return !string.IsNullOrEmpty(LogFileName) && !string.IsNullOrEmpty(Path.GetFileName(LogFileName));
            }
            catch (System.Exception exception)
            {
                Trace(exception.ToString());
                return false;
            }
        }
    }
}
