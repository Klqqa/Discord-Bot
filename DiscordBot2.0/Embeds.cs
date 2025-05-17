using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot2._0
{
    public class Embeds
    {
        
        public async Task RolesEmbedAsync(DiscordSocketClient _client, ulong channelId)
        {
             var embed = new EmbedBuilder()
            .WithTitle("**Під цим постом ви можете вибрати собі роль, натиснувши на відповідну кнопку ролі в меню вибору**")
            .WithDescription("ㅤCall Of Duty Warzone — <:9GamesWarzone:1328056059620753501> \r\nㅤCounter-Strike 2 — <:Games_CSGO:1328055216267857960> \r\nㅤGTA 5 — <:gtarole:1328055039482400890> \r\nㅤValorant — <:valorantrole:1328055186459070495> \r\nㅤApex Legends — <:Apex_Legends:1328055023183331381> \r\nㅤDota 2 —  <:dota2:1330145679770652793>\r\nㅤTom Clancy's Rainbow Six — <:GAMERainbow6:1328057588213874739> \r\nㅤWar Thunder — <:war_thunder51:1330145726247862302>\r\nㅤMinecraft — <:Minecraft_Launcher:1328057447989903392> \r\nㅤSCP Secret Laboratory —  <:scp:1330145541962731581>\n\n**Повторне натискання кнопки видаляє роль**")
            .WithImageUrl("https://cdn.mos.cms.futurecdn.net/nQ67v7m8tjkJA8gAYwDiJE.jpg")
            .WithColor(new Discord.Color(60, 02, 14))
            .Build();

            var builder = new SelectMenuBuilder()
                .WithCustomId("role_menu")
                .WithPlaceholder("Вибір ролі")
                .WithMinValues(1)
                .WithMaxValues(1)
                .AddOption("Call Of Duty Warzone", "Warzone_role", emote: Emote.Parse("<:9GamesWarzone:1328056059620753501>"))
                .AddOption("Counter-Strike 2", "CS2_role", emote: Emote.Parse("<:Games_CSGO:1328055216267857960>"))
                .AddOption("GTA V", "GTAV_role", emote: Emote.Parse("<:gtarole:1328055039482400890>"))
                .AddOption("Apex Legends", "ApexLegends_role", emote: Emote.Parse("<:Apex_Legends:1328055023183331381>"))
                .AddOption("Valorant", "VALORANT_role", emote: Emote.Parse("<:valorantrole:1328055186459070495>"))
                .AddOption("Dota 2", "Dota2_role", emote: Emote.Parse("<:dota2:1330145679770652793>"))
                .AddOption("Tom Clancy's Rainbow Six", "R6_role", emote: Emote.Parse("<:GAMERainbow6:1328057588213874739>"))
                .AddOption("War Thunder", "WarThunder_role", emote: Emote.Parse("<:war_thunder51:1330145726247862302>"))
                .AddOption("Minecraft", "Minecraft_role", emote: Emote.Parse("<:Minecraft_Launcher:1328057447989903392>"))
                .AddOption("SCP Secret Laboratory", "SCP_role", emote: Emote.Parse("<:scp:1330145541962731581>"));


            var comp = new ComponentBuilder()
               .WithSelectMenu(builder);

            var channel = _client.GetChannel(channelId) as IMessageChannel;

            

            await channel.SendMessageAsync(embed: embed, components: comp.Build());
        }
        public async Task AnnouncementEmbedAsync(DiscordSocketClient _client)
        {
            var channel = _client.GetChannel(1328042305210417173) as IMessageChannel;

            var embed = new EmbedBuilder()
           .WithTitle("**Важливо**")
           .WithDescription(":middle_finger:")
           .WithColor(new Discord.Color(60, 02, 14))
           .WithImageUrl("https://cdn.discordapp.com/emojis/1329430467589439529.webp")
           .Build();


            await channel.SendMessageAsync(embed: embed);
        }
    }
}