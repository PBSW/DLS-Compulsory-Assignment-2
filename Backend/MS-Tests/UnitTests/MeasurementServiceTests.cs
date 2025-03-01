using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using MS_Application;
using MS_Application.Helpers;
using MS_Application.Interfaces;
using Shared;
using Shared.DTOs.Create;
using Shared.Helpers.Mapper;

namespace MS_Tests.UnitTests;

public class MeasurementServiceTests
{
    // Service Creation Tests
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(null, new Mock<IPatientCheck>().Object, new Mock<IMapper>().Object, new Mock<IValidator<Measurement>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService repository is null");
    }

    [Fact]
    public void CreateService_WithNullPatientCheck_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(new Mock<IMeasurementRepository>().Object, null, new Mock<IMapper>().Object, new Mock<IValidator<Measurement>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService patientcheck is null");
    }
    
    [Fact]
    public void CreateService_WithNullAutoMapper_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(new Mock<IMeasurementRepository>().Object, new Mock<IPatientCheck>().Object, null, new Mock<IValidator<Measurement>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService mapper is null");
    }

    [Fact]
    public void CreateService_WithNullFluentValidation_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(new Mock<IMeasurementRepository>().Object, new Mock<IPatientCheck>().Object, new Mock<IMapper>().Object, null);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService validator is null");
    }

    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        Action action = () => new MeasurementService(new Mock<IMeasurementRepository>().Object, new Mock<IPatientCheck>().Object, new Mock<IMapper>().Object,
            new Mock<IValidator<Measurement>>().Object);

        action.Should().NotThrow();
    }
    
    // Patient Create Tests
    [Fact]
    public async void CreateMeasurement_WithValidMeasurement_ShouldReturnValidMeasurementObject()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var measurement = new Measurement()
        {
            Id = 1,
            PatientSSN = "0123456789",
            Systolic = 120,
            Diastolic = 80,
            Seen = false
        };
        
        var measurementCreate = new MeasurementCreate()
        {
            PatientSSN = "0123456789",
            Systolic = 120,
            Diastolic = 80,
        };
        
        setup.GetMockRepo().Setup(x => x.CreateMeasurementAsync(measurement)).ReturnsAsync(measurement);
        
        // Act
        Func<Task> action = () => service.CreateMeasurementAsync(measurementCreate);
        
        setup.GetMockPatientCheck().Setup(x => x.CheckPatientExistsAsync("0123456789")).ReturnsAsync(true);
        // Assert
        await action.Should().NotThrowAsync();
    }
    
    [Fact]
    public async void CreateMeasurement_WithNullMeasurementCreate_ShouldThrowNullExceptionWithMessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        // Act
        Func<Task> action = () => service.CreateMeasurementAsync(null);
        
        // Assert
        await action.Should().ThrowAsync<NullReferenceException>().WithMessage("MeasurementCreate is null");
    }
    
    [Theory]
    [InlineData("", "SSN is required")]
    [InlineData(" ", "SSN is required")]
    [InlineData(null, "SSN is null")]
    [InlineData("awdadssad","SSN may only contain numbers")]
    [InlineData("12345678901", "SSN may not be longer than 10 numbers")]
    public async void CreateMeasurement_WithInvalidPatientSSN_ShouldThrowValidationExceptionWithMessage(string ssn, string errorMessage)
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var measurement = new MeasurementCreate()
        {
            PatientSSN = ssn,
            Systolic = 120,
            Diastolic = 80,
        };

        setup.GetMockPatientCheck().Setup(x => x.CheckPatientExistsAsync(ssn)).ReturnsAsync(true);
        
        // Act
        Func<Task> action = () => service.CreateMeasurementAsync(measurement);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }
    
    [Fact]
    public async void CreateMeasurements_WithNonexistentPatient_ShouldThrowArugmentExceptionWithMessage()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        var measurement = new MeasurementCreate()
        {
            PatientSSN = "123456789",
            Systolic = 120,
            Diastolic = 80,
        };
        
        setup.GetMockRepo().Setup(x => x.GetPatientMeasurementsAsync(measurement.PatientSSN)).ReturnsAsync(new List<Measurement>());
        setup.GetMockPatientCheck().Setup(x => x.CheckPatientExistsAsync(measurement.PatientSSN)).ReturnsAsync(false);
            
        // Act
        Func<Task> action = () => service.CreateMeasurementAsync(measurement);
        
        // Assert
        await action.Should().ThrowAsync<ArgumentException>().WithMessage($"Patient with SSN {measurement.PatientSSN} does not exist");
    }

    
    // Meausrement GetAll Tests
    [Fact]
    public async void GetAllMeasurements_ShouldReturnValidMeasurementList()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        setup.GetMockRepo().Setup(x => x.GetAllMeasurementsAsync()).ReturnsAsync(new List<Measurement>());
        
        // Act
        Func<Task> action = () => service.GetAllMeasurementsAsync();
        
        // Assert
        await action.Should().NotThrowAsync();
    }
    
    // Measurement GetBySSN Tests
    [Fact]
    public async void GetPatientMeasurementsBySSN_ShouldReturnValidMeasurementList()
    {
        // Setup
        var setup = CreateServiceSetup();
        var service = setup.CreateService();
        
        string ssn = "0123456789";
        
        
        setup.GetMockRepo().Setup(x => x.GetPatientMeasurementsAsync(ssn)).ReturnsAsync(new List<Measurement>());
        setup.GetMockPatientCheck().Setup(x => x.CheckPatientExistsAsync(ssn)).ReturnsAsync(true);
            
        // Act
        Func<Task> action = () => service.GetPatientMeasurementsAsync("0123456789");
        
        // Assert
        await action.Should().NotThrowAsync();
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var measurementRepoMock = new Mock<IMeasurementRepository>();
        var patientCheck = new Mock<IPatientCheck>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
        var mapper = mapperConfig.CreateMapper();
        var validator = new MeasurementValidators();

        return new ServiceSetup(measurementRepoMock, patientCheck, mapper, validator);
    }

    private class ServiceSetup
    {
        private Mock<IMeasurementRepository> _measurementRepositoryMock;
        private Mock<IPatientCheck> _patientCheck;
        private IMapper _mapper;
        private IValidator<Measurement> _valdiator;

        public ServiceSetup(Mock<IMeasurementRepository> measurementRepositoryMock, Mock<IPatientCheck> patientCheck, IMapper mapper, IValidator<Measurement> validator)
        {
            _measurementRepositoryMock = measurementRepositoryMock;
            _patientCheck = patientCheck;
            _mapper = mapper;
            _valdiator = validator;
        }

        public Mock<IMeasurementRepository> GetMockRepo()
        {
            return _measurementRepositoryMock;
        }

        public Mock<IPatientCheck> GetMockPatientCheck()
        {
            return _patientCheck;
        }
        
        public MeasurementService CreateService()
        {
            return new MeasurementService(
                _measurementRepositoryMock.Object,
                _patientCheck.Object,
                _mapper,
                _valdiator
            );
        }
    }
}