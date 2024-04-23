using AutoMapper;
using Shared;
using Shared.Helpers.Mapper;

namespace MS_Tests;

public class MeasurementServiceTests
{
    // Service Creation Tests
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(null, new Mock<IMapper>().Object, new Mock<IValidator<Measurement>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService repository is null");
    }

    [Fact]
    public void CreateService_WithNullAutoMapper_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(new Mock<IMeasurementRepository>().Object, null, new Mock<IValidator<Patient>>().Object);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService mapper is null");
    }

    [Fact]
    public void CreateService_WithNullFluentValidation_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () =>
            new MeasurementService(new Mock<IMeasurementRepository>().Object, new Mock<IMapper>().Object, null);

        action.Should().Throw<NullReferenceException>().WithMessage("MeasurementService validator is null");
    }

    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        Action action = () => new MeasurementService(new Mock<IMeasurementRepository>().Object, new Mock<IMapper>().Object,
            new Mock<IValidator<Measurement>>().Object);

        action.Should().NotThrow();
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var measurementRepoMock = new Mock<IMeasurementRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
        var mapper = mapperConfig.CreateMapper();
        var validator = new MeasurementValidators();

        return new ServiceSetup(measurementRepoMock, mapper, validator);
    }

    private class ServiceSetup
    {
        private Mock<IMeasurementRepository> _measurementRepositoryMock;
        private IMapper _mapper;
        private IValidator<Measurement> _valdiator;

        public ServiceSetup(Mock<MeasurementRepository> measurementRepositoryMock, IMapper mapper, IValidator<Measurement> validator)
        {
            _measurementRepositoryMock = measurementRepositoryMock;
            _mapper = mapper;
            _valdiator = validator;
        }

        public Mock<IMeasurementRepository> GetMockRepo()
        {
            return _measurementRepositoryMock;
        }

        public MeasurementService CreateService()
        {
            return new MeasurementService(
                _measurementRepositoryMock.Object,
                _mapper,
                _valdiator
            );
        }
    }
}