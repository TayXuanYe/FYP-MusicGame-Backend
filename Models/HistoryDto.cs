using System.Collections.Generic;

namespace FYP_MusicGame_Backend.Models
{
    public class HistoryDto
    {
        public int ChartId { get; set; }
        public int UserId { get; set; }
        public int? HistoryId { get; set; }
        public int Score { get; set; }
        public int MaxCombo { get; set; }
        public float Accuracy { get; set; }
        public float FinalAttention { get; set; }

        public int TapCriticalPerfectCount { get; set; }
        public int TapPerfectCount { get; set; }
        public int TapGreatCount { get; set; }
        public int TapGoodCount { get; set; }
        public int TapMissCount { get; set; }

        public int HoldCriticalPerfectCount { get; set; }
        public int HoldPerfectCount { get; set; }
        public int HoldGreatCount { get; set; }
        public int HoldGoodCount { get; set; }
        public int HoldMissCount { get; set; }

        public List<double> HitTimings { get; set; } = new List<double>();
        public DateTime RecordTime { get; set; }
        public int TrackNo { get; set; }
    }
}