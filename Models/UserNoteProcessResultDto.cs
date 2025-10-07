public class UserNoteProcessResultDto
{
    public int LaneIndex { get; set; }
    public string? NoteType { get; set; }
    public string? HitResult { get; set; }
    public double HitTime { get; set; }
    public double TimeDifference { get; set; } // Only for Tap notes
    public double TargetHitTime { get; set; }
    public double SystemTime { get; set; }
    public double DurationTime { get; set; } // Only for Hold notes
    public double HoldTotalTime { get; set; } // Only for Hold notes
    public double HoldTimeRatio => DurationTime > 0 ? HoldTotalTime / DurationTime : 0; // Only for Hold notes
    protected UserNoteProcessResultDto() { }
    public static UserNoteProcessResultDto CreateTapNoteResult(int laneIndex, string hitResult, double hitTime, double timeDifference, double targetHitTime, double systemTime)
    {
        return new UserNoteProcessResultDto
        {
            LaneIndex = laneIndex,
            NoteType = "Tap",
            HitResult = hitResult,
            HitTime = hitTime,
            TimeDifference = timeDifference,
            TargetHitTime = targetHitTime,
            SystemTime = systemTime,
            DurationTime = 0 // Not applicable for Tap notes
        };
    }

    public static UserNoteProcessResultDto CreateHoldNoteResult(int laneIndex, string hitResult, double hitTime, double targetHitTime, double systemTime, double durationTime, double holdTotalTime)
    {
        return new UserNoteProcessResultDto
        {
            LaneIndex = laneIndex,
            NoteType = "Hold",
            HitResult = hitResult,
            HitTime = hitTime,
            TargetHitTime = targetHitTime,
            SystemTime = systemTime,
            DurationTime = durationTime,
            HoldTotalTime = holdTotalTime
        };
    }

}