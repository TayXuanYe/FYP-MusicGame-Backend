using FYP_MusicGame_Backend.Models;
using System.Threading.Tasks;

namespace FYP_MusicGame_Backend.Services
{
    public interface IBugReportService
    {
        Task<BugReportResponseDto> CreateBugReportAsync(BugReportDto bugReportDto);
        Task<BugReportResponseDto> GetBugReportByIdAsync(int id);
    }
}