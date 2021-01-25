using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using VLE_Bot.Models;

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
            IEnumerable<SchoolClass> allClasses = await DatabaseTools.GetAllClasses(_botInfo);
            string classOutput = "--- Classes ---\n";
            foreach (SchoolClass cClass in allClasses)
            {
                classOutput += $"{cClass.ClassName} - {cClass.ClassLink}\n";
            }

            classOutput += "<@&803327093285978123>";



            await ReplyAsync(classOutput);


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
