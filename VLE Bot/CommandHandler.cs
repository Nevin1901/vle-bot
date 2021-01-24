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
    class CommandHandler
    {
        private readonly DiscordSocketClient _client;

        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);

        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;

            if (message == null) return;

            int argPos = 0;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(context: context, argPos: argPos, services: null);

        }
    }
}
