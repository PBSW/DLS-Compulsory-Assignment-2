using Shared;

namespace PS_Application.Interfaces;

public interface IPatientRepository
{
    public Task<int> CreatePatientAsync(Patient request);
}