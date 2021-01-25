using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;

namespace VLE_Bot
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        private readonly BotInfo _botInfo;
        public InfoModule(BotInfo botInfo)
        {
            _botInfo = botInfo;
        }

        [Command("test")]
        [Summary("Echoes a Message")]
        public async Task SayASync()
        {
            IEnumerable<Temps> temps = await DatabaseTools.GetAllPeople(_botInfo);
            string outputTemps = "";
            foreach (Temps temp in temps)
            {
                outputTemps += $"{temp.City} - {temp.Temp}\n";
            }

            await ReplyAsync($"{Context.User.Mention}, {outputTemps}");


            /*
            if (amount > 10 || amount < 0)
            {
                await DatabaseTools.GetAllPeople(_botInfo);
                await ReplyAsync($"{Context.User.Mention}, You can't do it more than 10 times.");
                return;
            }
            */
            /*
            for (int i = 0; i < amount; i++)
            {
                await Context.Channel.SendMessageAsync($"{echo}");
            }
            */

        }

    }
}
