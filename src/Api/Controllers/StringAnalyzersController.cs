using Api.Contract;
using Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateForWord;
using Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateHighest;
using Api.CQRS.WordFrequency.StringAnalyzer.Queries.CalculateMostFrequentWords;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/cqrs/wordFrequencies")]
    [Authorize]
    public class StringAnalyzersController : Controller
    {
        private readonly ILogger<StringAnalyzersController> _logger;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public StringAnalyzersController(
            ILogger<StringAnalyzersController> logger,
            IMapper mapper,
            ISender mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Computes the highest word frequency value among all the computed frequencies for words found in the provided Text.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The highest word frequency value found.</returns>
        [HttpPost("highest")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CalculateHighestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCalculateHighest(CalculateHighestRequest request)
        {
            var query = _mapper.Map<CalculateHighestQuery>(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Computes the word frequency value in the provided Text for the specified word.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The word frequency value for the specified word.</returns>
        [HttpPost("word")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CalculateForWordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCalculateForWord(CalculateForWordRequest request)
        {
            var query = _mapper.Map<CalculateForWordQuery>(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Computes the specified number of most frequent words given the provided Text. In case of equal frequencies, alphabetical order is considered.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A specified number of most frequent words.</returns>
        [HttpPost("words")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CalculateMostFrequentWordsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCalculateForWords(CalculateForWordsRequest request)
        {
            var query = _mapper.Map<CalculateMostFrequentWordsQuery>(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
