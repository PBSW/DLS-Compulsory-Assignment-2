using Shared;

namespace PS_Application.Interfaces;

public interface IPatientRepository
{
    public Task<Patient> CreatePatientAsync(Patient request);
}