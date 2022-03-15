using puchalski.model;

namespace puchalski.api.core {
    public interface IExternalRecommendationApi {

        public Task<GetRecommendationResponse> GetRecommendationAsync(GetRecommendationRequest request);
        public Task<SearchResponse> SearchAsync(SearchRequest request);
    }
}