using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace MS_Application.Interfaces;

public interface IMeasurementService
{
    public Task<MeasurementResponse> CreateMeasurementAsync(MeasurementCreate request);
}