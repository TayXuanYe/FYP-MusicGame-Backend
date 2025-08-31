// UserService.cs

using FYP_MusicGame_Backend.Models;
using System.Text.RegularExpressions;
using System.Security.Claims;

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

    public async Task<Result<UserLoginResponseDto>> CreateUserAsync(UserDto userDto)
    {
        // validate input
        var usernameValidateResult = ValidateUsername(userDto.Username);
        if (!usernameValidateResult.isValid)
        {
            return Result<UserLoginResponseDto>.Failure(usernameValidateResult.errorMessage);
        }

        var emailValidateResult = ValidateUserEmail(userDto.Email);
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

        // get new add user
        var newUserGet = await _userRepository.GetUserByUsernameAsync(newUser.Username);

        // spawn jwt token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, newUserGet!.Username),
            new Claim(ClaimTypes.NameIdentifier, newUserGet.Id.ToString())
        };
        var jwtToken = _tokenService.GenerateJwtToken(claims);

        var newUserDto = new UserLoginResponseDto
        {
            Username = newUser.Username,
            Email = newUser.Email,
            IsLogin = true,
            Token = jwtToken
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

        var usernameValidateResult = ValidateUsername(userDto.Username);
        if (!usernameValidateResult.isValid)
        {
            return Result<bool>.Failure(usernameValidateResult.errorMessage);
        }

        var emailValidateResult = ValidateUserEmail(userDto.Email);
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

    private (bool isValid, string errorMessage) ValidateUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return (false, "Username are required.");
        }

        return (true, "");
    }

    private (bool isValid, string errorMessage) ValidateUserEmail(string email)
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

        return (true, "");
    }

    public async Task<Result<UserLoginResponseDto>> AuthenticateUserAsync(string username, string password)
    {
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(username);

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
        {
            return Result<UserLoginResponseDto>.Failure("User not found.");
        }

        if (!Equals(hashPassword, user.PasswordHash))
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
            Token = jwtToken
        };

        return Result<UserLoginResponseDto>.Success(userDto);
    }
}