namespace Api.Providers.IdentityProvider
{
    public interface IJwtTokenProvider
    {
        Task<string> GenerateTokenAsync();
    }
}
