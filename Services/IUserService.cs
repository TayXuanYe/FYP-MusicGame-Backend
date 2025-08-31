// IUserService.cs

using FYP_MusicGame_Backend.Models;
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByUsernameAsync(string name);
    Task<Result<UserDto>> CreateUserAsync(UserDto userDto);
    Task<Result<bool>> UpdateUserAsync(UserDto userDto);
    Task<Result<bool>> DeleteUserAsync(int id);
}