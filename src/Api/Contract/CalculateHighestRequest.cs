namespace Api.Contract
{
    public record CalculateHighestRequest
    {
        /// <summary>
        /// Text subject to analysis.
        /// </summary>
        /// <example>The sun shines over the lake.</example>
        public string Text { get; init; } = string.Empty;
    };
}
