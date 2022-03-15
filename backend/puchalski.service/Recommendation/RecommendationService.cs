using puchalski.api.core;
using puchalski.model;
using puchalski.spotify.api.core.externalApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace puchalski.service {
    public class RecommendationService : IRecommendationService {

        private IConfiguration _configuration;
        private readonly ILogger<IRecommendationService> _logger;

        public RecommendationService(ILogger<IRecommendationService> logger, IConfiguration configuration) {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GetRecommendationResponse> GetRecommendationAsync(GetRecommendationRequest request) {
            IExternalRecommendationApi api = await getApiBaseonConfig();
            return await api.GetRecommendationAsync(request);
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request) {
            IExternalRecommendationApi api = await getApiBaseonConfig();
            return await api.SearchAsync(request);
        }

        async private Task<IExternalRecommendationApi> getApiBaseonConfig() {
            string? recommendationApiName = _configuration.GetRequiredSection("RecommendationApiName")?.Value;
            if (string.IsNullOrEmpty(recommendationApiName)) {
                throw Exception("RecommendationApiName required");
            }

            if (recommendationApiName == "Spotify") {
                var recommendationApiSpotifySection = _configuration.GetRequiredSection("RecommendationApiSpotify");
                if (!recommendationApiSpotifySection.Exists()) {
                    throw Exception("RecommendationApiSpotify required");
                } else {
                    if (string.IsNullOrEmpty(recommendationApiSpotifySection["client_id"])) {
                        throw Exception("RecommendationApiName client_id required");
                    }

                    if (string.IsNullOrEmpty(recommendationApiSpotifySection["client_secret"])) {
                        throw Exception("RecommendationApiName client_secret required");
                    }
                    var r = new SpotifyApi(recommendationApiSpotifySection["client_id"], recommendationApiSpotifySection["client_secret"]);
                    await r.CreateAccessTokenAsync();
                    return r;
                }
            } else {
                throw Exception("RecommendationApi required");
            }
        }

        private Exception Exception(string v) {
            _logger.LogError(v);
            return new Exception(v);
        }
    }
}