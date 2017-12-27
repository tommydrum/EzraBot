using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Ezra.Commands
{
    class PingModule
    {
        int counter = 0;

        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            if (counter++ > 3)
            {
                await ctx.RespondAsync("(╯-_-）╯︵ ┻━┻");
                counter = 0;
            }
            else
                await ctx.RespondAsync("...");
        }
    }
}
