using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using VLE_Bot.Models;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Auth.OAuth2;

namespace VLE_Bot.Modules
{
    public class UploadModule : ModuleBase<SocketCommandContext>
    {
        private BotInfo _botInfo;
        public UploadModule(BotInfo botInfo)
        {
            _botInfo = botInfo;
        }
        [Command("addsong")]
        [Summary("Uploads a song using the provided song and image")]
        public async Task UploadSong()
        {
            IReadOnlyCollection<Attachment> attachments = Context.Message.Attachments;
            if (attachments.FirstOrDefault() != null)
            {
                int result = await DatabaseTools.AddSong(_botInfo, attachments.FirstOrDefault().Url);
                await Context.Channel.SendMessageAsync($"{result} - Result of operation");
            }
            else
                await Context.Channel.SendMessageAsync("Error: Please specify a song");
        }

        [Command("addimage")]
        [Summary("adds an image to the specified song")]
        public async Task AddImage()
        {
            IReadOnlyCollection<Attachment> attachments = Context.Message.Attachments;
            if (attachments.FirstOrDefault() != null)
            {
                int result = await DatabaseTools.AddImage(_botInfo, attachments.FirstOrDefault().Url);
                await Context.Channel.SendMessageAsync($"{result} - Result of operation");
            }
            else
                await Context.Channel.SendMessageAsync("Error: Please specify an image");
        }

        [Command("checkupload")]
        [Summary("checks the files to be uploaded onto youtube")]
        public async Task CheckUpload()
        {
            Audio uploadedFiles = await DatabaseTools.CheckUpload(_botInfo);
            await Context.Channel.SendMessageAsync(
                $"{uploadedFiles.songlink} - song URL\n{uploadedFiles.imagelink} - image URL");
        }

        [Command("upload")]
        [Summary("uploads song with image to youtube")]
        public async Task Upload()
        {
            Audio audioData = await DatabaseTools.CheckUpload(_botInfo);
            Uri songData = new Uri(audioData.songlink);
            Uri imageData = new Uri(audioData.imagelink);
            using (var client = new WebClient())
            {
                /*
                await Context.Channel.SendMessageAsync("Downloading song");
                await client.DownloadFileTaskAsync(songData, "song.mp3");
                await Context.Channel.SendMessageAsync("Downloading Image");
                await client.DownloadFileTaskAsync(imageData, "image.jpg");
                await Context.Channel.SendMessageAsync("Downloaded Both Files");
                */
                ClientSecrets secrets = new ClientSecrets()
                {
                    ClientId = "nope",
                    ClientSecret = "no sir"
                };
                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets,
                    new[] {YouTubeService.Scope.YoutubeUpload}, "user", CancellationToken.None);
                await Context.Channel.SendMessageAsync("Done");
            }
        }
    }
}
