using System;
using System.ComponentModel.Design;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace VLE_Bot
{
    class Program
    {

        private DiscordSocketClient _client;

        private readonly CommandService _commands = new CommandService(new CommandServiceConfig {CaseSensitiveCommands = false});


        //private readonly BotStatus _botStatus = new BotStatus("Google Meets", "Test Description", ActivityType.Playing, ActivityProperties.None);

        public BotInfo _botInfo;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            if (!File.Exists(@"config.json"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No Config File. Create one following config.def.h");
                Environment.Exit(0);
            }
            using (System.IO.StreamReader reader = new StreamReader(@"config.json"))
            { 
                string jsonObject = await reader.ReadToEndAsync();
                _botInfo = JsonConvert.DeserializeObject<BotInfo>(jsonObject);
            }

            _client = new DiscordSocketClient();
            
            // make it so that the bot info can be passed as an argument to the command classes
            IServiceProvider services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).AddSingleton(_botInfo).BuildServiceProvider();

            // initialize commands
            CommandHandler commandHandler = new CommandHandler(_client, _commands, services);
            await commandHandler.InstallCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, _botInfo.Token);
            await _client.StartAsync();

            Console.WriteLine("Logged In");

            var allGuilds = _client.DMChannels;
            foreach (var guild in allGuilds)
            {
                Console.WriteLine(guild.Recipient);
            }

            await _client.SetActivityAsync(_botInfo.BotStatus);

            //Task.Run(() => CheckTime());

            await Task.Delay(-1);

            Console.WriteLine("Hello");
        }

        public async Task CheckTime()
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));

                int hi =_client.DMChannels.Count;

                Console.WriteLine($"{hi} Nevin");

                Thread.Sleep(5000);
            }
        }


    }
}
