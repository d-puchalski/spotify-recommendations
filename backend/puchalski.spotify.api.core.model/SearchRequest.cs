namespace puchalski.model {
    public partial class SearchRequest {

        public string? Text { get; set; }

        public SearchRequestTypeEnum Type { get; set; }
    }

    public enum SearchRequestTypeEnum {
        track = 1,
        artist = 2
    }
}