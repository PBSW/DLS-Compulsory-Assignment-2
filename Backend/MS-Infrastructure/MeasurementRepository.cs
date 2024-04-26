using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Measurement>> GetAllMeasurementsAsync()
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetAllMeasurementsAsync");
        Monitoring.Log.Debug("Getting all Measurements");
        
        return await _dbcontext.Measurements.ToListAsync();
    }

    public async Task<List<Measurement>> GetPatientMeasurementsAsync(string ssn)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetPatientMeasurementsAsync");
        Monitoring.Log.Debug("Getting Patient Measurements by SSN");
        
        return await _dbcontext.Measurements.Where(m => m.PatientSSN == ssn).ToListAsync();
    }

    public async Task<Measurement> UpdateMeasurementAsync(Measurement measurement)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("UpdateMeasurementAsync");
        Monitoring.Log.Debug("Updating Measurement");
        
        var entity = _dbcontext.Measurements.Update(measurement);
        int change = await _dbcontext.SaveChangesAsync();
        
        if (change == 0)
        {
            Monitoring.Log.Error("No changes were made to the database");
            throw new Exception("No changes were made to the database");
        }
        
        return entity.Entity;
    }
}