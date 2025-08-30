namespace FYP_MusicGame_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdmin { get; set; }

        public UserGameSetting? UserGameSetting { get; set; }
    }
}