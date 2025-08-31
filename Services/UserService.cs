// UserService.cs

using FYP_MusicGame_Backend.Models;
using System.Text.RegularExpressions;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

    public async Task<Result<UserDto>> CreateUserAsync(UserDto userDto)
    {
        var usernameValidateResult = ValidateUsername(userDto.Username);
        if (!usernameValidateResult.isValid)
        {
            return Result<UserDto>.Failure(usernameValidateResult.errorMessage);
        }

        var emailValidateResult = ValidateUserEmail(userDto.Email);
        if (!emailValidateResult.isValid)
        {
            return Result<UserDto>.Failure(emailValidateResult.errorMessage);
        }

        var newUser = new User
        {
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email,
        };

        await _userRepository.AddUserAsync(newUser);
        var newUserDto = new UserDto
        {
            Username = newUser.Username,
            Email = newUser.Email,
            IsLogin = true
        };
        return Result<UserDto>.Success(newUserDto);
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
        if(!emailValidateResult.isValid)
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
}