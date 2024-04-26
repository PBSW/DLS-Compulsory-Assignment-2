using Shared;

namespace MS_Application.Interfaces;

public interface IMeasurementRepository
{
    public Task<Measurement> CreateMeasurementAsync(Measurement measurement);
    public Task<List<Measurement>> GetAllMeasurementsAsync();
    public Task<List<Measurement>> GetPatientMeasurementsAsync(string ssn);
    public Task<Measurement> UpdateMeasurementAsync(Measurement measurement);
}