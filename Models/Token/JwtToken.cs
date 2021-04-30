using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Models.Token
{
    public class JwtToken
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public string Token_type { get; set; }
    }
}
