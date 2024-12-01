using baleares.challenge.API.Infrastructure.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace baleares.challenge.API.Infrastructure.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TokenValidationService _tokenValidationService;

    public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration, TokenValidationService tokenValidationService)
    {
        _next = next;
        _secretKey = configuration["Jwt:Key"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
        _tokenValidationService = tokenValidationService;
    }

    public async Task Invoke(HttpContext context)
    {
            // Verificar si auth es requerido.
            var authorize = context.GetEndpoint()?.Metadata?.GetMetadata<AuthorizeAttribute>();

            if (authorize != null) 
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Verificar si token es enviado.
            if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token faltante.");
                    return;
                }

            // Verificar si token es válido. 
            if (!_tokenValidationService.IsTokenAllowed(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token revocado.");
                return;
            }

            try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_secretKey);

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _issuer,
                        ValidateAudience = true,
                        ValidAudience = _audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out _);

                    await _next(context);
                }
                catch (SecurityTokenExpiredException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token ya expiró.");
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token inválido.");
                }
            }
            else
            {
                await _next(context);
            }
        }
    }

