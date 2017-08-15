using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace TommydrumBot
{
    public class PingModule : ModuleBase
    {
        [Command("ping"), Summary("Responds to a ping with a pong")]
        public async Task Ping()
        {
            await ReplyAsync("Pong!");
        }

        [Command("stop"), Summary("Stops Bot Service")]
        public async Task Stop()
        {
            await ReplyAsync("Stopping Discord Bot");
            Environment.Exit(0);
        }
    }
}
