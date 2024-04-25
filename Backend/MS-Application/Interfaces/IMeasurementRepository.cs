using Shared;

namespace MS_Application.Interfaces;

public interface IMeasurementRepository
{
    public Task<Measurement> CreateMeasurementAsync(Measurement measurement);
}