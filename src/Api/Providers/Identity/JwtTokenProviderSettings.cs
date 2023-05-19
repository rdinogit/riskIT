namespace Api.Providers.Identity
{
    public class JwtTokenProviderSettings
    {
        public const string SectionName = nameof(JwtTokenProviderSettings);
        public string Secret { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public int ExpirationTimeInMinutes { get; init; }
    }
}
