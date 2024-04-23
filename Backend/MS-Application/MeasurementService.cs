using System.Diagnostics.Metrics;
using AutoMapper;
using FluentValidation;
using MS_Application.Interfaces;
using Shared;

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
}