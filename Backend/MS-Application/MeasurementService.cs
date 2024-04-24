using AutoMapper;
using FluentValidation;
using MS_Application.Interfaces;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

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
            throw new ArgumentNullException("MeasurementCreate request is null");
        }
        
        var measurement = _mapper.Map<Measurement>(request);
        
        var validationResult = _validator.Validate(measurement);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        Measurement returnMeasurement = await _repo.CreateMeasurementAsync(measurement);
        
        return _mapper.Map<MeasurementResponse>(returnMeasurement);
    }
}