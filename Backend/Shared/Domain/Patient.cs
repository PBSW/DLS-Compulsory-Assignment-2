namespace Shared;

public class Patient
{
    public string Ssn { get; set; }
    public string Mail { get; set; }
    public string Name { get; set; }
    public Measurement[] measurements { get; set; }
}