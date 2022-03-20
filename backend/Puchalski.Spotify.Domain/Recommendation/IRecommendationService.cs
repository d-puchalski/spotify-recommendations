namespace Puchalski.Spotify.Domain.Recommendation {
    public interface IRecommendationService {
        Task<List<RecommendationItem>> GetRecommendationAsync(RecommendationRequest request);
    }
}
