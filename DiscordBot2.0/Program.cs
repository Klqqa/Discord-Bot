using Discord;
using Discord.WebSocket;
using Discord.Audio;
using System.Diagnostics;
using YoutubeExplode;
using AngleSharp.Dom;
using System.IO;

namespace DiscordBot2._0
{
    internal class Program
    {
        private static async Task Main(string[] args) => await new Bot().StartBot();

        public class Bot
        {
            private readonly DiscordSocketClient _client;
            private static BotConfig _config;
            private static Embeds _embed;
            public Bot()
            {
                var config = new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.All
                };
                _client = new DiscordSocketClient(config);
                _client.Log += LogAsync;
                _client.Ready += ReadyAsync;
                _client.MessageReceived += MessageReceivedAsync;
                _client.UserLeft += UserLeftAsync;
                _client.UserJoined += UserJoinedAsync;
                _client.InteractionCreated += HandleInteractionAsync;
            }
            public async Task StartBot()
            {
                try
                {
                    _config = ConfigLoader.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading config: {ex.Message}");
                    return;
                }
                await _client.LoginAsync(TokenType.Bot, _config.Token);
                await _client.StartAsync();
                await Task.Delay(-1);
            }
            private Task LogAsync(LogMessage log)
            {
                Console.WriteLine(log);
                return Task.CompletedTask;
            }
            private Task ReadyAsync()
            {
                Console.WriteLine("Бот подключен и готов к работе!");
                _embed = new Embeds();
                _embed.RolesEmbedAsync(_client, _config.channelRoleId);
                return Task.CompletedTask;
            }
            private async Task MessageReceivedAsync(SocketMessage message)
            {
                if (message.Author.IsBot)
                    return;

                var userMessage = message as SocketUserMessage;
                var channel = message.Channel;

                if (userMessage.Content.StartsWith("!play"))
                {
                    var args = userMessage.Content.Split(' ', 2);
                    if (args.Length < 2)
                    {
                        await channel.SendMessageAsync("Укажите ссылку или путь к аудиофайлу после команды `!play`.");
                        return;
                    }

                    var urlOrPath = args[1];
                    var voiceChannel = (message.Author as SocketGuildUser)?.VoiceChannel;

                    if (voiceChannel == null)
                    {
                        await channel.SendMessageAsync("Вы должны быть в голосовом канале, чтобы использовать эту команду!");
                        return;
                    }

                    try
                    {
                        var audioClient = voiceChannel.ConnectAsync(); // Блокирующий вызов
                        await voiceChannel.DisconnectAsync();
                        await channel.SendMessageAsync($"Воспроизведение началось: {urlOrPath}");
                    }
                    catch (Exception ex)
                    {
                        await channel.SendMessageAsync($"Ошибка при воспроизведении: {ex.Message}");
                    }
                }
            }
            private async Task PlayAudioAsync(IAudioClient client, string path)
            {
                var ffmpeg = CreateStream(path);
                var output = client.CreatePCMStream(AudioApplication.Mixed);

                // Асинхронно копируем данные в поток
                await ffmpeg.StandardOutput.BaseStream.CopyToAsync(output);
                await output.FlushAsync();
            }


            private async Task SendAudioAsync(IAudioClient client, string path)
            {
                var ffmpeg = CreateStream(path);
                var output = client.CreatePCMStream(AudioApplication.Mixed);
                await ffmpeg.StandardOutput.BaseStream.CopyToAsync(output);
                await output.FlushAsync();
            }

            private Process CreateStream(string path)
            {
                return new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg",
                        Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    },
                    EnableRaisingEvents = true
                };
            }
           

            private async Task HandleInteractionAsync(SocketInteraction interaction)
            {
                if (interaction is SocketMessageComponent component)
                {
                    if (component.Data.CustomId == "role_menu")
                    {
                        var selectedOption = component.Data.Values.First();
                        var roles = _config.EmojiRole;
                        if (roles.TryGetValue(selectedOption, out ulong roleId))
                        {
                            var user = interaction.User as SocketGuildUser;
                            if (user != null)
                            {
                                var guild = user.Guild;
                                var role = guild.Roles.FirstOrDefault(x => x.Id == roleId);
                                if (role != null)
                                {
                                    if (user.Roles.FirstOrDefault(x => x.Id == role.Id) != null)
                                    {
                                        await user.RemoveRoleAsync(role);
                                        await component.DeferAsync();
                                        return;
                                    }
                                    await user.AddRoleAsync(role);
                                    await component.DeferAsync();
                                }
                            }
                        }
                    }
                }
            }
        private async Task UserLeftAsync(SocketGuild guild, SocketUser user)
        {

            Console.WriteLine($"{user.Username} покинул сервер {guild.Name}.");

            var webhook = new Webhooks();
            var webhookUrl = "";
            string imgURL = "https://cdn.discordapp.com/emojis/598839725280722978.webp";

            await webhook.SendEmbedWebhookMessageLeft(webhookUrl, user as SocketGuildUser, imgURL);

        }
        private async Task UserJoinedAsync(SocketGuildUser user)
        {
            Console.WriteLine($"{user.Username} зашел на сервер {user.Guild.Name}");

            var webhook = new Webhooks();
            var channelID = new ulong[] { 1321970090933096500, 1314993312834392235, 1321969984888508509, 1321970872407293962 };

            var channelsID = channelID.Where(id => user.Guild.GetTextChannel(id) != null).Distinct().ToArray();

            var webhookUrl = "";

            string avatarURL = user.GetAvatarUrl(size: 1024);
            await webhook.SendEmbedWebhookMessage(webhookUrl, user, channelsID, avatarURL);
        }
    }
}
}