using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace VLE_Bot
{
    class Program
    {

        private DiscordSocketClient _client;

        //private readonly BotStatus _botStatus = new BotStatus("Google Meets", "Test Description", ActivityType.Playing, ActivityProperties.None);

        private BotInfo _botInfo;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {

            using (System.IO.StreamReader reader = new StreamReader(@"C:\Users\Nevin\source\repos\VLE Bot\config.json"))
            { 
                string jsonObject = await reader.ReadToEndAsync();

                _botInfo = JsonConvert.DeserializeObject<BotInfo>(jsonObject);

                Console.WriteLine(_botInfo.Token);

            }


        
            _client = new DiscordSocketClient();


            await _client.LoginAsync(TokenType.Bot, _botInfo.Token);

            await _client.StartAsync();

            Console.WriteLine("Logged In");

            await _client.SetActivityAsync(_botInfo.BotStatus);

            await Task.Delay(-1);

            Console.WriteLine("Hello");
        }

    }
}
