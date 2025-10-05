using FYP_MusicGame_Backend.Models;
using FYP_MusicGame_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public async Task<IActionResult> CreateBugReport([FromBody] BugReportDto bugReportDto)
        {
            var result = await _bugReportService.CreateBugReportAsync(bugReportDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            
            return CreatedAtAction(nameof(GetBugReportById), new { id = result.Value.ReportId }, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBugReportById(int id)
        {
            var result = await _bugReportService.GetBugReportByIdAsync(id);
            
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }
            
            return Ok(result.Value);
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBugReportsByUserId(int userId)
        {
            var result = await _bugReportService.GetBugReportsByUserIdAsync(userId);
            
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }
            
            return Ok(result.Value);
        }
    }
}