namespace FYP_MusicGame_Backend.Models
{
    public class GameHistory
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required int ChartId { get; set; }
        
        public required int MaxCombo { get; set; }
        public required int CriticalPerfectCount { get; set; }
        public required int PerfectCount { get; set; }
        public required int GreatCount { get; set; }
        public required int GoodCount { get; set; }
        public required int MissCount { get; set; }

        public required float Accuracy { get; set; }
        public required float FinalAttention { get; set; }

        public required DateTime PlayedAt { get; set; }

        public required User User { get; set; }
        public required Chart Chart { get; set; }
    }
}