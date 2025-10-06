using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_MusicGame_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FYP_MusicGame_Backend.Data
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public HistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GameHistory> GetHistoryByIdAsync(int historyId)
        {
            return await _context.GameHistories
                .FirstOrDefaultAsync(h => h.HistoryId == historyId);
        }

        public async Task<List<GameHistory>> GetHistoriesByUserIdAsync(int userId)
        {
            return await _context.GameHistories
                .Where(h => h.UserId == userId)
                .ToListAsync();
        }

        public async Task<GameHistory> CreateHistoryAsync(GameHistory history)
        {
            _context.GameHistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }
    }
}