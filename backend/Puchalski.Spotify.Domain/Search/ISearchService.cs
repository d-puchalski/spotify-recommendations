namespace Puchalski.Spotify.Domain.Search {
    public interface ISearchService {
        Task<List<SearchItem>> SearchAsync(SearchRequest request);
    }
}
