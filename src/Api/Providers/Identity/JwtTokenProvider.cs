using Api.Providers.IdentityProvider;
using Api.Providers.TimeDate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Providers.Identity
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtTokenProviderSettings _settings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtTokenProvider(IOptions<JwtTokenProviderSettings> settings, IDateTimeProvider dateTimeProvider)
        {
            _settings = settings.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<string> GenerateTokenAsync()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, "Example First Name."),
                new Claim(JwtRegisteredClaimNames.FamilyName, "Example Last Name."),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var now = _dateTimeProvider.UtcNow;

            var securityToken = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: now.AddMinutes(_settings.ExpirationTimeInMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Task.FromResult(token);
        }
    }
}
