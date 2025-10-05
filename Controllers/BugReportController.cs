using Microsoft.AspNetCore.Mvc;
using FYP_MusicGame_Backend.Models;
using FYP_MusicGame_Backend.Services;
using System.Threading.Tasks;

namespace FYP_MusicGame_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugReportController : ControllerBase
    {
        private readonly IBugReportService _bugReportService;

        public BugReportController(IBugReportService bugReportService)
        {
            _bugReportService = bugReportService;
        }

        // POST: api/BugReport
        [HttpPost]
        public async Task<ActionResult<BugReportResponseDto>> ReportBug(BugReportDto bugReportDto)
        {
            try
            {
                var result = await _bugReportService.CreateBugReportAsync(bugReportDto);
                return CreatedAtAction(nameof(GetBugReport), new { id = result.ReportId }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/BugReport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BugReportResponseDto>> GetBugReport(int id)
        {
            try
            {
                var result = await _bugReportService.GetBugReportByIdAsync(id);
                return result;
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}