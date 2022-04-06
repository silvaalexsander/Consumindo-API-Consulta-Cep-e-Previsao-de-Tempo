using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITempo
{
    internal class Previsao
    {
        public Previsao()
        {
            this.tempo = new Tempo();
        }

        [JsonProperty("results")]
        public Tempo tempo { get; set; }
    }
}
