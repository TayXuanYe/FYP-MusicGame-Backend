// UserRepository.cs

using FYP_MusicGame_Backend.Data;
using FYP_MusicGame_Backend.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, name));
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => string.Equals(u.Email, email));
    }

    public async Task AddUserAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var userToRemove = await _dbContext.Users.FindAsync(id);
        if (userToRemove != null)
        {
            _dbContext.Users.Remove(userToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}
