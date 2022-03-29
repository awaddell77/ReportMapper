using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ReportManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportMTest.getUserConfigs("reportconfigs.xml");
        }
    }
    class ReportMTest: ReportManager
    {
        
    }
    abstract class ReportManager
    {
        List<string> errorDump;
        public static Dictionary<string, string> getUserConfigs(string pathToConfigFile)
        {
            Dictionary<string, string> userConfigs = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(pathToConfigFile);
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodeList;
            string fileprefix;
            string archiveDirLoc;
            string archiveDirName;
            string targetFileLoc;
            string outFileName;
            string outFileDir;
            try
            {
                targetFileLoc = root.SelectNodes("/configuration/TargetFileLoc")[0].InnerText;
                fileprefix = root.SelectNodes("/configuration/TargetFilePrefix")[0].InnerText;
                archiveDirLoc = root.SelectNodes("/configuration/ArchiveDirectoryLoc")[0].InnerText;
                archiveDirName = root.SelectNodes("/configuration/ArchiveDirectoryName")[0].InnerText;
                outFileName = root.SelectNodes("/configuration/OutputFileName")[0].InnerText;
                outFileDir = root.SelectNodes("/configuration/OutputFileName")[0].InnerText;
            }
            catch (System.Xml.XPath.XPathException ex) 
            {
                throw;
            }
            Console.WriteLine("FilePrefix: " + fileprefix);
            Console.WriteLine("File Location:" + targetFileLoc + "end");
            Console.WriteLine(targetFileLoc == "");
            userConfigs.Add("TargetFileLoc", targetFileLoc);
            userConfigs.Add("TargetFilePrefix", fileprefix);
            userConfigs.Add("ArchiveDirLoc", archiveDirLoc);
            userConfigs.Add("ArchiveDirName", archiveDirName);
            userConfigs.Add("OutFileName", outFileName);
            userConfigs.Add("OutFileDir", outFileDir);
            //XmlReader xmlReader = XmlReader.Create(pathToConfigFile);
            string xmlvalue = "";
            Console.Read();
            // Dictionary<string, string> userConfigs = JsonConvert.DeserializeObject<Dictionary<string, string>>(lines);
            //return userConfigs;
            return parseUserConfigs(userConfigs); //only here so that it compiles during testing
        }
        protected static Dictionary<string, string> parseUserConfigs(Dictionary<string, string> userConfigs)
        {
            //contains no default implementation
            //this is where it decides how to deal with empty fields 
            //can use this method to set defaults
            return userConfigs;
        }
        protected static void createArchiveDirectory(string pathToFolder,string folderName)
        {
            string tempdir = Path.Combine(new string[] { pathToFolder, folderName} );
            string tempFolderName = folderName;
            string[] tempDirArr = new string[folderName.Split('\\').Length - 2];
            //if foldername contains full directory it extracts the name
            if (folderName.Contains("\\"))
            {

                tempFolderName = folderName.Split('\\')[folderName.Split('\\').Length - 2];
                Array.Copy(folderName.Split('\\'), tempDirArr, tempDirArr.Length);
                tempdir = string.Join("\\", tempDirArr);
            }
            string targetDirPath;
            string[] tempArr;
            tempArr = Directory.GetDirectories(tempdir, tempFolderName);
            if (tempArr.Length == null || tempArr.Length == 0)
            {
                Directory.CreateDirectory(tempdir + "\\" + tempFolderName);
            }
            targetDirPath = Directory.GetCurrentDirectory() + "\\" + folderName;




        }
        



    }
}
