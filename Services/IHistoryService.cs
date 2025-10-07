using System.Collections.Generic;
using System.Threading.Tasks;
using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Services
{
    public interface IHistoryService
    {
        Task<HistoryDto> GetHistoryByIdAsync(int historyId);
        Task<List<HistoryDto>> AnalyzeGameDataAsync(List<PerChartAnalysisRequest> requests);
    }

    public class PerChartAnalysisRequest
    {
        [System.Text.Json.Serialization.JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("chart_id")]
        public int ChartId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("user_input_data")]
        public List<UserNoteProcessResultDto>? UserInputData { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("user_gaze_data")]
        public List<GazeDataDto>? UserGazeData { get; set; }
    }
        
}