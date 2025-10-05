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
        
        // User setting        
        public string? SuggestedDifficulty { get; set; }
        public float MasterVolume { get; set; } = 1.0f;
        public float EffectVolume { get; set; } = 1.0f;
        public float MusicVolume { get; set; } = 1.0f;
    }
}