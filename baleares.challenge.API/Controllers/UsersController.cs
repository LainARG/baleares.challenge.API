using baleares.challenge.API.infrastructure.services.utilities;
using baleares.challenge.API.model.users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using baleares.challenge.API.Application.DTO_s;
using baleares.challenge.API.Application.Validation;

namespace Baleares.Challenge.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet("sortByEmail")]
    public async Task<IActionResult> GetUsersSortedByEmail()
    {
        try
        {
            var users = await _userManager.Users.OrderBy(u => u.Email).ToListAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO usr)
    {
        try
        {
            if (!ModelValidator.ValidateUser(usr)) return BadRequest(ModelValidator.ErrorMessage);
            var result = await _userManager.CreateAsync(new User
            {
                UserName = usr.UserName,
                Email = usr.Email,
                FirstName = usr.FirstName,
                LastName = usr.LastName
            }, usr.Password);
            return result.Succeeded ? Ok("Usuario Registrado Exitosamente.") : BadRequest("Usuario no registrado: " + string.Join(", ", result.Errors.Select(err => err.Description)));
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }
}






