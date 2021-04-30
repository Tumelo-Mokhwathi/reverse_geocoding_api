using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Models.ReverseGeocode
{
    public class SrwReverseGeocode
    {
        [JsonProperty("result")]
        public Result[] Result { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("docid")]
        public object Docid { get; set; }

        [JsonProperty("seoid")]
        public string Seoid { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

    }
}
