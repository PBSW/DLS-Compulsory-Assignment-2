using Shared;

namespace PS_Application.Interfaces;

public interface IPatientRepository
{
    public Task<Patient> CreatePatientAsync(Patient request);
    public Task<bool> DeletePatientAsync(Patient request);
}