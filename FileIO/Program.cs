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

        public void DisplayFile(FileHandler file)
        {
            Console.WriteLine(file);
        }

        public void CreateFile(string fileTxt)
        {
            //File.WriteAllText(@"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentString.txt", fileTxt);
            string fileJSON = JsonConvert.SerializeObject(fileTxt);
            Console.WriteLine(">> fileJSON is: {0}", fileJSON);
            File.WriteAllText(@"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentJSON.json", fileJSON);

            //////////////////////////////////////////////////
            string path = @"C:\Users\Administrator\source\repos\JARVIS2\CSC438\contentJSON.json";
            
            string[] previousFile = File.ReadAllLines(path);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < previousFile.Length; i++)
            {
                sb.Append(previousFile[i]);
            }
            Console.WriteLine(">> StringBuilder sb is: {0}", sb);
            //var jo = JObject.Parse(sb.ToString());
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
