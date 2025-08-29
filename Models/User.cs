public class User
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required bool IsAdmin { get; set; }
    
    public UserGameSetting? UserGameSetting { get; set; }
}