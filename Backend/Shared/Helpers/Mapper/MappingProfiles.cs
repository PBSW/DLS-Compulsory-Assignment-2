using AutoMapper;
using Shared.DTOs.Create;
using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace Shared.Helpers.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Patient Mapping
        CreateMap<PatientCreate, Patient>();
        CreateMap<Patient, PatientResponse>();
        CreateMap<PatientDelete, Patient>();
        
        // Measurement Mapping
        CreateMap<MeasurementCreate, Measurement>();
        CreateMap<Measurement, MeasurementResponse>();
    }
}