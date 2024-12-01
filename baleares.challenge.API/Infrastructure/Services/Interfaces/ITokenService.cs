namespace baleares.challenge.API.infrastructure.services.interfaces;

public interface ITokenService
{
   string GenerateJwtToken(string username);
}

