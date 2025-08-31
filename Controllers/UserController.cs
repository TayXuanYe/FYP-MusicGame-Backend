// UserController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FYP_MusicGame_Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("by-username/{username}")]
    [Authorize]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        try
        {
            var newUserResult = await _userService.CreateUserAsync(userDto);
            if (!newUserResult.IsSuccess)
            {
                return BadRequest(newUserResult.ErrorMessage);
            }
            var newUser = newUserResult.Value;
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var authResult = await _userService.AuthenticateUserAsync(loginDto.Username, loginDto.Password);

        if (!authResult.IsSuccess)
        {
            return Unauthorized(authResult.ErrorMessage);
        }

        return Ok(authResult.Value);
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
    {
        var updateResult = await _userService.UpdateUserAsync(userDto);
        if (!updateResult.IsSuccess)
        {
            if (Equals(updateResult.ErrorMessage, "User not found."))
            {
                return NotFound();
            }

            return BadRequest(updateResult.ErrorMessage);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleteResult = await _userService.DeleteUserAsync(id);
        if (!deleteResult.IsSuccess)
        {
            return NotFound();
        }
        return NoContent();
    }
}