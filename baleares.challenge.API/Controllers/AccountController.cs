using baleares.challenge.API.infrastructure.services.interfaces;
using baleares.challenge.API.infrastructure.services.utilities;
using baleares.challenge.API.Infrastructure.Services.Token;
using baleares.challenge.API.model.users;
using baleares.challenge.API.model.account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Baleares.Challenge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly TokenValidationService _tokenValidationService;

    public AccountController(UserManager<User> userManager, ITokenService tokenService, TokenValidationService tokenValidationService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _tokenValidationService = tokenValidationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login req)
    {
        try
        {
            User? user = await _userManager.FindByNameAsync(req.Username);
            if(user == null) return Unauthorized("Credenciales inválidas.");
            var token = _tokenService.GenerateJwtToken(user.UserName).ToString();
            _tokenValidationService.AddToken(token);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            _tokenValidationService.RevokeTokens();
            return Ok("Logout exitoso.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }
}






