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
using Ezra.Commands;

namespace Ezra
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;

        static void Main(string[] args)
        {
            MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync()
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
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            // Configure Commands
            commands = CommandBase.SetupCommands(discord);

            // Create non-command responces
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.Contains("<@395374670599421952>"))
                    await e.Message.RespondAsync("no.");
            };

            // Connect bot to discord (includes authorization)
            await discord.ConnectAsync();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}
