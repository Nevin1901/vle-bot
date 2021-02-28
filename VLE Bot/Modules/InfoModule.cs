using System;
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

        [Command("classes")]
        [Summary("Echoes a Message")]
        public async Task GetClassesAsync()
        {
            IEnumerable<SchoolClass> allClasses = await DatabaseTools.GetAllClasses(_botInfo);
            string classOutput = "--- Classes ---\n";
            foreach (SchoolClass cClass in allClasses)
            {
                classOutput += $"{cClass.ClassName} - <{cClass.ClassLink}>\n";
            }

            classOutput += "<@&803327093285978123>";

           // IEmote emote = new Emoji("\u23EB");
           // await message.AddReactionAsync(emote);

            await Context.Channel.SendMessageAsync(classOutput);

            await GetWeekAsync();

            await Context.Channel.SendMessageAsync(
                "https://cdn.discordapp.com/attachments/791777084359573544/815688482780741722/unknown.png");


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

        [Command("week")]
        [Summary("Gets the current week")]
        public async Task GetWeekAsync()
        {
            int currentWeek = await DatabaseTools.GetCurrentWeek(_botInfo);
            if (currentWeek == 1) await ReplyAsync("It is currently A Week. ASH Goes 3 times");
            else if (currentWeek == 2) await ReplyAsync("It is currently B Week. ASH Goes 2 times");
            else await ReplyAsync("An error has occurred");
        }

        [Command("addclass")]
        [Summary("Adds a class")]
        public async Task AddClassAsync(string className = "", string classLink = "")
        {
            if (className == "" || classLink == "")
            {
                await ReplyAsync("Usage: !addclass className classLink");
            }
            else
            {
                int result = await DatabaseTools.AddClass(_botInfo, className, classLink);
                await ReplyAsync($"{result} - Result of operation");
            }
        }

    }
}
