using PS_Infrastructure.Interfaces;

namespace PS_Infrastructure;

public class PatientRepository : IPatientRepository
{
    private readonly DatabaseContext _dbcontext;
    
    public PatientRepository(DatabaseContext dbcontext)
    {
        _dbcontext = dbcontext ?? throw new NullReferenceException("PatientRepository database context is null");
    }
    
    
}