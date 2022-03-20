using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;
using Newtonsoft.Json;

namespace Puchalski.Spotify.Domain.Configuration {
    public class SpotifyDomainServiceBase {
        private IConfiguration _configuration;

        public SpotifyDomainServiceBase(IConfiguration configuration) {
            this._configuration = configuration;
        }
    }
}
