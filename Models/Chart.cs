namespace FYP_MusicGame_Backend.Models
{
    public class Chart
    {
        public required int Id { get; set; }
        public required int SongId { get; set; }
        public required string Artist { get; set; }
        public required string Difficulty { get; set; }
        public required float Level { get; set; }
        public required string Designer { get; set; }
        public required DateTime UploadedAt { get; set; }

        public required Song Song { get; set; }
    }
}
