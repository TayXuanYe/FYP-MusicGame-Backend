using System.Collections.Generic;
using System.Threading.Tasks;
using FYP_MusicGame_Backend.Models;
using FYP_MusicGame_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FYP_MusicGame_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{historyId}")]
        public async Task<ActionResult<HistoryDto>> GetHistoryById(int historyId)
        {
            var history = await _historyService.GetHistoryByIdAsync(historyId);
            
            if (history == null)
            {
                return NotFound(new { message = "History not found" });
            }

            return Ok(history);
        }

        [HttpPost("analyze")]
        public async Task<ActionResult<List<HistoryDto>>> AnalyzeGameData([FromBody] GameDataAnalysisRequest request)
        {
            if (request == null || request.UserRawInputData == null || request.UserRawGazeData == null)
            {
                return BadRequest(new { message = "Invalid request data" });
            }

            var results = await _historyService.AnalyzeGameDataAsync(request);
            return Ok(results);
        }
    }
}