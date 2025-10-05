using FYP_MusicGame_Backend.Data;
using FYP_MusicGame_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP_MusicGame_Backend.Services
{
    public class BugReportService : IBugReportService
    {
        private readonly IBugReportRepository _bugReportRepository;
        private readonly IUserRepository _userRepository;

        public BugReportService(IBugReportRepository bugReportRepository, IUserRepository userRepository)
        {
            _bugReportRepository = bugReportRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<BugReportResponseDto>> CreateBugReportAsync(BugReportDto bugReportDto)
        {
            // Verify user exists
            var user = await _userRepository.GetUserByIdAsync(bugReportDto.UserId);
            if (user == null)
            {
                return new Result<BugReportResponseDto>($"User with ID {bugReportDto.UserId} not found");
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

            var createdReport = await _bugReportRepository.AddBugReportAsync(bugReport);
            return new Result<BugReportResponseDto>(MapToResponseDto(createdReport));
        }

        public async Task<Result<BugReportResponseDto>> GetBugReportByIdAsync(int id)
        {
            var bugReport = await _bugReportRepository.GetBugReportByIdAsync(id);
            if (bugReport == null)
            {
                return new Result<BugReportResponseDto>($"Bug report with ID {id} not found");
            }

            return new Result<BugReportResponseDto>(MapToResponseDto(bugReport));
        }

        public async Task<Result<IEnumerable<BugReportResponseDto>>> GetBugReportsByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new Result<IEnumerable<BugReportResponseDto>>($"User with ID {userId} not found");
            }

            var bugReports = await _bugReportRepository.GetBugReportsByUserIdAsync(userId);
            var bugReportDtos = bugReports.Select(MapToResponseDto);
            
            return new Result<IEnumerable<BugReportResponseDto>>(bugReportDtos);
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