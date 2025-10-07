using FYP_MusicGame_Backend.Data;
using FYP_MusicGame_Backend.Models;
using Microsoft.EntityFrameworkCore;

public class ChartRepository : IChartRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChartRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Chart> GetByIdAsync(int chartId)
    {
        return await _dbContext.Charts.FindAsync(chartId);
    }

    public async Task<List<Chart>> GetAllAsync()
    {
        return await _dbContext.Charts.ToListAsync();
    }

    public async Task AddAsync(Chart chart)
    {
        _dbContext.Charts.Add(chart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Chart chart)
    {
        _dbContext.Charts.Update(chart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int chartId)
    {
        var chartToRemove = await _dbContext.Charts.FindAsync(chartId);
        if (chartToRemove != null)
        {
            _dbContext.Charts.Remove(chartToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}