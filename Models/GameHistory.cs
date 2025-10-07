using System.Collections.Generic;

namespace FYP_MusicGame_Backend.Models
{
    public class GameHistory
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int ChartId { get; set; }
        
        public required int Score { get; set; }
        public required int MaxCombo { get; set; }
        public required float Accuracy { get; set; }
        public required float FinalAttention { get; set; }

        public required int TapCriticalPerfectCount { get; set; }
        public required int TapPerfectCount { get; set; }
        public required int TapGreatCount { get; set; }
        public required int TapGoodCount { get; set; }
        public required int TapMissCount { get; set; }

        public required int HoldCriticalPerfectCount { get; set; }
        public required int HoldPerfectCount { get; set; }
        public required int HoldGreatCount { get; set; }
        public required int HoldGoodCount { get; set; }
        public required int HoldMissCount { get; set; }

        public List<double> HitTimings { get; set; } = new List<double>();

        public required DateTime PlayedAt { get; set; }
        public required int TrackNo { get; set; }


        public required User User { get; set; }
        public required Chart Chart { get; set; }
    }
}