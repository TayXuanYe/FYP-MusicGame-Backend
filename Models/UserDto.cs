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
        public bool IsLogin { get; set; }
    }
}