using System.ComponentModel.DataAnnotations;

namespace Puchalski.Spotify.Domain.Search {
    public partial class Request {
        [Required]
        public string Text { get; set; }
        [Required]
        public RequestTypeEnum Type { get; set; }
    }

    public enum RequestTypeEnum {
        Track = 1,
        Artist = 2
    }
}