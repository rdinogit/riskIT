namespace Api.Contract
{
    public record CalculateForWordRequest
    {
        /// <summary>
        /// Text subject to analysis.
        /// </summary>
        /// <example>The sun shines over the lake.</example>
        public string Text { get; init; } = string.Empty;

        /// <summary>
        /// Word to be searched.
        /// </summary>
        /// <example>sun</example>
        public string Word { get; init; } = string.Empty;
    }
}
