namespace FYP_MusicGame_Backend.Models
{
    public class Song
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Artist { get; set; }
        public required int Bpm { get; set; }
        public required DateTime UploadedAt { get; set; }
    }
}