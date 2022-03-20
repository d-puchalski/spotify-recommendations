using AutoMapper;
using Microsoft.Extensions.Configuration;
using Puchalski.Spotify.Domain.Configuration;
using Puchalski.Spotify.ExternalApi.Abstraction;
using ext = Puchalski.Spotify.ExternalApi.Models;

namespace Puchalski.Spotify.Domain.Recommendation {
    public class RecommendationService : SpotifyDomainServiceBase, IRecommendationService {

        private readonly IConfiguration _configuration;
        private readonly IExternalCommonApi _externalApi;
        private readonly IMapper _mapper;

        public RecommendationService(IConfiguration configuration, IExternalCommonApi externalApi, IMapper mapper) : base(configuration) {
            _configuration = configuration;
            _externalApi = externalApi;
            _mapper = mapper;
        }

        public async Task<List<RecommendationItem>> GetRecommendationAsync(RecommendationRequest request) {
            return _mapper.Map<List<RecommendationItem>>(await _externalApi.GetRecommendationAsync(_mapper.Map<ext.RecommendationRequest>(request)));
        }
    }
}