using AutoMapper;
using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace Shared.Helpers.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PatientCreate, Patient>();
        CreateMap<Patient, PatientResponse>();
        CreateMap<PatientDelete, Patient>();
    }
}