using Shared.DTOs.Create;
using Shared.DTOs.Response;
using Shared.DTOs.Update;

namespace MS_Application.Interfaces;

public interface IMeasurementService
{
    public Task<MeasurementResponse> CreateMeasurementAsync(MeasurementCreate request);
    public Task<List<MeasurementResponse>> GetAllMeasurementsAsync();
    public Task<MeasurementResponse> UpdateMeasurementAsync(MeasurementUpdate request);
    public Task<List<MeasurementResponse>> GetPatientMeasurementsAsync(string ssn);
}