using Api.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Test.Interfaces;
using Test.Providers;

namespace Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/analyzers")]
    [Authorize]
    public class WordFrequenciesController : ControllerBase
    {
        private readonly ILogger<WordFrequenciesController> _logger;
        private readonly IWordFrequencyAnalyzerRegex _regexAnalyzer;
        private readonly IWordFrequencyAnalyzerString _stringAnalyzer;

        public WordFrequenciesController(
            ILogger<WordFrequenciesController> logger,
            IWordFrequencyAnalyzerRegex regexAnalyzer,
            IWordFrequencyAnalyzerString stringAnalyzer)
        {
            _logger = logger;
            _regexAnalyzer = regexAnalyzer;
            _stringAnalyzer = stringAnalyzer;
        }

        /// <summary>
        /// Using the Regex Analyzer, computes the highest word frequency value among all the computed frequencies for words found in the provided Text.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The highest word frequency value found.</returns>
        [HttpGet("regex/highest")]
        [Produces(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRegexCalculateHighest([FromQuery]CalculateHighestRequest request)
        {
            var result = _regexAnalyzer.CalculateHighestFrequency(request.Text);
            return Ok(result.ToString());
        }

        /// <summary>
        /// Using the Regex Analyzer, computes the word frequency value in the provided Text for the specified word.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The word frequency value for the specified word.</returns>
        [HttpGet("regex/word")]
        [Produces(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRegexCalculateForWord([FromQuery]CalculateForWordRequest request)
        {
            var result = _regexAnalyzer.CalculateFrequencyForWord(request.Text, request.Word);
            return Ok(result.ToString());
        }

        /// <summary>
        /// Using the Regex Analyzer, computes the specified number of most frequent words given the provided Text. In case of equal frequencies, alphabetical order is considered.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A specified number of most frequent words.</returns>
        [HttpGet("regex/words")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<IWordFrequency>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRegexCalculateForWords([FromQuery] CalculateForWordsRequest request)
        {
            var result = _regexAnalyzer.CalculateMostFrequentNWords(request.Text, request.NumberOfWords);
            return Ok(result);
        }

        /// <summary>
        /// Using the String Analyzer, computes the highest word frequency value among all the computed frequencies for words found in the provided Text.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The highest word frequency value found.</returns>
        [HttpGet("string/highest")]
        [Produces(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetStringCalculateHighest([FromQuery] CalculateHighestRequest request)
        {
            var result = _stringAnalyzer.CalculateHighestFrequency(request.Text);
            return Ok(result.ToString());
        }

        /// <summary>
        /// Using the String Analyzer, computes the word frequency value in the provided Text for the specified word.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The word frequency value for the specified word.</returns>
        [HttpGet("string/word")]
        [Produces(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetStringCalculateForWord([FromQuery] CalculateForWordRequest request)
        {
            var result = _stringAnalyzer.CalculateFrequencyForWord(request.Text, request.Word);
            return Ok(result.ToString());
        }

        /// <summary>
        /// Using the String Analyzer, computes the specified number of most frequent words given the provided Text. In case of equal frequencies, alphabetical order is considered.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A specified number of most frequent words.</returns>
        [HttpGet("string/words")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<IWordFrequency>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetStringCalculateForWords([FromQuery] CalculateForWordsRequest request)
        {
            var result = _stringAnalyzer.CalculateMostFrequentNWords(request.Text, request.NumberOfWords);
            return Ok(result);
        }

        /// <summary>
        /// An endpoint with the sole purpose of showcasing versioning
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        [HttpGet("example/versioning")]
        public IActionResult GetExampleDeprecated()
        {
            return Ok();
        }

        /// <summary>
        /// An endpoint with the sole purpose of showcasing versioning
        /// </summary>
        /// <returns></returns>
        [ApiVersion("2.0")]
        [HttpGet("example/versioning")]
        public IActionResult GetExampleNew()
        {
            return Ok();
        }
    }
}