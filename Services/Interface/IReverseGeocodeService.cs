using reverse_geocoding_api.Models.ReverseGeocode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Services.Interface
{
    public interface IReverseGeocodeService
    {
        Task<ReverseGeocodeResult> GetAsync(
            double latitude,
            double longitude);
    }
}
