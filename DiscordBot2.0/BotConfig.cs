using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot2._0
{
    public class BotConfig
    {
        public string Token { get; set; }
        public string IpServer { get; set; }
        public int Port { get; set; }
        public ulong channelRoleId { get; set; }
        public Dictionary<string, ulong> EmojiRole { get; set; }
    }
}
