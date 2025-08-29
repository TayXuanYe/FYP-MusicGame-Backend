public class UserGameSetting
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required int Volume { get; set; }

    public required User User { get; set; }
}