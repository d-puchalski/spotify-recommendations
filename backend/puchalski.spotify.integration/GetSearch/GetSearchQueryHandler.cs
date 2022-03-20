using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Puchalski.Spotify.Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Integration.GetSearch {
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, List<SearchItemDto>> {

        private readonly ISearchService _service;
        private readonly IMapper _mapper;

        public GetSearchQueryHandler(ISearchService service, IMapper mapper) {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

       async public Task<List<SearchItemDto>> Handle(GetSearchQuery request, CancellationToken cancellationToken) {
            var result = await _service.SearchAsync(new SearchRequest {
                Text = request.Text,
                Type = Enum.Parse<SearchRequestTypeEnum>(request.TypeSearch.ToString())
            });
            return _mapper.Map<List<SearchItem>, List<SearchItemDto>>(result);
        }
    }
}
