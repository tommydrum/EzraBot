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
    public static class CommandBase
    {
        public static CommandsNextModule SetupCommands(DiscordClient discord)
        {
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = ""
            });
            discord.DebugLogger.LogMessage(LogLevel.Info, "Ezra", "Registered prefix", DateTime.Now);

            // Register all the modules!
            commands.RegisterCommands<PingModule>();

            discord.DebugLogger.LogMessage(LogLevel.Info, "Ezra", "Registered all commands", DateTime.Now);
            return commands;
        }
    }
}
