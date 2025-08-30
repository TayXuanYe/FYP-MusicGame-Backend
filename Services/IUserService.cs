// IUserService.cs

using FYP_MusicGame_Backend.Models;
public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string name);
    Task<Result<User>> CreateUserAsync(UserDto userDto);
    Task<Result<bool>> UpdateUserAsync(UserDto userDto);
    Task<Result<bool>> DeleteUserAsync(int id);
}