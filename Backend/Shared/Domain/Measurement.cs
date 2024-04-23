namespace Shared;

public class Measurement
{
    public int Id { get; set; }
    public DateTime date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public bool Seen { get; set; }
}