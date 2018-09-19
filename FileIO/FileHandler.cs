using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileIO
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");
    //    }
    //}

    public class FileHandler
    {
        #region Constructors
        public FileHandler() { }
        #endregion

        #region Methods
        public void DisplayFile()
        {
            Console.WriteLine("> FileHandler class check.");
        }

        public void DisplayFile(string file)
        {
            Console.WriteLine(file);
        }

        public void CreateFile(string fileTxt)
        {
            //File.WriteAllText(@"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentString.txt", fileTxt);
            //string fileJSON = JsonConvert.SerializeObject(fileTxt);
            //Console.WriteLine("\n>> fileJSON is: {0}", fileJSON);
            //File.WriteAllText(@"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentJSON.json", fileJSON);
            File.WriteAllText(@"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentJSON.json", fileTxt);

            //var jo = JObject.Parse(fileTxt);
            //var faceID = jo["faceID"].ToString();
            //Console.WriteLine("\n> facedID: {0}", faceID);
            //////////////////////////////////////////////////
            string path = @"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentJSON.json";
            
            string[] previousFile = File.ReadAllLines(path);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < previousFile.Length; i++)
            {
                sb.Append(previousFile[i]);
            }
            Console.WriteLine("\n>> StringBuilder sb is: {0}", sb);
            //////////////////////////////////////////////////

            //var faceID = jo["faceID"].ToString();
            Console.WriteLine("********************************************");
            Console.WriteLine(">>> FaceID: {0}", "faceID");
            Console.WriteLine("********************************************");
        }

        public string GetFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string fileText = sr.ReadToEnd();
            sr.Close();

            //DEBUG:
            string[] fileNameParts = fileName.Split('\\');
            int fLen = fileNameParts.Length;
            Console.WriteLine("\n> Retrieved file: /{0}/{1}", fileNameParts[fLen - 2], fileNameParts[fLen -1]);
            return fileText;
        }

        //public string GetFile(string fileName)
        //{
        //    StreamReader sr = new StreamReader(fileName);
        //    string fileText = sr.ReadToEnd();
        //    sr.Close();

        //    return fileText;
        //}
        #endregion
    }
}
