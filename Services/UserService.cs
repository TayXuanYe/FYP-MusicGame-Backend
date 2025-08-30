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

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return _userRepository.GetAllUsersAsync();
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        return _userRepository.GetUserByIdAsync(id);
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<Result<User>> CreateUserAsync(UserDto userDto)
    {
        var usernameValidateResult = ValidateUsername(userDto.Username);
        if (!usernameValidateResult.isValid)
        {
            return Result<User>.Failure(usernameValidateResult.errorMessage);
        }

        var emailValidateResult = ValidateUserEmail(userDto.Email);
        if (!emailValidateResult.isValid)
        {
            return Result<User>.Failure(emailValidateResult.errorMessage);
        }

        var newUser = new User
        {
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email
        };

        await _userRepository.AddUserAsync(newUser);
        return Result<User>.Success(newUser);
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
        if (Regex.IsMatch(email, emailPattern))
        {
            return (false, "Invalid email format.");
        }

        return (true, "");
    }
}