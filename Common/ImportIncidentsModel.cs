using System;
using Newtonsoft.Json;

namespace Mobiroller.Common
{
    public class ImportIncidentsModel
    {
        [JsonProperty(PropertyName = "ID")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "dc_Zaman")]
        public string Time { get; set; }

        [JsonProperty(PropertyName = "dc_Kategori")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "dc_Olay")]
        public string Event { get; set; }

    }
}