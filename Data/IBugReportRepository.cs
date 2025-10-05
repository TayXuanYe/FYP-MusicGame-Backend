using FYP_MusicGame_Backend.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FYP_MusicGame_Backend.Data
{
    public interface IBugReportRepository
    {
        Task<IEnumerable<BugReport>> GetAllBugReportsAsync();
        Task<BugReport?> GetBugReportByIdAsync(int id);
        Task<IEnumerable<BugReport>> GetBugReportsByUserIdAsync(int userId);
        Task<BugReport> AddBugReportAsync(BugReport bugReport);
        Task UpdateBugReportAsync(BugReport bugReport);
    }
}