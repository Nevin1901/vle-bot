using Discord.Commands;
using System.Threading.Tasks;
using RestSharp;

namespace VLE_Bot
{
    public class TextInflateModule : ModuleBase<SocketCommandContext>
    {
        private readonly BotInfo _botInfo;

        public TextInflateModule(BotInfo botInfo)
        {
            _botInfo = botInfo;
        }

        [Command("inflate")]
        [Summary("inflates text passed along")]
        public async Task InflateTextAsync([Remainder] string text)
        {
            var client = new RestClient("http://www.textinflator.com/php/inflatePhpTag.php");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"desperation=100&text={text}&output=iob", ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                await Context.Channel.SendMessageAsync("Error");
                return;
            }
            else
            {
                await Context.Channel.SendMessageAsync(response.Content);
                return;
            }
        }
    }
}