using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Puchalski.Spotify.Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Integration.PostRecommendation {
    public class PostRecommendationQueryHandler : IRequestHandler<PostRecommendationQuery, List<RecommendationItemDto>> {

        private readonly IRecommendationService _service;
        private readonly IMapper _mapper;

        public PostRecommendationQueryHandler(IRecommendationService service, IMapper mapper) {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<RecommendationItemDto>> Handle(PostRecommendationQuery request, CancellationToken cancellationToken) {
            var result = await _service.GetRecommendationAsync(new RecommendationRequest {
                Artists = request.Artists,
                GenresName = request.GenresName,
                Limit = request.Limit,
                Market = request.Market,
                Tracks = request.Tracks,
            });
            return _mapper.Map<List<RecommentationItem>, List<RecommendationItemDto>>(result);
        }
    }
}
