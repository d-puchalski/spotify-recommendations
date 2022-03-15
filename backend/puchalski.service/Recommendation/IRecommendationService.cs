using puchalski.model;

namespace puchalski.service {
    public interface IRecommendationService {
        Task<GetRecommendationResponse> GetRecommendationAsync(GetRecommendationRequest request);
        Task<SearchResponse> SearchAsync(SearchRequest request);
    }
}
