// UserLoginResponseDto.cs

public class UserLoginResponseDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required bool IsLogin { get; set; }
    public required string AuthToken { get; set; }
}