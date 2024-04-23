using MS_Application.Interfaces;

namespace MS_Infrastructure;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly DatabaseContext _dbcontext;
    
    public MeasurementRepository(DatabaseContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    
}