using AutoMapper;
using PS_Application.Interfaces;

namespace PS_Application;

public class PatientService : IPatientService
{
    private IMapper _mapper;
    
    public PatientService(IMapper mapper)
    {
        _mapper = mapper ?? throw new NullReferenceException("PatientService mapper is null");
    }
}