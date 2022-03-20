namespace Puchalski.Spotify.ExternalApi.Models {

    public partial class RecommendationRequest {
        public int? Limit { get; set; }
        public string? Market { get; set; }
        public List<string>? Artists { get; set; }
        public List<string>? Tracks { get; set; }
        public List<string>? GenresName { get; set; }
    }


}

