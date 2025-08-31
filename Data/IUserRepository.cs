// IUserRepository.cs

using FYP_MusicGame_Backend.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string name);
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}