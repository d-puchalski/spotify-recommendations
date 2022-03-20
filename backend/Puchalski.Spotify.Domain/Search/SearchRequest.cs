
namespace Puchalski.Spotify.Domain.Search {
    public partial class SearchRequest {

        public string? Text { get; set; }

        public SearchRequestTypeEnum Type { get; set; }
    }

    public enum SearchRequestTypeEnum {
        Track = 1,
        Artist = 2
    }
}