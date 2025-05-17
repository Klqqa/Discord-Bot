using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot2._0
{
    public class DiscordApi
    {
        private readonly RestClient _client;
        private readonly string _botToken;

        public DiscordApi(string botToken)
        {
            _client = new RestClient("https://discord.com/api/v10/");
            _botToken = botToken;
        }
    }
}
