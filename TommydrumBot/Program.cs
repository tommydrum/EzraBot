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
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;


namespace TommydrumBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;

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

            _services = new ServiceCollection()
                .BuildServiceProvider();

            await InstallCommands();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        public async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            // Make sure author of msg is authorized to run commands
            if (messageParam.Author.Username != "tommydrum")
            {
                await messageParam.Channel.SendMessageAsync("Must be bot owner to run a command!");
                return;
            }
            // Create a Command Context
            var context = new CommandContext(_client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            //var result = await _commands.ExecuteAsync(context, argPos, _services);
            await _commands.ExecuteAsync(context, argPos, _services);
            //if (!result.IsSuccess)
            //{
            //    await context.Channel.SendMessageAsync(result.ErrorReason);
            //    await Log(new LogMessage(LogSeverity.Warning, messageParam.Author.Username.ToString(),
            //        messageParam.Content + ": Error processing this command", null));
            //}
            //else
            //{
            //    await Log(new LogMessage(LogSeverity.Info, messageParam.Author.Username.ToString(),
            //        messageParam.Content, null));
            //}
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.FromResult(false);
        }

    }
}
