namespace puchalski.model {
    public partial class GetRecommendationRequest {
        public int? Limit { get; set; }

        public string? Market { get; set; }

        public List<string>? Artists { get; set; }

        public List<string>? Tracks { get; set; }

        public List<string>? GenresName { get; set; }
    }
}