// IUserService.cs

using FYP_MusicGame_Backend.Models;
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByUsernameAsync(string name);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<Result<UserLoginResponseDto>> CreateUserAsync(UserDto userDto);
    Task<Result<bool>> UpdateUserAsync(UserDto userDto);
    Task<Result<bool>> DeleteUserAsync(int id);
    Task<Result<UserLoginResponseDto>> AuthenticateUserAsync(string username, string password);
}