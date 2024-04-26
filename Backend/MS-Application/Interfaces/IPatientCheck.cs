namespace MS_Application.Interfaces;

public interface IPatientCheck
{
    Task<bool> CheckPatientExistsAsync(string ssn);
}