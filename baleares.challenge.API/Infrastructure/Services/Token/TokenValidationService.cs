namespace baleares.challenge.API.Infrastructure.Services.Token
{
    public class TokenValidationService
    {
        private readonly HashSet<string> _allowedTokens = new HashSet<string>();

        public void AddToken(string token)
        {
            _allowedTokens.Add(token);
        }

        public bool IsTokenAllowed(string token)
        {
            return _allowedTokens.Contains(token);
        }

        public void RevokeTokens()
        {
            _allowedTokens.Clear();
        }

    }
}
