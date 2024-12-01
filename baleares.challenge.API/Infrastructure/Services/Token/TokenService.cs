using baleares.challenge.API.infrastructure.services.interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace baleares.challenge.API.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration cfg)
    {
        _configuration = cfg;
    }
    string ITokenService.GenerateJwtToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        int expires = int.Parse(_configuration["Jwt:Expires"]);
        string issuer = _configuration["Jwt:Issuer"];
        string audience = _configuration["Jwt:Audience"];
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
           new Claim(JwtRegisteredClaimNames.Sub, username),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(expires), signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
