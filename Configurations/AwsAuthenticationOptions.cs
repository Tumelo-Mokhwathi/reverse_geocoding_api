using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api.Configurations
{
    public class AwsAuthenticationOptions
    {
        public string AwsId { get; set; }
        public string AwsKey { get; set; }
        public string AwsSecret { get; set; }
        public string Url { get; set; }
    }
}
