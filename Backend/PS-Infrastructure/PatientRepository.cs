using Microsoft.EntityFrameworkCore;
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
    
        // Check if the SSN already exists
        var existingPatient = await _dbcontext.Patients.FirstOrDefaultAsync(p => p.Ssn == patient.Ssn);
        if (existingPatient != null)
        {
            throw new Exception("A patient with the same SSN already exists");
        }

        var entityEntry = await _dbcontext.Patients.AddAsync(patient);
        await _dbcontext.SaveChangesAsync();

        return entityEntry.Entity; // Access the Entity property to get the added patient entity
    }

    public Task<List<Patient>> GetAllPatientsAsync()
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("GetAllPatientsAsync");
        Monitoring.Log.Debug("Getting all Patients from Database");

        return _dbcontext.Patients.ToListAsync();
    }
    
    public async Task<bool> DeletePatientAsync(Patient request)
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("DeletePatientAsync");
        Monitoring.Log.Debug("Deleting Patient from Database");

        var patient = await _dbcontext.Set<Patient>().FirstOrDefaultAsync(p => p.Ssn == request.Ssn);
        
        if (patient == null)
            return false;
        
        // Patient exists, remove them
        _dbcontext.Set<Patient>().Remove(patient);
        int change = await _dbcontext.SaveChangesAsync();
        
        if (change == 0)
            return false;
        
        return true;
    }
}