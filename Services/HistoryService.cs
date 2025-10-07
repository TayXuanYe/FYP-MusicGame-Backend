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
        private readonly IUserRepository _userRepository;
        private readonly IChartRepository _chartRepository;

        public HistoryService(IHistoryRepository historyRepository, IUserRepository userRepository, IChartRepository chartRepository)
        {
            _historyRepository = historyRepository;
            _userRepository = userRepository;
            _chartRepository = chartRepository;
        }

        public async Task<List<HistoryDto>> GetHistoryByIdAsync(int historyId)
        {
            var history = await _historyRepository.GetHistoryByIdAsync(historyId);

            if (history == null)
            {
                return new List<HistoryDto>();
            }

            return new List<HistoryDto> { MapToHistoryDto(history) };
        }

        public async Task<List<HistoryDto>> AnalyzeGameDataAsync(List<PerChartAnalysisRequest> requests)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            var histories = new List<HistoryDto>();

            foreach (var request in requests)
            {
                try
                {
                    if (request.UserId <= 0 || request.ChartId <= 0)
                    {
                        continue;
                    }

                    int currentCombo = 0;
                    int maxCombo = 0;
                    double totalSimulateScore = 0;
                    int totalSimulateCount = 0;
                    List<double> timeDifferences = new List<double>();

                    int tapCriticalPerfectCount = 0;
                    int tapPerfectCount = 0;
                    int tapGreatCount = 0;
                    int tapGoodCount = 0;
                    int tapMissCount = 0;

                    int holdCriticalPerfectCount = 0;
                    int holdPerfectCount = 0;
                    int holdGreatCount = 0;
                    int holdGoodCount = 0;
                    int holdMissCount = 0;

                    if (request.UserInputData != null && request.UserInputData.Count > 0)
                    {
                        foreach (var processResult in request.UserInputData)
                        {
                            if (processResult.NoteType == "Tap")
                            {
                                totalSimulateCount++;
                                timeDifferences.Add(processResult.TimeDifference);
                                switch (processResult.HitResult)
                                {
                                    case "CriticalPerfect":
                                        totalSimulateScore += 1;
                                        tapCriticalPerfectCount++;
                                        currentCombo++;
                                        break;
                                    case "Perfect":
                                        totalSimulateScore += 0.9;
                                        tapPerfectCount++;
                                        currentCombo++;
                                        break;
                                    case "Great":
                                        totalSimulateScore += 0.7;
                                        tapGreatCount++;
                                        currentCombo++;
                                        break;
                                    case "Good":
                                        totalSimulateScore += 0.5;
                                        tapGoodCount++;
                                        currentCombo++;
                                        break;
                                    case "Miss":
                                        tapMissCount++;
                                        if (currentCombo > maxCombo)
                                        {
                                            maxCombo = currentCombo;
                                        }
                                        currentCombo = 0;
                                        break;
                                }
                            }
                            else if (processResult.NoteType == "Hold")
                            {
                                double duration = processResult.DurationTime;
                                int count = 1;
                                while (duration > 0)
                                {
                                    duration -= count * 0.3;
                                    count++;
                                }
                                totalSimulateCount += count;
                                switch (processResult.HitResult)
                                {
                                    case "CriticalPerfect":
                                        totalSimulateScore += count * 1;
                                        holdCriticalPerfectCount++;
                                        currentCombo++;
                                        break;
                                    case "Perfect":
                                        totalSimulateScore += count * 0.9;
                                        holdPerfectCount++;
                                        currentCombo++;
                                        break;
                                    case "Great":
                                        totalSimulateScore += count * 0.7;
                                        holdGreatCount++;
                                        currentCombo++;
                                        break;
                                    case "Good":
                                        totalSimulateScore += count * 0.5;
                                        holdGoodCount++;
                                        currentCombo++;
                                        break;
                                    case "Miss":
                                        holdMissCount++;
                                        if (currentCombo > maxCombo)
                                        {
                                            maxCombo = currentCombo;
                                        }
                                        currentCombo = 0;
                                        break;
                                }
                            }

                            if (currentCombo > maxCombo)
                            {
                                maxCombo = currentCombo;
                            }
                        }
                    }

                    if (request.UserGazeData != null && request.UserGazeData.Count > 0)
                    {
                        // future implementation for attention calculation
                    }

                    if (timeDifferences.Count == 0)
                    {
                        timeDifferences.Add(0);
                    }

                    double average = timeDifferences.Average();
                    double variance = timeDifferences.Sum(d => Math.Pow(d - average, 2)) / timeDifferences.Count;
                    double std = Math.Sqrt(variance);
                    double score = totalSimulateScore / Math.Max(1, totalSimulateCount) * 1000000;
                    double accuracy = std * 1000;

                    double finalAttention = 0;

                    var history = new GameHistory
                    {
                        Id = 0,
                        PlayedAt = DateTime.UtcNow,
                        User = await _userRepository.GetUserByIdAsync(request.UserId) ?? throw new InvalidOperationException($"User with ID {request.UserId} not found."),
                        Chart = await _chartRepository.GetByIdAsync(request.ChartId) ?? throw new InvalidOperationException($"Chart with ID {request.ChartId} not found."),
                        ChartId = request.ChartId,
                        UserId = request.UserId,
                        Score = (int)score,
                        MaxCombo = maxCombo,
                        Accuracy = (float)accuracy,
                        FinalAttention = (float)finalAttention,
                        TapCriticalPerfectCount = tapCriticalPerfectCount,
                        TapPerfectCount = tapPerfectCount,
                        TapGreatCount = tapGreatCount,
                        TapGoodCount = tapGoodCount,
                        TapMissCount = tapMissCount,
                        HoldCriticalPerfectCount = holdCriticalPerfectCount,
                        HoldPerfectCount = holdPerfectCount,
                        HoldGreatCount = holdGreatCount,
                        HoldGoodCount = holdGoodCount,
                        HoldMissCount = holdMissCount,
                        HitTimings = timeDifferences,
                        TrackNo = request.TrackNo
                    };

                    await _historyRepository.CreateHistoryAsync(history);
                    histories.Add(MapToHistoryDto(history));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing request for ChartId {request.ChartId}: {ex.Message}");
                    continue;
                }
            }

            return histories;
        }

        public async Task<List<HistoryDto>> GetHistoryByUserIdAsync(int userId)
        {
            var histories = await _historyRepository.GetHistoriesByUserIdAsync(userId);

            if (histories == null || histories.Count == 0)
            {
                return new List<HistoryDto>();
            }

            return histories.Select(MapToHistoryDto).ToList();
        }

        private HistoryDto MapToHistoryDto(GameHistory history)
        {
            return new HistoryDto
            {
                ChartId = history.ChartId,
                UserId = history.UserId,
                HistoryId = history.Id,
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
                HitTimings = history.HitTimings,
                RecordTime = history.PlayedAt,
                TrackNo = history.TrackNo
            };
        }
    }
}