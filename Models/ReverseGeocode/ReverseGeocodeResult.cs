using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Models.ReverseGeocode
{
    public class ReverseGeocodeResult
    {
        public string Description { get; set; }
        public string SeoId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }

    }
}
