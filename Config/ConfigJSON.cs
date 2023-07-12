using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot
{
    internal struct ConfigJSON
    {
        [JsonProperty("DiscordToken")]
        public string DiscordToken { get; private set; }
        [JsonProperty("BotPrefix")]
        public string BotPrefix { get; private set; }
        [JsonProperty("TenorApiKey")]
        public string TenorApiToken { get; private set; }
    }
}