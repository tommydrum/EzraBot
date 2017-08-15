using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Audio.Streams;
using Discord.Commands;

namespace TommydrumBot
{
    public class VoiceModule : ModuleBase
    {
        private Task<IAudioClient> audioTask;
        private List<Thread> _threads = new List<Thread>();
        private IAudioClient audioClient;
        [Command("join"), Description("Joins a voice channel and starts recording.")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await ReplyAsync("You must be in a voice channel before I will join."); return; }

 //           var audioClient = await channel.ConnectAsync();
            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            Thread t = new Thread(Record);
            t.Start(channel);
            _threads.Add(t);
            await audioTask;
        }

        private async void Record(object ochannel)
        {
            IVoiceChannel channel = ochannel as IVoiceChannel;
            audioTask = channel.ConnectAsync();
            audioClient = await audioTask;
//            var audioStream = audioClient.CreateDirectOpusStream().
        }

        [Command("disconnect"), Description("Disconnects from voice channel")]
        public async Task DisconnectChannel(IVoiceChannel channel = null)
        {
            //audioClient
            //audioClient.StopAsync();
        }
    }
}
