using AutoMapper;
using FluentValidation;
using PS_Application.Interfaces;
using Shared;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace PS_Application;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    private readonly IMapper _mapper;
    private readonly IValidator<Patient> _validator;
    
    public PatientService(IPatientRepository repo, IMapper mapper, IValidator<Patient> validator)
    {
        _repo = repo ?? throw new NullReferenceException("PatientService repository is null");
        _mapper = mapper ?? throw new NullReferenceException("PatientService mapper is null");
        _validator = validator ?? throw new NullReferenceException("PatientService validator is null");

    }

    public async Task<PatientResponse> CreatePatientAsync(PatientCreate request)
    {
        Patient patient = _mapper.Map<Patient>(request);
        
        await _repo.CreatePatientAsync(patient);

        PatientResponse response = _mapper.Map<PatientResponse>(patient);

        return response;
    }
}