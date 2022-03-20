using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Net;
using Puchalski.Spotify.Integration.PostRecommendation;

namespace Puchalski.Spotify.Api.Controllers.Recommendation {

    [ApiController]
    [Route("api/recommendation")]
    public class RecommendationController : Controller {

        private readonly IMediator _mediator;

        public RecommendationController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<RecommendationItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRecommendation([FromBody] Request request) {
            var query = new PostRecommendationQuery {
                Artists = request.Artists,
                GenresName = request.GenresName,
                Limit = request.Limit,
                Market = request.Market,
                Tracks = request.Tracks,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}