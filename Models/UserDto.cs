// UserDto.cs

namespace FYP_MusicGame_Backend.Models
{
    public class UserDto
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool IsAdmin { get; set; }
    }
}