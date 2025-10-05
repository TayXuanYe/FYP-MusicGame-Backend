using FYP_MusicGame_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP_MusicGame_Backend.Services
{
    public interface IBugReportService
    {
        Task<Result<BugReportResponseDto>> CreateBugReportAsync(BugReportDto bugReportDto);
        Task<Result<BugReportResponseDto>> GetBugReportByIdAsync(int id);
        Task<Result<IEnumerable<BugReportResponseDto>>> GetBugReportsByUserIdAsync(int userId);
    }
}