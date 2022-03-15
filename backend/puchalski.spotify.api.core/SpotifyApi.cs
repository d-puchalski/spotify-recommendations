using puchalski.api.core;
using puchalski.model;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace puchalski.spotify.api.core.externalApi {
    public class SpotifyApi : IExternalCommonApi, IExternalRecommendationApi {

        private string _client_id;
        private string _client_secret;
        private GetAccessTokenResponse? _apiKey = null;

        public SpotifyApi(string client_id, string client_secret) {
            _client_id = client_id;
            _client_secret = client_secret;
        }

        /// <summary>
        /// curl -X "GET" "https://api.spotify.com/v1/recommendations" -H "Accept: application/json" -H "Content-Type: application/json" -H "Authorization: Bearer 
        /// </summary>
        /// <returns></returns>
        async public Task<GetRecommendationResponse> GetRecommendationAsync(GetRecommendationRequest request) {
            try {
                GetRecommendationResponse result = new GetRecommendationResponse();

                if (string.IsNullOrEmpty(request.Market))
                    throw ApiException.GetRecommendationRequestException;

                if (request.Limit < 1 || request.Limit > 100)
                    throw ApiException.GetRecommendationRequestException;

                if (request.GenresName == null && request.Artists == null && request.Tracks == null)
                    throw ApiException.GetRecommendationRequestException;

                if (request.GenresName?.Count == 0 && request.Artists?.Count == 0 && request.Tracks?.Count == 0)
                    throw ApiException.GetRecommendationRequestException;

                if (request.Artists?.Count > 2 || request.Tracks?.Count > 2)
                    throw ApiException.GetRecommendationRequestException;

                string genresString = string.Empty;
                if (request.GenresName != null && request.GenresName.Count > 0)
                    genresString = string.Join("%2", request.GenresName);

                string artistsString = string.Empty;
                if (request.Artists != null && request.Artists?.Count > 0)
                    artistsString = string.Join(",", request.Artists);

                string tracksString = string.Empty;
                if (request.Tracks != null && request.Tracks?.Count > 0)
                    tracksString = string.Join(",", request.Tracks);
#pragma warning disable CS8603 // Possible null reference return.
                return await Task.Run(() => {
                    using (WebClient wc = new WebClient()) {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                        wc.Headers[HttpRequestHeader.Accept] = "application/json";
                        wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + _apiKey?.access_token;
                        string body = wc.DownloadString($"https://api.spotify.com/v1/recommendations?limit={request.Limit}&market={request.Market}&seed_artists={artistsString}&seed_genres={genresString}&seed_tracks={tracksString}");
                        if (body != null) {
                            result = JsonConvert.DeserializeObject<GetRecommendationResponse>(body);
                        }
                    }
                    return result;
                });
#pragma warning restore CS8603 // Possible null reference return.
            } catch {
                throw ApiException.GetRecommendationException;
            }
        }

        async public Task<SearchResponse> SearchAsync(SearchRequest request) {
            try {
#pragma warning disable CS8603 // Possible null reference return.
                return await Task.Run(() => {
                    SearchResponse result = new SearchResponse();
                    using (WebClient wc = new WebClient()) {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                        wc.Headers[HttpRequestHeader.Accept] = "application/json";
                        wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + _apiKey?.access_token;
                        string body = wc.DownloadString($"https://api.spotify.com/v1/search?q={request.Text}&type={request.Type}&limit=5");
                        if (body != null) {
                            result = JsonConvert.DeserializeObject<SearchResponse>(body);
                        }
                        return result;
                    }
                });
#pragma warning restore CS8603 // Possible null reference return.
            } catch {
                throw ApiException.GetSearchException;
            }
        }

        /// <summary>
        /// token should be recreated evry 3600 seconds (this funcionality is not included here)
        /// </summary>
        async public Task CreateAccessTokenAsync() {
            try {
                await Task.Run(() => {
                    var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _client_id, _client_secret)));
                    using (WebClient wc = new WebClient()) {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        wc.Headers[HttpRequestHeader.Accept] = "application/json";
                        wc.Headers[HttpRequestHeader.Authorization] = "Basic " + encode_clientid_clientsecret;
                        string result = wc.UploadString("https://accounts.spotify.com/api/token", "POST", "grant_type=client_credentials");
                        if (!string.IsNullOrEmpty(result)) {
                            _apiKey = JsonConvert.DeserializeObject<GetAccessTokenResponse>(result);
                        }
                    }
                });
            } catch {
                throw ApiException.CreateAccessTokenException;
            }
        }


    }
}
