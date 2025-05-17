using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DiscordBot2._0
{
    internal class Webhooks
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task SendWebhookMessage(string webhookUrl, string content)
        {
            var payload = new
            {
                content = content,
                username = "NUnix"
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var contentString = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(webhookUrl, contentString);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Сообщение успешно отправлено!");
            }
            else
            {
                Console.WriteLine("Ошибка отправки сообщения");
            }
        }
        public async Task<string> GetChannels(SocketGuildUser x, ulong[] channelsId)
        {
            string result = "";

            foreach (var res in channelsId)
            {
                var targetChannel = x.Guild.GetTextChannel(res);
                result += $"**• <#{targetChannel.Id}> **\n";
            }

            return result;
        }
        public async Task SendEmbedWebhookMessageMCServerStatus(string webhookUrl, MinecraftAPI server, string serverName)
        {
            var embed = new
            {
                username = "NUnix",
                embeds = new[]
                {
                new
                {
                    title = $"Status {serverName}",
                    description = $"\n· **Server IP**: {server.GetIpPort()}\n\n · **Server version**: {server.GetVersion()}\n\n · **Players online**: {server.GetOnline()}\n\n\n **Сборка модов для сервера -** https://drive.google.com/file/d/1DFwzPRvqCxfBQ1jyyiMhdpUlATmFoDXZ/view?usp=sharing",
                    color = 600214,
                }
            }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(embed);
            var contentString = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(webhookUrl, contentString);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Эмбед успешно отправлен через вебхук!");
            }
            else
            {
                Console.WriteLine($"Ошибка отправки эмбеда: {response.StatusCode}");
            }
        }
        public async Task SendEmbedWebhookMessage(string webhookUrl, SocketGuildUser user, ulong[] channelsId, string avatarURL)
        {
            var embed = new
            {
                username = "NUnix",
                embeds = new[]
                {
                new
                {
                    title = "Вітаю",
                    description = $"Вітаємо {user.Mention} на **HyperGame** \n рекомендуємо ознайомитися з наступними каналами:\n{await GetChannels(user, channelsId)}",
                    color = 600214, 
                    image = new
                    {
                        url = avatarURL
                    }
                }
            }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(embed);
            var contentString = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(webhookUrl, contentString);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Эмбед успешно отправлен через вебхук!");
            }
            else
            {
                Console.WriteLine($"Ошибка отправки эмбеда: {response.StatusCode}");
            }
        }
        public async Task SendEmbedWebhookMessageLeft(string webhookUrl, SocketGuildUser user, string imgURL)
        {
            var embed = new
            {
                username = "NUnix",
                embeds = new[]
                {
                new
                {
                    title = "Вийшов",
                    description = $"Підарас ебаний {user.Mention} плакі плакі нахуй",
                    color = 600214, 
                    image = new
                    {
                        url = imgURL 
                    }
                }
            }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(embed);
            var contentString = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(webhookUrl, contentString);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Эмбед успешно отправлен через вебхук!");
            }
            else
            {
                Console.WriteLine($"Ошибка отправки эмбеда: {response.StatusCode}");
            }
        }
    }
}
