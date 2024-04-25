using AutoMapper;
using FluentValidation;
using MS_Application.Interfaces;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;
using Shared.Monitoring;

namespace MS_Application;

public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _repo;
    private readonly IMapper _mapper;
    private readonly IValidator<Measurement> _validator;
    
    public MeasurementService(IMeasurementRepository repo, IMapper mapper, IValidator<Measurement> validator)
    {
        _repo = repo ?? throw new NullReferenceException("MeasurementService repository is null");
        _mapper = mapper ?? throw new NullReferenceException("MeasurementService mapper is null");
        _validator = validator ?? throw new NullReferenceException("MeasurementService validator is null");
    }

    public async Task<MeasurementResponse> CreateMeasurementAsync(MeasurementCreate request)
    {
        if (request == null)
        {
            throw new NullReferenceException("MeasurementCreate is null");
        }
        
        var measurement = _mapper.Map<Measurement>(request);
        
        var validationResult = await _validator.ValidateAsync(measurement);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }
        
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

    public async Task<List<MeasurementResponse>> GetPatientMeasurementsAsync(string ssn)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetPatientMeasurementsAsync");
        Monitoring.Log.Debug("Getting Patient Measurements by SSN");
        
        var measurements = await _repo.GetPatientMeasurementsAsync(ssn);
        
        return _mapper.Map<List<MeasurementResponse>>(measurements);
    }
}