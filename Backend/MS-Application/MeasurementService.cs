using AutoMapper;
using FluentValidation;
using MS_Application.Interfaces;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;
using Shared.DTOs.Update;
using Shared.Monitoring;

namespace MS_Application;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _repo;
    private readonly IPatientCheck _patientCheck;
    private readonly IMapper _mapper;
    private readonly IValidator<Measurement> _validator;
    
    public MeasurementService(IMeasurementRepository repo, IPatientCheck patientCheck, IMapper mapper, IValidator<Measurement> validator)
    {
        _repo = repo ?? throw new NullReferenceException("MeasurementService repository is null");
        _patientCheck = patientCheck ?? throw new NullReferenceException("MeasurementService patientCheck is null");
        _mapper = mapper ?? throw new NullReferenceException("MeasurementService mapper is null");
        _validator = validator ?? throw new NullReferenceException("MeasurementService validator is null");
    }

    public async Task<MeasurementResponse> CreateMeasurementAsync(MeasurementCreate request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("CreateMeasurementAsync");
        Monitoring.Log.Debug("Creating Measurement");
        
        if (request == null)
        {
            Monitoring.Log.Error("MeasurementCreate is null");
            throw new NullReferenceException("MeasurementCreate is null");
        }
        
        Monitoring.Log.Debug($"Checking if Patient with SSN {request.PatientSSN} exists");
        var patientExists = await _patientCheck.CheckPatientExistsAsync(request.PatientSSN);
        if (!patientExists)
        {
            Monitoring.Log.Error($"Patient with SSN {request.PatientSSN} does not exist");
            throw new ArgumentException($"Patient with SSN {request.PatientSSN} does not exist");
        }
        
        var measurement = _mapper.Map<Measurement>(request);
        
        var validationResult = await _validator.ValidateAsync(measurement);
        if (!validationResult.IsValid)
        {
            Monitoring.Log.Error(validationResult.ToString());
            throw new ValidationException(validationResult.ToString());
        }
        
        measurement.date = DateTime.Now;
        
        Measurement returnMeasurement = await _repo.CreateMeasurementAsync(measurement);
        
        return _mapper.Map<MeasurementResponse>(returnMeasurement);
    }

    public async Task<List<MeasurementResponse>> GetAllMeasurementsAsync()
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetAllMeasurementsAsync");
        Monitoring.Log.Debug("Getting all Measurements");
        
        var measurements = await _repo.GetAllMeasurementsAsync();
        
        return _mapper.Map<List<MeasurementResponse>>(measurements);
    }

    public async Task<MeasurementResponse> UpdateMeasurementAsync(MeasurementUpdate request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("UpdateMeasurementAsync");
        Monitoring.Log.Debug("Updating Measurement");
        
        if (request == null)
        {
            Monitoring.Log.Error("MeasurementUpdate is null");
            throw new NullReferenceException("MeasurementUpdate is null");
        }
        
        var measurement = _mapper.Map<Measurement>(request);
        
        var validationResult = await _validator.ValidateAsync(measurement);
        if (!validationResult.IsValid)
        {
            Monitoring.Log.Error(validationResult.ToString());
            throw new ValidationException(validationResult.ToString());
        }
        
        Measurement returnMeasurement = await _repo.UpdateMeasurementAsync(measurement);
        
        return _mapper.Map<MeasurementResponse>(returnMeasurement);
    }

    public async Task<List<MeasurementResponse>> GetPatientMeasurementsAsync(string ssn)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetPatientMeasurementsAsync");
        Monitoring.Log.Debug("Getting Patient Measurements by SSN");
        
        var measurements = await _repo.GetPatientMeasurementsAsync(ssn);
        
        return _mapper.Map<List<MeasurementResponse>>(measurements);
    }
}