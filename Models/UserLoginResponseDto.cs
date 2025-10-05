// UserLoginResponseDto.cs

public class UserLoginResponseDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required bool IsLogin { get; set; }
    public required string AuthToken { get; set; }
    
    public string? SuggestedDifficulty { get; set; }
    public float MasterVolume { get; set; } = 1.0f;
    public float EffectVolume { get; set; } = 1.0f;
    public float MusicVolume { get; set; } = 1.0f;
}