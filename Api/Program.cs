using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var Word = "Example";
            var url = $"https://wordsapiv1.p.rapidapi.com/words/{Word}";

            var request = WebRequest.Create(url);
            request.Headers.Add("x-rapidapi-key", "7dc9c2102cmsh590a9a30e7366a2p186d71jsn428e1160ef63");
            request.Headers.Add("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var word = JsonConvert.DeserializeObject<Root>(result);
                foreach (var el in word.results)
                {
                    if (el.examples != null)
                    {
                        Console.WriteLine(el.examples[0]);
                    }
                }
            }
            Console.ReadKey();
        }
    }
}