using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using System.Net;
using Puchalski.Spotify.Application.GetSearch;

namespace Puchalski.Spotify.Domain.Search {

    [ApiController]
    [Route("api/search")]
    public class SearchController : Controller {

        private readonly IMediator _mediator;

        public SearchController(ILogger<SearchController> logger, IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{Text}/{Type}")]
        [ProducesResponseType(typeof(SearchItemDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromRoute] Request request) {
            var query = new GetSearchQuery(request.Text, Enum.Parse<TypeSearchEnum>(request.Type.ToString()));
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}