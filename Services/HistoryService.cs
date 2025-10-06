using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FYP_MusicGame_Backend.Data;
using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<HistoryDto> GetHistoryByIdAsync(int historyId)
        {
            var history = await _historyRepository.GetHistoryByIdAsync(historyId);

            if (history == null)
            {
                return null;
            }

            return MapToHistoryDto(history);
        }

        public async Task<List<HistoryDto>> AnalyzeGameDataAsync(GameDataAnalysisRequest request)
        {
            // 这里是占位符，之后会实现分析逻辑
            // 目前只返回一个空列表
            return new List<HistoryDto>();
        }

        private HistoryDto MapToHistoryDto(GameHistory history)
        {
            return new HistoryDto
            {
                ChartId = history.ChartId,
                UserId = history.UserId,
                HistoryId = history.HistoryId,
                Score = history.Score,
                MaxCombo = history.MaxCombo,
                Accuracy = history.Accuracy,
                FinalAttention = history.FinalAttention,
                TapCriticalPerfectCount = history.TapCriticalPerfectCount,
                TapPerfectCount = history.TapPerfectCount,
                TapGreatCount = history.TapGreatCount,
                TapGoodCount = history.TapGoodCount,
                TapMissCount = history.TapMissCount,
                HoldCriticalPerfectCount = history.HoldCriticalPerfectCount,
                HoldPerfectCount = history.HoldPerfectCount,
                HoldGreatCount = history.HoldGreatCount,
                HoldGoodCount = history.HoldGoodCount,
                HoldMissCount = history.HoldMissCount,
                HitTimings = history.HitTimings
            };
        }
    }
}