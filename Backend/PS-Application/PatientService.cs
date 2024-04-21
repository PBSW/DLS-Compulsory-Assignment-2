using AutoMapper;
using PS_Application.Interfaces;
using Shared;

namespace PS_Application;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    private readonly IMapper _mapper;
    
    public PatientService(IPatientRepository repo, IMapper mapper)
    {
        _repo = repo ?? throw new NullReferenceException("PatientService repo is null");
        _mapper = mapper ?? throw new NullReferenceException("PatientService mapper is null");
    }
}