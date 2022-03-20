using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Puchalski.Spotify.Domain.Configuration;
using System.Net;
using Newtonsoft.Json;
using Puchalski.Spotify.ExternalApi.Abstraction;
using AutoMapper;
using ext = Puchalski.Spotify.ExternalApi.Models;

namespace Puchalski.Spotify.Domain.Search {
    public class SearchService : SpotifyDomainServiceBase, ISearchService {

        private readonly IConfiguration _configuration;
        private readonly IExternalCommonApi _externalApi;
        private readonly IMapper _mapper;

        public SearchService(IConfiguration configuration, IExternalCommonApi externalApi, IMapper mapper) : base(configuration) {
            _configuration = configuration;
            _externalApi = externalApi;
            _mapper = mapper;
        }

        public async Task<List<SearchItem>> SearchAsync(SearchRequest request) {
            return _mapper.Map<List<SearchItem>>(await _externalApi.SearchAsync(_mapper.Map<ext.SearchRequest>(request)));
        }
    }
}
