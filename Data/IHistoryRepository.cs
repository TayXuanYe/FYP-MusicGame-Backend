using System.Collections.Generic;
using System.Threading.Tasks;
using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Data
{
    public interface IHistoryRepository
    {
        Task<GameHistory> GetHistoryByIdAsync(int historyId);
        Task<List<GameHistory>> GetHistoriesByUserIdAsync(int userId);
        Task<GameHistory> CreateHistoryAsync(GameHistory history);
    }
}