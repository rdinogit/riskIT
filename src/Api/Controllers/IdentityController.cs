using Api.Providers.IdentityProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public IdentityController(ILogger<IdentityController> logger, IJwtTokenProvider jwtTokenProvider)
        {
            _logger = logger;
            _jwtTokenProvider = jwtTokenProvider;
        }

        /// <summary>
        /// Returns a dummy token with dummy claims only to be used for testing purposes.
        /// </summary>
        /// <returns>A dummy JWT.</returns>
        [HttpGet("token")]
        [AllowAnonymous]
        [Produces(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetToken()
        {
            var token = await _jwtTokenProvider.GenerateTokenAsync();
            return Ok(token);
        }
    }
}
