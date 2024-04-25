using MS_Application.Interfaces;
using Shared;
using Shared.Monitoring;

namespace MS_Infrastructure;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly DatabaseContext _dbcontext;
    
    public MeasurementRepository(DatabaseContext dbcontext)
    {
        _dbcontext = dbcontext;
        _dbcontext.Database.EnsureCreated();
    }

    public async Task<Measurement> CreateMeasurementAsync(Measurement measurement)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("CreateMeasurementAsync");
        Monitoring.Log.Debug("Creating Measurement");
        
        var entity = await _dbcontext.Measurements.AddAsync(measurement);
        int change = await _dbcontext.SaveChangesAsync();
        
        if (change == 0)
        {
            Monitoring.Log.Error("No changes were made to the database");
            throw new Exception("No changes were made to the database");
        }
        
        return entity.Entity;
    }

    public Task<List<Measurement>> GetAllMeasurementsAsync()
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetAllMeasurementsAsync");
        Monitoring.Log.Debug("Getting all Measurements");
        
        return Task.FromResult(_dbcontext.Measurements.ToList());
    }
}