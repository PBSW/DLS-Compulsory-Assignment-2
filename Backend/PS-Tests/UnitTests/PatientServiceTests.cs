using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using PS_Application;
using PS_Application.Interfaces;
using PS_Application.Validator;
using Shared;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;
using Shared.Helpers.Mapper;

namespace PS_Tests.UnitTests;

public class PatientServiceTests
{
    // Service Creation Tests
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new PatientService(null, new Mock<IMapper>().Object, new Mock<IValidator<Patient>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("PatientService repository is null");
    }

    [Fact]
    public void CreateService_WithNullAutoMapper_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new PatientService(new Mock<IPatientRepository>().Object, null, new Mock<IValidator<Patient>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("PatientService mapper is null");
    }

    [Fact]
    public void CreateService_WithNullFluentValidation_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new PatientService(new Mock<IPatientRepository>().Object, new Mock<IMapper>().Object, null);

        action.Should().Throw<NullReferenceException>().WithMessage("PatientService validator is null");
    }

    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        Action action = () => new PatientService(new Mock<IPatientRepository>().Object, new Mock<IMapper>().Object,
            new Mock<IValidator<Patient>>().Object);

        action.Should().NotThrow();
    }

    // Patient Create Tests
    [Fact]
    public void CreatePatient_WithValidPatient_ShouldReturnValidPatientObject()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var patient = new Patient()
        {
            Name = "Test",
            Mail = "Test@Mail.com",
            Ssn = "123456789"
        };
        
        var patientCreate = new PatientCreate()
        {
            Name = "Test",
            Mail = "Test@Mail.com",
            Ssn = "123456789"
        };
        
        setup.GetMockRepo().Setup(x => x.CreatePatientAsync(patient)).ReturnsAsync(1);
        
        // Act
        var action = () => service.CreatePatientAsync(patientCreate).Result;
        
        // Assert
        action.Should().NotThrow();
    }
    
    [Fact]
    public async void CreatePatient_WithNullPatient_ShouldThrowValidationExceptionWithMessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();

        // Act
        Func<Task> action = () => service.CreatePatientAsync(null);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage("Patient cannot be null");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async void CreatePatient_WithInvalidPatientName_ShouldThrowValidationExceptionWithMessage(string name)
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var patient = new PatientCreate()
        {
            Name = name,
            Mail = "Test@Mail.com",
            Ssn = "123456789"
        };
        
        // Act
        Func<Task> action = () => service.CreatePatientAsync(patient);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage("Patient name is invalid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task CreatePatient_WithInvalidPatientMail_ShouldThrowValidationExceptionWithMessage(string mail)
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var patient = new PatientCreate()
        {
            Name = "Test",
            Mail = mail,
            Ssn = "123456789"
        };
        
        // Act
        Func<Task> action = () => service.CreatePatientAsync(patient);

        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage("Patient mail is invalid");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async void CreatePatient_WithInvalidPatientSsn_ShouldThrowValidationExceptionWithMessage(string ssn)
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var patient = new PatientCreate()
        {
            Name = "Test",
            Mail = "Test@Mail.com",
            Ssn = ssn
        };
        
        // Act
        Func<Task> action = () => service.CreatePatientAsync(patient);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage("Patient SS is invalid");
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var patientRepoMock = new Mock<IPatientRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
        var mapper = mapperConfig.CreateMapper();
        var validator = new PatientValidators();

        return new ServiceSetup(patientRepoMock, mapper, validator);
    }

    private class ServiceSetup
    {
        private Mock<IPatientRepository> _patientRepositoryMock;
        private IMapper _mapper;
        private IValidator<Patient> _valdiator;

        public ServiceSetup(Mock<IPatientRepository> patientRepositoryMock, IMapper mapper, IValidator<Patient> validator)
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
}