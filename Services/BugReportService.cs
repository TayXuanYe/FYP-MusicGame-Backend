using FYP_MusicGame_Backend.Data;
using FYP_MusicGame_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FYP_MusicGame_Backend.Services
{
    public class BugReportService : IBugReportService
    {
        private readonly ApplicationDbContext _context;

        public BugReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BugReportResponseDto> CreateBugReportAsync(BugReportDto bugReportDto)
        {
            // Verify user exists
            var user = await _context.Users.FindAsync(bugReportDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {bugReportDto.UserId} not found");
            }

            var bugReport = new BugReport
            {
                ReporterId = bugReportDto.UserId,
                Reporter = user,
                Title = bugReportDto.Title,
                Description = bugReportDto.Description,
                StepsToReproduce = bugReportDto.StepsToReproduce,
                ReportTime = DateTime.Now,
                Status = "Pending"
            };

            _context.BugReports.Add(bugReport);
            await _context.SaveChangesAsync();

            return MapToResponseDto(bugReport);
        }

        public async Task<BugReportResponseDto> GetBugReportByIdAsync(int id)
        {
            var bugReport = await _context.BugReports.FindAsync(id);
            if (bugReport == null)
            {
                throw new KeyNotFoundException($"Bug report with ID {id} not found");
            }

            return MapToResponseDto(bugReport);
        }

        private BugReportResponseDto MapToResponseDto(BugReport bugReport)
        {
            return new BugReportResponseDto
            {
                ReportId = bugReport.ReportId,
                ReporterId = bugReport.ReporterId,
                Title = bugReport.Title,
                Description = bugReport.Description,
                StepsToReproduce = bugReport.StepsToReproduce!,
                ReportTime = bugReport.ReportTime,
                Status = bugReport.Status
            };
        }
    }
}