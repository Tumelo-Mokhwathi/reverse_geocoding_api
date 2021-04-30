using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using reverse_geocoding_api.Configurations;
using reverse_geocoding_api.Models.ReverseGeocode;
using reverse_geocoding_api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Services
{
    public class ReverseGeocodeService : IReverseGeocodeService
    {
        private readonly IAwsAuthenticationService _awsAuthenticationService;
        private readonly AwsAuthenticationOptions _awsOptions;
        private readonly General _generalOptions;

        public ReverseGeocodeService(
            IAwsAuthenticationService awsAuthenticationService, 
            IOptions<AwsAuthenticationOptions> awsOptions,
            IOptions<General> generalOptions)
        {
            _awsAuthenticationService = awsAuthenticationService;
            _awsOptions = awsOptions.Value;
            _generalOptions = generalOptions.Value;
        }

        public async Task<ReverseGeocodeResult> GetAsync(double latitude, double longitude)
        {
            var token = _awsAuthenticationService.GetBearerToken(_awsOptions.AwsId, _awsOptions.AwsSecret);

            ReverseGeocodeResult result = await ReverseGeocodeToSeoId(token, latitude, longitude).ConfigureAwait(false);

            return result ?? throw new KeyNotFoundException("Could not reverse geocode");
        }

        private async Task<SrwReverseGeocode> GetReversegeocodeResult(
            string token,
            string safeLat,
            string safeLon,
            int radius,
            string schema)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Add("x-api-key", _awsOptions.AwsKey);
            httpClient.DefaultRequestHeaders.Add("Host", "YOUR HOST HERE");

            var result = await httpClient
                .GetStringAsync($"{_generalOptions.ReverseGeocodeBaseUrl}?latitude={safeLat}&longitude={safeLon}&radius={radius}&limit=1&schema={schema}")
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<SrwReverseGeocode>(result);
        }

        private async Task<ReverseGeocodeResult> ReverseGeocodeToSeoId(
            string token,
            double latitude,
            double longitude)
        {
            var schemas = new[] {
                "ADD YOUR SCHEMA HERE",
            };

            var safeLat = latitude.ToString(CultureInfo.InvariantCulture);
            var safeLon = longitude.ToString(CultureInfo.InvariantCulture);

            foreach (var schema in schemas)
            {
                SrwReverseGeocode srwReverseGeocodeResult = await GetReversegeocodeResult(
                token,
                safeLat,
                safeLon,
                0,
                schema).ConfigureAwait(false);

                if (srwReverseGeocodeResult.Result.Length == 0)
                {
                    continue;
                }
                return srwReverseGeocodeResult
                    .Result
                    .Select(r => new ReverseGeocodeResult
                    {
                        SeoId = r.Seoid,
                        Description = r.FormattedAddress,
                        Latitude = latitude,
                        Longitude = longitude,
                        Distance = r.Distance
                    })
                    .First();

            }

            return null;
        }
    }
}
