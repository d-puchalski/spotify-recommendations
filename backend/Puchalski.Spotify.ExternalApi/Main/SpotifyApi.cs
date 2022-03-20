using Newtonsoft.Json;
using Puchalski.Spotify.ExternalApi.Abstraction;
using Puchalski.Spotify.ExternalApi.Models;
using System.Net;
using System.Text;

namespace Puchalski.Spotify.ExternalApi.Main {
    public class SpotifyApi : IExternalCommonApi {

        private string? token = null;

        public SpotifyApi(string client_id, string client_secret) {
            Task.WaitAll(createAccessTokenAsync(client_id, client_secret));
        }

        public async Task<List<RecommendationResponse>> GetRecommendationAsync(RecommendationRequest request) {
            List<RecommendationResponse> result = new List<RecommendationResponse>();

            if (string.IsNullOrEmpty(request.Market))
                throw new ArgumentNullException("market is null");

            if (request.Limit < 1 || request.Limit > 100)
                throw new ArgumentOutOfRangeException("limit not in range 1-100");

            if (request.GenresName == null && request.Artists == null && request.Tracks == null)
                throw new ArgumentNullException("artists or tracks null");

            if (request.GenresName?.Count == 0 && request.Artists?.Count == 0 && request.Tracks?.Count == 0)
                throw new ArgumentException("artists or tracks empty");

            if (request.Artists?.Count > 2 || request.Tracks?.Count > 2)
                throw new ArgumentOutOfRangeException("artists or tracks above limits");

            string genresString = string.Empty;
            if (request.GenresName != null && request.GenresName.Count > 0)
                genresString = string.Join("%2", request.GenresName);

            string artistsString = string.Empty;
            if (request.Artists != null && request.Artists?.Count > 0)
                artistsString = string.Join(",", request.Artists);

            string tracksString = string.Empty;
            if (request.Tracks != null && request.Tracks?.Count > 0)
                tracksString = string.Join(",", request.Tracks);
            using (WebClient wc = new WebClient()) {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers[HttpRequestHeader.Accept] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                var body = await wc.DownloadStringTaskAsync($"https://api.spotify.com/v1/recommendations?limit={request.Limit}&market={request.Market}&seed_artists={artistsString}&seed_genres={genresString}&seed_tracks={tracksString}");
                if (body != null) {
                    dynamic returnBody = JsonConvert.DeserializeObject(body);
                    IEnumerable<dynamic> items = returnBody?.tracks;
                    if (items == null) {
                        throw new ArgumentNullException("returnBody is null");
                    } else {
                        result = items?.Select(q => new RecommendationResponse { Id = q.id, Name = q.name, UrlImage = q.album?.images?[0]?.url }).ToList();
                    }
                }
            }
            return result;
        }

        public async Task<List<SearchResponse>> SearchAsync(SearchRequest request) {
            List<SearchResponse> result = new List<SearchResponse>();

            if (string.IsNullOrEmpty(request.Text))
                throw new ArgumentNullException("Text is null");

            if (request.Text.Length < 3)
                throw new ArgumentOutOfRangeException("Text is too short");

            using (WebClient wc = new WebClient()) {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers[HttpRequestHeader.Accept] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                string typeAsString = request.Type == 1 ? "track" : "artist";
                var body = await wc.DownloadStringTaskAsync($"https://api.spotify.com/v1/search?q={request.Text}&type={typeAsString}&limit=5");
                if (body != null) {
                    dynamic returnBody = JsonConvert.DeserializeObject(body);
                    IEnumerable<dynamic> items = request.Type == 1 ? (IEnumerable<dynamic>?)(returnBody?.tracks?.items) : (IEnumerable<dynamic>?)(returnBody?.artists?.items);
                    if (items == null) {
                        throw new ArgumentNullException("returnBody is null");
                    } else {
                        result = items?.Select(q => new SearchResponse { Id = q.id, Name = q.name }).ToList();
                    }
                }
                return result;
            }
        }

        private async Task createAccessTokenAsync(string client_id, string client_secret) {
            var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", client_id, client_secret)));
            using (WebClient wc = new WebClient()) {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Headers[HttpRequestHeader.Accept] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Basic " + encode_clientid_clientsecret;
                var body = await wc.UploadStringTaskAsync("https://accounts.spotify.com/api/token", "POST", "grant_type=client_credentials");
                if (body != null) {
                    dynamic returnBody = JsonConvert.DeserializeObject(body);
                    token = returnBody?.access_token;
                }
            }
        }
    }
}