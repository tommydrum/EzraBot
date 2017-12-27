using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.DependencyInjection;
using TommydrumBot.Events;


namespace TommydrumBot
{
    class Program
    {
        private DiscordClient _discord;
        private static CommandsNextModule _commands;

        static void Main(string[] args)
            => new Program().MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // Retrieve token from token.txt
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

            // Configure Discord Session
            _discord = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            // Configure Commands
            _commands = _discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "::"
            });
            _commands.RegisterCommands<Commands>();

            // Connect bot to discord (includes authorization)
            await _discord.ConnectAsync();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}
