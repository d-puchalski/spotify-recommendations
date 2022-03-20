using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Integration.PostRecommendation {
    public class PostRecommendationQuery: IRequest<List<RecommendationItemDto>> {
        public int? Limit { get; set; }

        public string? Market { get; set; }

        public List<string>? Artists { get; set; }

        public List<string>? Tracks { get; set; }

        public List<string>? GenresName { get; set; }
    }
}
