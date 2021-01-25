using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace VLE_Bot
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        private readonly BotInfo _botInfo;
        public InfoModule(BotInfo botInfo)
        {
            _botInfo = botInfo;
        }

        [Command("say")]
        [Summary("Echoes a Message")]
        public async Task SayASync([Summary("The text to echo")] string echo, int amount = 1)
        {
            if (amount > 10 || amount < 0)
            {
                await ReplyAsync($"{Context.User.Mention}, You can't do it more than 10 times. {_botInfo.ConnectionString}");
                return;
            }
            for (int i = 0; i < amount; i++)
            {
                await Context.Channel.SendMessageAsync($"{echo}");
            }
        }

    }
}
