using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCQuery;

namespace DiscordBot2._0
{
    internal class MinecraftAPI
    {
        private int _port;
        private string _ipAdress;
        private MCServer _server;
        private ServerStatus _status;

        public MinecraftAPI(string ipAdress, int port)
        {
            _port = port;
            _ipAdress = ipAdress;
        }
        public bool Connect()
        {
            try
            {
                _server = new MCServer(_ipAdress, _port);
                _status = _server.Status();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка подключения к серверу " + ex.Message);
                return false;
            }
        }

        public string GetOnline()
        {
            if (_status == null)
            {
                Console.WriteLine("Сначала подключитесь к серверу");
                return string.Empty;
            }

            return $"{_status.Players.Online}/{_status.Players.Max}";
        }
        public string GetVersion()
        {
            if (_status == null)
            {
                Console.WriteLine("Сначала подключитесь к серверу");
                return string.Empty;
            }
            var version = _status.Version.Name.Select((c, i) => new { Char = c, Index = i }).FirstOrDefault(x => char.IsDigit(x.Char)).Index;

            return $"Forge {_status.Version.Name.Substring(version)}";
        }
        public string GetIpPort()
        {
            if (_server == null)
            {
                Console.WriteLine("Сначала подключитесь к серверу");
                return string.Empty;
            }
            return $"{_server.Address}:{_server.Port}";
        }
    }
}
