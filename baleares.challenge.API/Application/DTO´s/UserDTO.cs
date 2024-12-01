using Microsoft.AspNetCore.Identity;

namespace baleares.challenge.API.Application.DTO_s;

public class UserDTO
{
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public UserDTO(string? firstName, string? lastName, string? password, string? userName, string? email)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            UserName = userName;
            Email = email;
        }
    }


