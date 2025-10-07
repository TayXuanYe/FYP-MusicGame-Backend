using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Data
{
    public interface IChartRepository
    {
        Task<Chart> GetByIdAsync(int chartId);
        Task<List<Chart>> GetAllAsync();
        Task AddAsync(Chart chart);
        Task UpdateAsync(Chart chart);
        Task DeleteAsync(int chartId);
    }
}