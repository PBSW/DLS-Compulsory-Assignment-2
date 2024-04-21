using FluentAssertions;
using Moq;

namespace PS_Tests.UnitTests;

public class PatientServiceTests
{
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        Action action = () => new PatientService(null);
        
        action.Should().Throw<NullReferenceException>().WithMessage("PatientService repository is null");
    }
    
    [Fact]
    public void CreateService_WithNullAutoMapper_ShouldThrowNullExceptionWithMessage()
    {
        
        Action action = () => new PatientService(new Mock<IPatientRepository>().Object, null);
        
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