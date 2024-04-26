using AutoMapper;
using FluentValidation;
using PS_Application.Interfaces;
using Shared;
using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;
using Shared.Monitoring;

namespace PS_Application;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    private readonly IPatientMeasurements _measurements;
    private readonly IMapper _mapper;
    private readonly IValidator<Patient> _validator;
    
    public PatientService(IPatientRepository repo, IPatientMeasurements measurements, IMapper mapper, IValidator<Patient> validator)
    {
        _repo = repo ?? throw new NullReferenceException("PatientService repository is null");
        _measurements = measurements ?? throw new NullReferenceException("PatientService measurements is null");
        _mapper = mapper ?? throw new NullReferenceException("PatientService mapper is null");
        _validator = validator ?? throw new NullReferenceException("PatientService validator is null");

    }

    public async Task<PatientResponse> CreatePatientAsync(PatientCreate request)
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("CreatePatientAsync");
        Monitoring.Log.Debug("Creating Patient");
        
        if (request == null)
        {
            throw new NullReferenceException("PatientCreate is null");
        }
        
        Patient patient = _mapper.Map<Patient>(request);
        
        var validation = await _validator.ValidateAsync(patient);
        if (!validation.IsValid)
        {
            Monitoring.Log.Error(validation.ToString());
            throw new ValidationException(validation.ToString());
        }
        
        Patient patientReturn = await _repo.CreatePatientAsync(patient);
        
        PatientResponse response = _mapper.Map<PatientResponse>(patientReturn);

        return response;
    }

    public async Task<List<PatientResponse>> GetAllPatientsAsync()
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("GetAllPatientsAsync");
        Monitoring.Log.Debug("Getting all Patients");

        List<Patient> patientList = await _repo.GetAllPatientsAsync();

        if (patientList == null)
        {
            throw new NullReferenceException("Patient list from Repo is null");
        }
        
        List<PatientResponse> patientResponseList = _mapper.Map<List<PatientResponse>>(patientList);
        
        return patientResponseList;
    }
    
    public async Task<PatientResponse> GetPatientBySSNAsync(string ssn)
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("GetPatientBySSNAsync");
        Monitoring.Log.Debug("Getting Patient by SSN");

        if (string.IsNullOrEmpty(ssn))
        {
            Monitoring.Log.Error("SSN is invalid");
            throw new NullReferenceException("SSN is invalid");
        }
        
        Patient patient = await _repo.GetPatientBySSNAsync(ssn);
        List<MeasurementResponse> measurements = await _measurements.GetMeasurementsAsync(ssn);
        
        if (patient == null)
        {
            Monitoring.Log.Error("Patient from DB is null");
            throw new NullReferenceException("Patient from DB is null");
        }
        
        PatientResponse response = _mapper.Map<PatientResponse>(patient);
        response.Measurements = measurements;
        
        return response;
    }
    
    public async Task<bool> IsPatientAsync(string ssn)
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("IsPatientAsync");
        Monitoring.Log.Debug("Checking if Patient exists");

        if (string.IsNullOrEmpty(ssn))
        {
            Monitoring.Log.Error("SSN is invalid");
            throw new NullReferenceException("SSN is invalid");
        }
        
        bool action = await _repo.IsPatientAsync(ssn);

        return action;
    }
    
    public async Task<bool> DeletePatientAsync(PatientDelete request)
    {
        // Monitoring and Logging
        using var activity = Monitoring.ActivitySource.StartActivity("DeletePatientAsync");
        Monitoring.Log.Debug("Deleting Patient");
        
        if (request == null)
        {
            throw new NullReferenceException("PatientDelete is null");
        }
        
        Patient patient = _mapper.Map<Patient>(request);

        patient.Mail = "Delete@Me.com";
        patient.Name = "Delete Me";
        
        var validation = await _validator.ValidateAsync(patient);
        if (!validation.IsValid)
        {
            Monitoring.Log.Error(validation.ToString());
            throw new ValidationException(validation.ToString());
        }
        
        bool action = await _repo.DeletePatientAsync(patient);

        return action;
    }
}