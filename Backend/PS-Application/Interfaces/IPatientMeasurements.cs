using Shared;
using Shared.DTOs.Response;

namespace PS_Application.Interfaces;

public interface IPatientMeasurements
{
    public Task<List<MeasurementResponse>> GetMeasurementsAsync(string ssn);
}