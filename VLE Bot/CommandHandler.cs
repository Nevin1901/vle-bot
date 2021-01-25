using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Discord.Commands;
using Discord.WebSocket;

namespace VLE_Bot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;

        private readonly CommandService _commands;

        private readonly IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client, CommandService command, IServiceProvider services)
        {
            _client = client;
            _commands = command;
            _services = services;
        }



        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _services);

            Console.WriteLine("Installed Commands");
    
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {

            Console.WriteLine("Message Recieved");

            var message = messageParam as SocketUserMessage;

            if (message == null) return;

            int argPos = 0;

            if (!message.HasCharPrefix('!', ref argPos)) return;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(context: context, argPos: argPos, services: _services);

        }
    }
}
