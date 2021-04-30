using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using reverse_geocoding_api.Configurations;
using reverse_geocoding_api.Models.Token;
using reverse_geocoding_api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Services
{
    public class AwsAuthenticationService : IAwsAuthenticationService
    {
        private readonly AwsAuthenticationOptions _awsAuthenticationOptions;
        public AwsAuthenticationService(IOptions<AwsAuthenticationOptions> awsAuthenticationOptions)
        {
            _awsAuthenticationOptions = awsAuthenticationOptions.Value;
        }
        public string GetBearerToken(string id, string secret)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_awsAuthenticationOptions.Url)
            };

            var content = new FormUrlEncodedContent(new[] {
                            new KeyValuePair<string, string>("grant_type", "client_credentials"),
                            new KeyValuePair<string, string>("client_id", id),
                            new KeyValuePair<string, string>("client_secret", secret),
                        });
            var result = client.PostAsync("/oauth2/token", content).Result;
            JwtToken token = JsonConvert
                .DeserializeObject<JwtToken>(result.Content.ReadAsStringAsync().Result);
            return token.Access_token;
        }
    }
}
