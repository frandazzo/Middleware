using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class QueueLogger
    {

        private string _errorLogDir = "";
        private string _logFile = "";


        public QueueLogger(string folderName, string existingLogDir)
        {
            if (String.IsNullOrEmpty(folderName))
                throw new ArgumentNullException("folderName");

            if (String.IsNullOrEmpty(existingLogDir))
                throw new ArgumentNullException("existingLogDir");
            //recupero la directory appdata
            string appfFolder = existingLogDir;

            //string foldername = String.Format("{0}-{1}-{2}-noesis-queue-recevier", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string folder = folderName;
            string errorLogDir = Path.Combine(appfFolder, folder);



            if (Directory.Exists(errorLogDir))
                _errorLogDir = errorLogDir;
            else
            {
                try
                {
                    Directory.CreateDirectory(errorLogDir);
                }
                catch (Exception)
                {
                    _errorLogDir = appfFolder;
                }

            }


            //adesso creo il file di log nella cartella 
            string fname = String.Format("{0}-{1}-{2}-message-rec.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            _logFile = Path.Combine(_errorLogDir, fname);
        }

        public QueueLogger(string folderName)
        {
            if (String.IsNullOrEmpty(folderName))
                throw new ArgumentNullException();
            //recupero la directory appdata
            string appfFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //string foldername = String.Format("{0}-{1}-{2}-noesis-queue-recevier", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string folder = folderName;
            string errorLogDir = Path.Combine(appfFolder, folder);

            

            if (Directory.Exists (errorLogDir ))
                _errorLogDir = errorLogDir;
            else
            {
                try
                {
                    Directory.CreateDirectory(errorLogDir);
                }
                catch (Exception)
                {
                    _errorLogDir = appfFolder;
                }
                
            }


            //adesso creo il file di log nella cartella 
            string fname = String.Format("{0}-{1}-{2}-message-rec.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            _logFile = Path.Combine(_errorLogDir, fname);
        }


      
        public void AddMessage(string message)
        {
            StringBuilder b = new StringBuilder();
            b.AppendLine("");
            b.AppendLine(String.Format("Log del {0}-{1}", DateTime.Now.ToString(), message));


            File.AppendAllText(_logFile,b.ToString());
        }


    }
}
