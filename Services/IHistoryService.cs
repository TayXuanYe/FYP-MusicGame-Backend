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
        public List<object> DataList1 { get; set; } = new List<object>();
        public List<object> DataList2 { get; set; } = new List<object>();
    }
}