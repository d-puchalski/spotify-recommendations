using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Puchalski.Spotify.Domain.Configuration;
using System.Net;

namespace Puchalski.Spotify.Domain.Search {
    public class RecommendationService : SpotifyDomainServiceBase, IRecommendationService {

        private readonly IConfiguration _configuration;

        public RecommendationService(IConfiguration configuration) : base(configuration) {
            _configuration = configuration;
        }

        public async Task<List<RecommentationItem>> GetRecommendationAsync(RecommendationRequest request) {
            List<RecommentationItem> result = new List<RecommentationItem>();

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
                wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + _apiKey?.access_token;
                var body = await wc.DownloadStringTaskAsync($"https://api.spotify.com/v1/recommendations?limit={request.Limit}&market={request.Market}&seed_artists={artistsString}&seed_genres={genresString}&seed_tracks={tracksString}");
                if (body != null) {
                    dynamic returnBody = JsonConvert.DeserializeObject(body);
                    IEnumerable<dynamic> items = returnBody?.tracks;
                    if (items == null) {
                        throw new ArgumentNullException("returnBody is null");
                    } else {
                        result = items?.Select(q => new RecommentationItem { Id = q.id, Name = q.name, UrlImage = q.album?.images?[0]?.url }).ToList();
                    }
                }
            }
            return result;
        }
    }
}