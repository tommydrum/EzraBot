using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;


namespace TommydrumBot
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            string token;
            try
            {
                token = File.ReadAllText("token.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.FromResult(false);
        }

        private async Task MessageReceived(SocketMessage msg)
        {
            switch(msg.Content)
            {
                case "!ping":
                    await ping(msg);
                    break;
                case "!commands":
                    await commands(msg);
                    break;
                case "!help":
                    await commands(msg);
                    break;
                default:
                    return;
            }
            await Log(new LogMessage(LogSeverity.Info, msg.Author.Username.ToString(), msg.Content, null));
        }

        private async Task ping(SocketMessage msg)
        {
            await msg.Channel.SendMessageAsync("Pong!");
        }
        private async Task commands(SocketMessage msg)
        {
            string rtnMsg = "Commands:" + _nl;
            rtnMsg += "```";
            //begin commands
            rtnMsg += cmdlet("!ping", "Responds back that it's alive");
            rtnMsg += cmdlet("!commands", "Displays this help message");
            rtnMsg += cmdlet("!help", "Also displays this help message");
            //end commands
            rtnMsg += "```";
            await msg.Channel.SendMessageAsync(rtnMsg);
        }
        private string cmdlet(string cmd, string desc)
        {
            string format = "{0,-20} {1,-10}";
            string rtn = string.Format(format, cmd, desc);
            rtn += _nl;
            return rtn;
        }
        static string _nl = System.Environment.NewLine; //Make life slightly easier.
    }
}
