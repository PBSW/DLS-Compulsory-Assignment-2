namespace Shared.DTOs.Update;

public class MeasurementUpdate
{
    public int Id { get; set; }
    public DateTime date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public bool Seen { get; set; }
    public string PatientSSN { get; set; }
}