public class GazeDataDto
{
    public double X { get; set; }
    public double Y { get; set; }
    public int Confidence { get; set; }
    public double Timestamp { get; set; }

    public GazeDataDto(double x, double y, int confidence, double timestamp)
    {
        X = x;
        Y = y;
        Confidence = confidence;
        Timestamp = timestamp;
    }
}