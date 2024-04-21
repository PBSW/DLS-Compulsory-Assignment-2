using AutoMapper;
using FluentValidation;
using PS_Application.Interfaces;
using PS_Infrastructure.Interfaces;

namespace PS_Application;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    private readonly IMapper _mapper;
    private readonly IValidator _validator;
    
    public PatientService(IPatientRepository repo, IMapper mapper, IValidator validator)
    {
        _repo = repo ?? throw new NullReferenceException("PatientService repository is null");
        _mapper = mapper ?? throw new NullReferenceException("PatientService mapper is null");
        _validator = validator ?? throw new NullReferenceException("PatientService validator is null");
    }
}