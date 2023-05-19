namespace Api.Contract
{
    public record CalculateForWordsRequest
    {
        /// <summary>
        /// Text subject to analysis.
        /// </summary>
        /// <example>The sun shines over the lake.</example>
        public string Text { get; init; } = string.Empty;

        /// <summary>
        /// Number of identified words to be returned.
        /// </summary>
        /// <example>2</example>
        public int NumberOfWords { get; init; }

    }
}
