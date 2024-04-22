using PS_Application.Interfaces;
using Shared;
using Shared.Monitoring;

namespace PS_Infrastructure;

public class PatientRepository : IPatientRepository
{
    private readonly DatabaseContext _dbcontext;
    
    public PatientRepository(DatabaseContext dbcontext)
    {
        if (dbcontext == null)
        {
            Monitoring.Log.Error("PatientRepository database context is null");
            throw new NullReferenceException("PatientRepository database context is null");
        }
        _dbcontext = dbcontext;
        _dbcontext.Database.EnsureCreated();
    }


    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("CreatePatientAsync");
        Monitoring.Log.Debug("Creating Patient in Database");
        
        var entity = await _dbcontext.Patients.AddAsync(patient);
        await _dbcontext.SaveChangesAsync();

        return patient;
    }
}