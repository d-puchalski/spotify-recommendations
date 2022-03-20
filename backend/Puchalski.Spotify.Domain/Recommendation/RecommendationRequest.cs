using System.ComponentModel.DataAnnotations;

namespace Puchalski.Spotify.Domain.Search {
    public partial class RecommendationRequest {

        [Required]
        public int? Limit { get; set; }

        [Required]
        public string? Market { get; set; }

        [Required]
        public List<string>? Artists { get; set; }

        [Required]
        public List<string>? Tracks { get; set; }

        [Required]
        public List<string>? GenresName { get; set; }
    }
}