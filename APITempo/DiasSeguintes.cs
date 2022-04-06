using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITempo
{
    internal class DiasSeguintes
    {
        [JsonProperty("date")]
        public string data { get; set; }
        [JsonProperty("weekday")]
        public string diaDaSemana { get; set; }

        [JsonProperty("max")]
        public int max { get; set; }

        [JsonProperty("min")]
        public int min { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("condition")]
        public string condition { get; set; }
    }
}
