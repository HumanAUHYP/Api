using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Telegram.Bot;

namespace Api
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient("1806549539:AAGFkzLCXqDLNTNSwXEHblm-kDffkNDJYao");


            bot.OnMessage += (s, arg) =>
            {
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                Api(arg.Message.Text, arg.Message.Chat.Id, bot);
            };

            bot.StartReceiving();
            Console.ReadKey();
        }

        public static void Api(string Word, long botID, TelegramBotClient bot)
        {
            var url = $"https://wordsapiv1.p.rapidapi.com/words/{Word}";

            var request = WebRequest.Create(url);
            request.Headers.Add("x-rapidapi-key", "7dc9c2102cmsh590a9a30e7366a2p186d71jsn428e1160ef63");
            request.Headers.Add("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");

            try
            {
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
                            bot.SendTextMessageAsync(botID, el.examples[0]);
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", e.Status);
                bot.SendTextMessageAsync(botID, "Invalid word, try again!");
            }
            catch (Exception)
            {
                Console.WriteLine("Exeption");
            }
        }
    }
}