using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Aesthetics;
using FileIO;
using FaceDataDisplay;
using DebugTools;


namespace JARVIS
{
    static class Program
    {
        const string subscriptionKey = "2e77aaf63e4346a8b3820d03d396624b";

        // NOTE: Region must be 'westcentralus' for free/PAYGO keys
        const string uriBase =
            "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        static void Main()
        {
            Spacer sp = new Spacer('*');
            Header header = new Header(">>> NEW HEADER CLASS <<<");

            // Get the path and filename to process from the user.
            header.DisplayHeader();
            Console.Write(
                "Enter the path to an image with faces that you wish to analyze: ");
            string imageFilePath = @"C:\Users\Administrator\source\repos\JARVIS2\CSC438\faces\faceCollection.json"; //Console.ReadLine();
            Console.Write(imageFilePath); // DEBUG: skipping user input for now

            // DEBUG: Testing aux projects
            Console.WriteLine();
            sp.ShowSpacer('=');
            Console.WriteLine();

            Console.WriteLine("> Press any key to continue.");
            Console.ReadKey();
            XDebug xd = new XDebug();
            xd.ShowCheck();

            FileHandler fh = new FileHandler();
            FaceDisplayClass fd = new FaceDisplayClass();
            fd.DisplayFaceData();

            string fileText = fh.GetFile(imageFilePath);
            List<Face> faceList = JsonConvert.DeserializeObject<List<Face>>(fileText);
            Console.WriteLine("\n>>> First faceID in faceList: {0}", faceList[0].faceId);

            #region REST API Call
            //if (File.Exists(imageFilePath))
            //{
            //    // Execute the REST API call.
            //    try
            //    {
            //        Console.WriteLine("\nPlease wait a moment for the results to appear.\n");
            //        MakeAnalysisRequest(imageFilePath);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("\n" + e.Message + "\nPress Enter to exit...\n");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("\nInvalid file path.\nPress Enter to exit...\n");
            //}
            Console.ReadLine();
            #endregion
        }

        #region MakeAnalysisRequest()
        /// <summary>
        /// Gets the analysis of the specified image by using the Face REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file.</param>
        static async void MakeAnalysisRequest(string imageFilePath)
        {
            // DEBUG: Commenting out API call to reduce spamming API calls during testing.
            // Replacing with sample file of previous API response.
            FileHandler fh = new FileHandler();
            string fileText = fh.GetFile(imageFilePath);
            //fh.CreateFile(fileText);

            //Console.Write(JsonPrettyPrint(fileText));

            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json"
                // and "multipart/form-data".
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n");
                //string jsonStr = JsonPrettyPrint(contentString);
                //Console.WriteLine(jsonStr);

                // Parse and Display Selective JSON Response
                //Stuff JSON string return data from API into JSON object(s)
                List<Face> faceCollection = JsonConvert.DeserializeObject<List<Face>>(contentString);
                string faceFile = JsonConvert.SerializeObject(faceCollection);
                File.WriteAllText(@"C:\Users\Administrator\source\repos\JARVIS2\CSC438\faces\faceCollection", faceFile);

                // Viewing a few test cases.
                foreach (var face in faceCollection)
                {
                    Console.WriteLine("FACE_ID: {0}", face.faceId);
                    Console.WriteLine(face.faceAttributes.age);
                    foreach (var haircolor in face.faceAttributes.hair.hairColor)
                    {
                        Console.WriteLine(haircolor.color);
                        Console.WriteLine(haircolor.confidence);
                    }

                }

                // Exit
                Console.WriteLine("\nPress Enter to exit...");
            }
        }
        #endregion

        #region GetImageAsByteArray()
        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        #endregion

        #region JsonPrettyPrint()
        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        /// <example>static string JsonPrettyPrint(string json)</example>
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }
            string jsonStr = sb.ToString().Trim();
            return jsonStr;
        }
        #endregion
    }
}