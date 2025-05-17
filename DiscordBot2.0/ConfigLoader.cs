using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot2._0
{
    public class ConfigLoader
    {
        public static BotConfig LoadConfig(string filePath)
        {
            if(!File.Exists(filePath))
                throw new FileNotFoundException("Конфигурационный файл не найден, бот работает в автономном режиме");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<BotConfig>(json);
        }
    }
}
