// UserService.cs

using FYP_MusicGame_Backend.Models;
using System.Text.RegularExpressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();

        var userDtos = users.Select(user => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        }).ToList();

        return userDtos;
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<Result<UserLoginResponseDto>> CreateUserAsync(UserDto userDto)
    {
        // validate input
        var usernameValidateResult = await ValidateUsernameAsync(userDto.Username);
        if (!usernameValidateResult.isValid)
        {
            return Result<UserLoginResponseDto>.Failure(usernameValidateResult.errorMessage);
        }

        var emailValidateResult = await ValidateUserEmailAsync(userDto.Email);
        if (!emailValidateResult.isValid)
        {
            return Result<UserLoginResponseDto>.Failure(emailValidateResult.errorMessage);
        }

        var newUser = new User
        {
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email,
        };

        // add user
        await _userRepository.AddUserAsync(newUser);

        // spawn jwt token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, newUser!.Username),
            new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString())
        };
        var jwtToken = _tokenService.GenerateJwtToken(claims);

        var newUserDto = new UserLoginResponseDto
        {
            Id = newUser.Id,
            Username = newUser.Username,
            Email = newUser.Email,
            IsLogin = true,
            AuthToken = jwtToken
        };
        return Result<UserLoginResponseDto>.Success(newUserDto);
    }

    public async Task<Result<bool>> UpdateUserAsync(UserDto userDto)
    {
        var userToUpdate = await _userRepository.GetUserByIdAsync(userDto.Id);

        if (userToUpdate == null)
        {
            return Result<bool>.Failure("User not found.");
        }

        var usernameValidateResult = await ValidateUsernameAsync(userDto.Username);
        if (!usernameValidateResult.isValid)
        {
            return Result<bool>.Failure(usernameValidateResult.errorMessage);
        }

        var emailValidateResult = await ValidateUserEmailAsync(userDto.Email);
        if (!emailValidateResult.isValid)
        {
            return Result<bool>.Failure(emailValidateResult.errorMessage);
        }

        await _userRepository.UpdateUserAsync(userToUpdate);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteUserAsync(int id)
    {
        var userToDelete = await _userRepository.GetUserByIdAsync(id);

        if (userToDelete == null)
        {
            return Result<bool>.Failure("User not found.");
        }

        await _userRepository.DeleteUserAsync(id);
        return Result<bool>.Success(true);
    }

    private async Task<(bool isValid, string errorMessage)> ValidateUsernameAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return (false, "Username are required.");
        }

        var user = await GetUserByUsernameAsync(username);
        if (user != null)
        {
            return (false, "Username already exists.");
        }

        return (true, "");
    }

    private async Task<(bool isValid, string errorMessage)> ValidateUserEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return (false, "Email are required.");
        }

        string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        if (!Regex.IsMatch(email, emailPattern))
        {
            return (false, "Invalid email format.");
        }

        var getEmailResult = await GetUserByEmailAsync(email);
        if (getEmailResult != null)
        {
            return (false, "Email already exists.");
        }

        return (true, "");
    }

    public async Task<Result<UserLoginResponseDto>> AuthenticateUserAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
        {
            return Result<UserLoginResponseDto>.Failure("User not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Result<UserLoginResponseDto>.Failure("Password incorrect.");
        }

        // spawn jwt token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var jwtToken = _tokenService.GenerateJwtToken(claims);

        var userDto = new UserLoginResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsLogin = true,
            AuthToken = jwtToken
        };

        return Result<UserLoginResponseDto>.Success(userDto);
    }

    public async Task<Result<UserLoginResponseDto>> AuthenticateAdminAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
        {
            return Result<UserLoginResponseDto>.Failure("User not found.");
        }

        if (!user.IsAdmin)
        {
            return Result<UserLoginResponseDto>.Failure("User is not an admin.");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Result<UserLoginResponseDto>.Failure("Password incorrect.");
        }

        // spawn jwt token with admin role
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var jwtToken = _tokenService.GenerateJwtToken(claims);

        var userDto = new UserLoginResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsLogin = true,
            AuthToken = jwtToken
        };

        return Result<UserLoginResponseDto>.Success(userDto);
    }
}