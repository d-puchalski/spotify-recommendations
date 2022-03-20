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
        protected AccessTokenDto? _apiKey = null;

        public SpotifyDomainServiceBase(IConfiguration configuration) {
            this._configuration = configuration;
            createAccessToken();
        }

        private void createAccessToken() {
            var (client_id, client_secret) = getConfiguration();
            var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", client_id, client_secret)));
            using (WebClient wc = new WebClient()) {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Headers[HttpRequestHeader.Accept] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Basic " + encode_clientid_clientsecret;
                string result = wc.UploadString("https://accounts.spotify.com/api/token", "POST", "grant_type=client_credentials");
                if (!string.IsNullOrEmpty(result)) {
                    _apiKey = JsonConvert.DeserializeObject<AccessTokenDto>(result);
                }
            }
        }

        private (string client_id, string client_secret) getConfiguration() {
            string? recommendationApiName = _configuration.GetRequiredSection("RecommendationApiName")?.Value;
            if (string.IsNullOrEmpty(recommendationApiName)) {
                throw new Exception("RecommendationApiName required");
            }
            var recommendationApiSpotifySection = _configuration.GetRequiredSection("RecommendationApiSpotify");
            if (!recommendationApiSpotifySection.Exists()) {
                throw new Exception("RecommendationApiSpotify required");
            } else {
                if (string.IsNullOrEmpty(recommendationApiSpotifySection["client_id"])) {
                    throw new Exception("RecommendationApiName client_id required");
                }

                if (string.IsNullOrEmpty(recommendationApiSpotifySection["client_secret"])) {
                    throw new Exception("RecommendationApiName client_secret required");
                }

                return (recommendationApiSpotifySection["client_id"], recommendationApiSpotifySection["client_secret"]);
            }
        }
    }
}
