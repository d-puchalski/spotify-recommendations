namespace Puchalski.Spotify.Domain.Search {
    public interface IRecommendationService {
        Task<List<RecommentationItem>> GetRecommendationAsync(RecommendationRequest request);
    }
}
