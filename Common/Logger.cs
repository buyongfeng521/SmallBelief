using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public static class Logger
    {
        private static Type _threadAbortType = typeof(ThreadAbortException);

        //Temp
        //public static void HandleException(Exception e)
        //{
        //    HandleException(e, 0, true, true);
        //}

        public static void HandleException(Exception e, int errorCode)
        {
            HandleException(e, errorCode, true, true);
        }

        //Temp
        //public static void HandleException(Exception e, bool isWriteFile, bool isSendMail)
        //{
        //    HandleException(e, 0, "", isWriteFile, isSendMail);
        //}

        public static void HandleException(Exception e, int errorCode, bool isWriteFile, bool isSendMail)
        {
            HandleException(e, errorCode, "", isWriteFile, isSendMail);
        }

        //Temp
        //public static void HandleException(Exception e, string message)
        //{
        //    HandleException(e, 0, message);
        //}

        public static void HandleException(Exception e, int errorCode, string message)
        {
            HandleException(e, errorCode, message, true, true);
        }

        //Temp
        //public static void HandleException(Exception e, string message, bool isWriteFile, bool isSendMail)
        //{
        //    HandleException(e, 0, message, isWriteFile, isSendMail);
        //}

        public static void HandleException(Exception e, int errorCode, string message, bool isWriteFile, bool isSendMail)
        {
            try
            {
                string code = errorCode.ToString().PadLeft(4, '0');

                string content = "Exception ---- " + code + " \r\n";
                content += "\t Code:" + code + "\r\n";
                content += "\t Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
                content += "\t ApplicationName:" + Logger.GetApplicationName() + "\r\n";
                content += "\t ApplicationPath:" + AppDomain.CurrentDomain.BaseDirectory + "\r\n";
                content += "\t MachineName:" + Environment.MachineName + "\r\n";
                content += "\t UserDomain:" + Environment.UserDomainName + "\r\n";
                content += "\t UserName:" + Environment.UserName + "\r\n";
                content += "\t Information:" + message + "\r\n";
                content += "\t Message:" + e.Message + "\r\n";
                content += "\t Source:" + e.Source + "\r\n";
                content += "\t StackTrace:" + e.StackTrace + "\r\n\r\n";

                if (isWriteFile)
                    OutPut.WriteFile(content);


            }
            catch
            {

            }
        }


        public static void WriteLog(string content)
        {
            content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t " + content + "\r\n";

            OutPut.WriteFile(content);

        }

        public static void WriteExceptionLog(Exception exception, int errorCode)
        {
            WriteExceptionLog(exception, errorCode, exception.Message);
        }

        public static void WriteExceptionLog(Exception exception)
        {
            WriteExceptionLog(exception, 0, exception.Message);
        }

        public static void WriteExceptionLog(Exception exception, int errorCode, string message)
        {
            List<string> fileNameList = new List<string>();
            string currentFileName = DateTime.Now.ToString("yyyy-MM-dd-HH") + ".txt";
            string excetptionLogFileName = "exceptionLog_" + currentFileName;
            fileNameList.Add(currentFileName);
            fileNameList.Add(excetptionLogFileName);
            WriteExceptionLog(exception, errorCode, message, fileNameList, true, true);
        }

        public static void WriteExceptionLog(Exception e, int errorCode, string message,
            List<string> fileNameList, bool isWriteFile, bool isSendMail)
        {
            if (fileNameList == null || fileNameList.Count == 0)
            {
                return;
            }

            try
            {
                string code = errorCode.ToString().PadLeft(4, '0');

                string content = "Exception ---- " + code + " \r\n";
                content += "\t Code:" + code + "\r\n";
                content += "\t Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
                content += "\t ApplicationName:" + Logger.GetApplicationName() + "\r\n";
                content += "\t ApplicationPath:" + AppDomain.CurrentDomain.BaseDirectory + "\r\n";
                content += "\t MachineName:" + Environment.MachineName + "\r\n";
                content += "\t UserDomain:" + Environment.UserDomainName + "\r\n";
                content += "\t UserName:" + Environment.UserName + "\r\n";
                content += "\t Information:" + message + "\r\n";
                content += "\t Message:" + e.Message + "\r\n";
                content += "\t Source:" + e.Source + "\r\n";
                if (e.InnerException != null)
                {
                    content += "\t InnerException.Message:" + e.InnerException.Message + "\r\n";
                }
                content += "\t StackTrace:" + e.StackTrace + "\r\n\r\n";

                if (isWriteFile)
                {
                    foreach (string fileName in fileNameList)
                    {
                        OutPut.WriteFile(fileName, content);
                    }
                }


            }
            catch
            {

            }
        }


        /// <summary>
        /// 写支付日志
        /// </summary>
        /// <param name="log"></param>
        public static void WritePayLog(string log)
        {
            log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t " + log + "\r\n";

            List<string> fileNameList = new List<string>();
            string currentFileName = DateTime.Now.ToString("yyyy-MM-dd-HH") + ".txt";
            string payLogFileName = "pay_" + currentFileName;
            fileNameList.Add(currentFileName);
            fileNameList.Add(payLogFileName);
            log = log + "\r\n";
            foreach (string fileName in fileNameList)
            {
                OutPut.WriteFile(fileName, log);
            }
        }


        private static string GetApplicationName()
        {
            string name = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
            if (new FileInfo(name).Extension.ToLower().Replace(".", "").Equals("exe"))
                return name;
            else
                return "";
        }


    }
}
