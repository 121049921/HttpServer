using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Uitl
{
    public enum LogType
    {
        [Description("Info")]
        Info = 1,
        [Description("Error")]
        Error = 2,
        [Description("Debug")]
        Debug = 3
    }

    public class Log
    {


        private static object locker = new object();
        private static Log instance;
        private static string logFileDir;

        public static Log Instance
        {
            get
            {

                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new Log();

                            
                            InitLogDir();
                        }
                    }
                }
             
                return instance;
            }
            set => instance = value;
        }


        private static void InitLogDir()
        {
            logFileDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
        }

        public void Info(string message)
        {
            Writer(LogType.Info.ToString(), message);

        }

        public void Error(string message, Exception ex = null)
        {
            Writer(LogType.Error.ToString(), message);
        }

        public void Debug(string logType, string message)
        {
            Writer(LogType.Debug.ToString(), message);
        }

        public void Writer(string logType, string message)
        {
            if (!Directory.Exists(logFileDir))
            {
                Directory.CreateDirectory(logFileDir);
            }
            string sffName = DateTime.Now.ToString("yyyy-MM-dd");
            string logFiledName = string.Concat(sffName, "Log.txt");
            string logFileFullPath = Path.Combine(logFileDir, logFiledName);
            if (!File.Exists(logFileFullPath))
            {
                File.Create(logFileFullPath).Close();
            }

            try
            {
                using (FileStream fs = new FileStream(logFileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")).AppendLine();
                        sb.Append(logType).AppendLine();
                        sb.Append(message);
                        sb.AppendLine();
                        sb.AppendLine("===========================================================================");
                        sb.AppendLine().AppendLine();
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
            }
            catch (IOException ex)
            {


            }
            finally
            {

            }
        }
    }
}







