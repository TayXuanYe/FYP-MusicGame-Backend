// UserDto.cs

using Microsoft.AspNetCore.SignalR;

namespace FYP_MusicGame_Backend.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }

        public string? SuggestedDifficulty { get; set; }
        public float MasterVolume { get; set; } = 1.0f;
        public float EffectVolume { get; set; } = 1.0f;
        public float MusicVolume { get; set; } = 1.0f;
    }
}