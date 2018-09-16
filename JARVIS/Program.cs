using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Newtonsoft.Json;
using System.Collections.Generic;
using Aesthetics;
using FileIO;
using FaceDataDisplay;
using DebugTools;


namespace JARVIS
{
    static class Program
    {
#if DEBUG
        private static readonly bool RunDebugCode = true;
#endif
        const string subscriptionKey = "2e77aaf63e4346a8b3820d03d396624b";

        // NOTE: Region must be 'westcentralus' for free/PAYGO keys
        const string uriBase =
            "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        static void Main()
        {
            Spacer sp = new Spacer('*');

            // Get the path and filename to process from the user.
            sp.DisplayHeader("DETECT FACES");
            Console.Write(
                "Enter the path to an image with faces that you wish to analyze: ");
            string imageFilePath = "H:/UAT/2018/FALL/pipe.jpg"; //Console.ReadLine();

            // DEBUG: Testing aux projects
            Console.WriteLine();
            sp.ShowSpacer('=');
            Console.WriteLine();
            FileHandler fh = new FileHandler();
            FaceDisplayClass fd = new FaceDisplayClass();
            fh.DisplayFile();
            fd.DisplayFaceData();

            Console.WriteLine("> Press any key to continue.");
            Console.ReadKey();
            XDebug xd = new XDebug();
            xd.ShowCheck();

            #region REST API Call
            //if (File.Exists(imageFilePath))
            //{
            //    // Execute the REST API call.
            //    try
            //    {
            //        MakeAnalysisRequest(imageFilePath);
            //        Console.WriteLine("\nWait a moment for the results to appear.\n");
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
            //Console.ReadLine();
            #endregion
        }

        #region MakeAnalysisRequest()
        /// <summary>
        /// Gets the analysis of the specified image by using the Face REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file.</param>
        static async void MakeAnalysisRequest(string imageFilePath)
        {
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
                Console.WriteLine(JsonPrettyPrint(contentString));

                // Parse and Display Selective JSON Response
                //Stuff JSON string return data from API into JSON object(s)
                var faceCollection = JsonConvert.DeserializeObject<List<Face>>(contentString);

                // Viewing a few test cases.
                foreach (var face in faceCollection)
                {
                    Console.WriteLine(face.faceId);
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

            return sb.ToString().Trim();
        }
        #endregion
    }
}