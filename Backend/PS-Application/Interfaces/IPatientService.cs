namespace PS_Application.Interfaces;

public interface IPatientService
{
    public Task<PatientResponse> CreatePatientAsync(PatientRequest request);
}