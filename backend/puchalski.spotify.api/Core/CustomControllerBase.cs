using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace puchalski.spotify.api.Core {
    public class CustomControllerBase<T> : ControllerBase {
        public readonly ILogger<T> _logger;

        public CustomControllerBase(ILogger<T> logger) {
            _logger = logger;
        }

        /// <summary>
        /// Swagger requires implicit type, so I added different types
        /// </summary>
        public ActionResult error500<TX>(Exception e) {
            _logger.LogError(Environment.CurrentManagedThreadId, e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, nameof(StatusCodes.Status500InternalServerError));
        }
    }
}
