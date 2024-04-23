using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using PS_Application;
using PS_Application.Interfaces;
using PS_Application.Validator;
using Shared;
using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;
using Shared.Helpers.Mapper;
using Xunit.Sdk;

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
            Mail = "testing@example.com",
            Ssn = "0123456789"
        };
        
        var patientCreate = new PatientCreate()
        {
            Name = "Test",
            Mail = "testing@example.com",
            Ssn = "0123456789"
        };
        
        setup.GetMockRepo().Setup(x => x.CreatePatientAsync(patient)).ReturnsAsync(patient);
        
        // Act
        var action = () => service.CreatePatientAsync(patientCreate).Result;
        
        // Assert
        action.Should().NotThrow();
    }
    
    [Fact]
    public async void CreatePatient_WithNullPatientCreate_ShouldThrowNullExceptionWithMessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();

        // Act
        Func<Task> action = () => service.CreatePatientAsync(null);
        
        // Assert
        await action.Should().ThrowAsync<NullReferenceException>().WithMessage("PatientCreate is null");
    }
    
    [Theory]
    [InlineData("", "Name is required")]
    [InlineData(" ", "Name is required")]
    [InlineData(null, "Name is null")]
    public async void CreatePatient_WithInvalidPatientName_ShouldThrowValidationExceptionWithMessage(string name, string errorMessage)
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
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }

    [Theory]
    [InlineData("", "Mail is required")]
    [InlineData(" ", "Mail is required")]
    [InlineData(null, "Mail is null")]
    [InlineData("awdawdlkdaw", "Incorrect Mail formatting")]
    [InlineData("thisshouldbeanextremelylongemailthatgoesoverthelimitofcharactersavailablefortheemailexamplesothisshouldbelongenoughforittothrownow@example.com", "Mail cannot be longer than 128 characters")]
    public async Task CreatePatient_WithInvalidPatientMail_ShouldThrowValidationExceptionWithMessage(string mail, string errorMessage)
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
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }
    
    [Theory]
    [InlineData(null, "SSN is null")]
    [InlineData("", "SSN is required")]
    [InlineData(" ", "SSN is required")]
    [InlineData("12345678901", "SSN may not be longer than 10 numbers")]
    [InlineData("awdawds", "SSN may only contain numbers")]
    public async void CreatePatient_WithInvalidPatientSsn_ShouldThrowValidationExceptionWithMessage(string ssn, string errorMessage)
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var patient = new PatientCreate()
        {
            Name = "Test",
            Mail = "testing@example.com",
            Ssn = ssn
        };
        
        // Act
        Func<Task> action = () => service.CreatePatientAsync(patient);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }
    
    // Patient GetAll Tests
    [Fact]
    public async void GetAllPatients_ShouldReturnValidPatientList()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        setup.GetMockRepo().Setup(x => x.GetAllPatientsAsync()).ReturnsAsync(new List<Patient>());
        
        // Act
        Func<Task> action = () => service.GetAllPatientsAsync();
        
        // Assert
        await action.Should().NotThrowAsync();
    }
    
    [Fact]
    public async void GetAllPatients_WithNullFromRepo_ShouldThrowNullReferenceExceptionWithmessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        setup.GetMockRepo().Setup(x => x.GetAllPatientsAsync()).ReturnsAsync((List<Patient>)null);
        
        // Act
        Func<Task> action = () => service.GetAllPatientsAsync();
        
        // Assert
        await action.Should().ThrowAsync<NullReferenceException>().WithMessage("Patient list from Repo is null");
    }

    // Patient Delete Tests
    [Fact]
    public async void DeletePatient_WithValidSsn_ShouldNotThrowException()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var patientDelete = new PatientDelete()
        {
            Ssn = "0123456789"
        };
        
        var patient = new Patient()
        {
            Name = "Test",
            Mail = "testing@example.com",
            Ssn = "0123456789"
        };
        
        setup.GetMockRepo().Setup(x => x.DeletePatientAsync(patient)).ReturnsAsync(true);
        
        // Act
        Func<Task> action = () => service.DeletePatientAsync(patientDelete);
        
        // Assert
        await action.Should().NotThrowAsync();
    }
    
    [Fact]
    public async void DeletePatient_WithNullSsn_ShouldThrowNullExceptionWithMessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        // Act
        Func<Task> action = () => service.DeletePatientAsync(null);
        
        // Assert
        await action.Should().ThrowAsync<NullReferenceException>().WithMessage("PatientDelete is null");
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