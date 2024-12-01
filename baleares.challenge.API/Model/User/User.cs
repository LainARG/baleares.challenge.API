using Microsoft.AspNetCore.Identity;

namespace baleares.challenge.API.model.users;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
