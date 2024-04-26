namespace Shared.DTOs.Response;

public class PatientResponse
{
    public string Ssn { get; set; }
    public string Mail { get; set; }
    public string Name { get; set; }
    public Measurement[] measurements { get; set; }
}