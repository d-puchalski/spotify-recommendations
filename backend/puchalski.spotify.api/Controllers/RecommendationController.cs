using puchalski.model;
using puchalski.service;
using puchalski.spotify.api.Core;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace puchalski.spotify.api.Controllers {

    [ApiController]
    [Route("/")]
    public class RecommendationController : CustomControllerBase<IRecommendationController>, IRecommendationController {

        private readonly ILogger<IRecommendationController> _logger;
        private readonly IRecommendationService _service;
        public RecommendationController(ILogger<IRecommendationController> logger, IRecommendationService service) : base(logger) {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("Search")]
        [SwaggerOperation(OperationId = "post")]
        public async Task<ActionResult<SearchResponse>> Search([FromBody] SearchRequest request) {
            try {
                return CreatedAtAction(nameof(GetRecommendation), await _service.SearchAsync(request));
            } catch (Exception e) {
                _logger.LogError(e.Message);
                return base.error500<bool>(e);
            }
        }

        [HttpPost]
        [Route("GetRecommendation")]
        [SwaggerOperation(OperationId = "post")]
        public async Task<ActionResult<GetRecommendationResponse>> GetRecommendation([FromBody] GetRecommendationRequest request) {
            try {
                return CreatedAtAction(nameof(GetRecommendation), await _service.GetRecommendationAsync(request));
            } catch (Exception e) {
                _logger.LogError(e.Message);
                return base.error500<bool>(e);
            }
        }


    }
}