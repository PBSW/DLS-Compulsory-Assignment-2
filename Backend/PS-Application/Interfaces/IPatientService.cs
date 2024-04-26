using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace PS_Application.Interfaces;

public interface IPatientService
{
    public Task<PatientResponse> CreatePatientAsync(PatientCreate request);
    public Task<bool> DeletePatientAsync(PatientDelete request);
    public Task<List<PatientResponse>> GetAllPatientsAsync();
    public Task<bool> IsPatientAsync(string ssn);
}