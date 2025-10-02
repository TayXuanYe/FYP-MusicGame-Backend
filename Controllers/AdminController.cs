// AdminController.cs

using Microsoft.AspNetCore.Mvc;
using FYP_MusicGame_Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    
    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> AdminLogin([FromBody] UserLoginDto loginDto)
    {
        var authResult = await _userService.AuthenticateAdminAsync(loginDto.Username, loginDto.Password);

        if (!authResult.IsSuccess)
        {
            return Unauthorized(authResult.ErrorMessage);
        }

        return Ok(authResult.Value);
    }
}