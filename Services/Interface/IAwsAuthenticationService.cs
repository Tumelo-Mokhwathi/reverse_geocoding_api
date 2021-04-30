using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Services.Interface
{
    public interface IAwsAuthenticationService
    {
        string GetBearerToken(string id, string secret);
    }
}
