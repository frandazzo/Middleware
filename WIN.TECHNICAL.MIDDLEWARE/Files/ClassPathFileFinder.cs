using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WIN.TECHNICAL.MIDDLEWARE.Files
{
    public class ClassPathFileFinder
    {
        public static bool ExistFileInExecutingAssemblyPathOrInAncestorFolder(string filename, string dirname)
        {
            if (String.IsNullOrEmpty(filename))
                return false;

            bool result = false;

            //verifico se esiste nel path corrente
            String path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            path = Path.GetDirectoryName(path);



            string file = Path.Combine(path, filename);
            if (File.Exists(file))
            {
               
               result = true;
            }



            if (result)
                return result;



            //provo adesso nei vari parant della directori corrente
            if (String.IsNullOrEmpty(dirname))
                return result;

            string ancestorFolder = GetParent(path, dirname);

            if (ancestorFolder == null)
                return result;

            file = Path.Combine(ancestorFolder, filename);

            if (File.Exists(file))
            {

                result = true;
            }


            return result;
        }


        private static string GetParent(string path, string parentName)
        {
            var dir = new DirectoryInfo(path);

            if (dir.Parent == null)
            {
                return null;
            }

            if (dir.Parent.Name == parentName)
            {
                return dir.Parent.FullName;
            }

            return GetParent(dir.Parent.FullName, parentName);
        }
    }
}
