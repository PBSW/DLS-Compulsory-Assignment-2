using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using PS_Application;
using PS_Application.Interfaces;
using PS_Infrastructure.Interfaces;
using Shared;

namespace PS_Tests.UnitTests;

public class PatientServiceTests
{
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () => new PatientService(null, new Mock<IMapper>().Object, new Mock<IValidator<Patient>>().Object);
        
        action.Should().Throw<NullReferenceException>().WithMessage("PatientService repository is null");
    }
    
    [Fact]
    public void CreateService_WithNullAutoMapper_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () => new PatientService(new Mock<IPatientRepository>().Object, null, new Mock<IValidator>().Object);
        
        action.Should().Throw<NullReferenceException>().WithMessage("PatientService mapper is null");
    }
    
    [Fact]
    public void CreateService_WithNullFluentValidation_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () => new PatientService(new Mock<IPatientRepository>().Object, new Mock<IMapper>().Object, null);
        
        action.Should().Throw<NullReferenceException>().WithMessage("PatientService validator is null");
    }

    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        Action action = () => new PatientService(new Mock<IPatientRepository>().Object, new Mock<IMapper>().Object,
            new Mock<IValidator<Patient>>().Object);

        action.Should().NotThrow();
    }
}

public class ServiceSetup
{
    private Mock<IPatientRepository> _patientRepositoryMock;
    private IMapper _mapper;
    private IValidator _valdiator;

    public ServiceSetup(Mock<IPatientRepository> patientRepositoryMock, IMapper mapper, IValidator validator)
    {
        _patientRepositoryMock = patientRepositoryMock;
        _mapper = mapper;
        _valdiator = validator;
    }
    
    public Mock<IPatientRepository> GetMockRepo()
    {
        return _patientRepositoryMock;
    }

    public PatientService CreateService()
    {
        return new PatientService(
            _patientRepositoryMock.Object,
            _mapper,
            _valdiator
        );
    }
}