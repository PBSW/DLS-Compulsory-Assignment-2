using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace PS_Application.Interfaces;

public interface IPatientService
{
    public Task<PatientResponse> CreatePatientAsync(PatientCreate request);
}