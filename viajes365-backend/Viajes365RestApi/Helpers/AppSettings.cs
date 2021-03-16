using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viajes365RestApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        // Not used in REST wich is supposed stateless
        public int SessionTime { get; set; }
        public string TuTiempoApiKey { get; set; }

    }
}
