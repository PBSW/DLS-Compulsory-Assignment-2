using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using PS_Application;
using PS_Application.Interfaces;
using Shared;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

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
        
        setup.GetMockRepo().Setup(x => x.CreatePatientAsync(patient)).ReturnsAsync(patient);
        
        // Act
        var action = () => service.CreatePatientAsync(patientCreate).Result;
        
        // Assert
        action.Should().NotThrow();
    }
    
    [Fact]
    public void CreatePatient_WithNullPatient_ShouldThrowValidationExceptionWithMessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();

        // Act
        Action action = () => service.CreatePatientAsync(null);
        
        // Assert
        action.Should().Throw<ValidationException>().WithMessage("Patient cannot be null");
    }
    
    [Fact]
    public void CreatePatient_WithInvalidPatient_ShouldThrowValidationExceptionWithMessage()
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
        
        setup.GetMockRepo().Setup(x => x.CreatePatientAsync(patient)).ReturnsAsync(patient);
        
        // Act
        Action action = () => service.CreatePatientAsync(patientCreate);
        
        // Assert
        action.Should().Throw<ValidationException>().WithMessage("Patient is invalid");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreatePatient_WithInvalidPatientName_ShouldThrowValidationExceptionWithMessage(string name)
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
        Action action = () => service.CreatePatientAsync(patient);
        
        // Assert
        action.Should().Throw<ValidationException>().WithMessage("Patient name is invalid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreatePatient_WithInvalidPatientMail_ShouldThrowValidationExceptionWithMessage(string mail)
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
        Action action = () => service.CreatePatientAsync(patient);

        // Assert
        action.Should().Throw<ValidationException>().WithMessage("Patient mail is invalid");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreatePatient_WithInvalidPatientSsn_ShouldThrowValidationExceptionWithMessage(string ssn)
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
        Action action = () => service.CreatePatientAsync(patient);
        
        // Assert
        action.Should().Throw<ValidationException>().WithMessage("Patient ssn is invalid");
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var patientRepoMock = new Mock<IPatientRepository>();
        var mapperMock = new Mock<IMapper>();
        var validatorMock = new Mock<IValidator<Patient>>();

        return new ServiceSetup(patientRepoMock, mapperMock.Object, validatorMock.Object);
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