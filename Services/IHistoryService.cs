using System.Collections.Generic;
using System.Threading.Tasks;
using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Services
{
    public interface IHistoryService
    {
        Task<HistoryDto> GetHistoryByIdAsync(int historyId);
        Task<List<HistoryDto>> AnalyzeGameDataAsync(GameDataAnalysisRequest request);
    }

    public class GameDataAnalysisRequest
    {
        public List<object> UserRawInputData { get; set; } = new List<object>();
        public List<object> UserRawGazeData { get; set; } = new List<object>();
    }
}