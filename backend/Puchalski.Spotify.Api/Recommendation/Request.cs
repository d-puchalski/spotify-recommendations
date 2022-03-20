using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Api.Controllers.Recommendation {
    public class Request {
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
