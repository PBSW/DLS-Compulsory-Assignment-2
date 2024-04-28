using Shared;

namespace PS_Application.Interfaces;

public interface IPatientRepository
{
    public Task<Patient> CreatePatientAsync(Patient request);
    public Task<bool> DeletePatientAsync(Patient request);
    public Task<List<Patient>> GetAllPatientsAsync();
    public Task<Patient> GetPatientBySSNAsync(string ssn);
    public Task<bool> IsPatientAsync(string ssn);
}