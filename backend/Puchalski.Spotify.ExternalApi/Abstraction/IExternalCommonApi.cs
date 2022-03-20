using Puchalski.Spotify.ExternalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.ExternalApi.Abstraction {
    public interface IExternalCommonApi {
        public Task<List<RecommendationResponse>> GetRecommendationAsync(RecommendationRequest request);
        public Task<List<SearchResponse>> SearchAsync(SearchRequest request);
    }
}
