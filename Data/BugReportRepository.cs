using FYP_MusicGame_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP_MusicGame_Backend.Data
{
    public class BugReportRepository : IBugReportRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BugReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BugReport>> GetAllBugReportsAsync()
        {
            return await _dbContext.BugReports.ToListAsync();
        }

        public async Task<BugReport?> GetBugReportByIdAsync(int id)
        {
            return await _dbContext.BugReports.FindAsync(id);
        }

        public async Task<IEnumerable<BugReport>> GetBugReportsByUserIdAsync(int userId)
        {
            return await _dbContext.BugReports
                .Where(b => b.ReporterId == userId)
                .ToListAsync();
        }

        public async Task<BugReport> AddBugReportAsync(BugReport bugReport)
        {
            _dbContext.BugReports.Add(bugReport);
            await _dbContext.SaveChangesAsync();
            return bugReport;
        }

        public async Task UpdateBugReportAsync(BugReport bugReport)
        {
            _dbContext.BugReports.Update(bugReport);
            await _dbContext.SaveChangesAsync();
        }
    }
}