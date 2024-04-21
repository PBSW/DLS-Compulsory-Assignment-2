using Microsoft.Extensions.DependencyInjection;
using PS_Application.Interfaces;

namespace PS_Application.DependencyInjection;

public class DependencyInjectionResolver
{
    public static void RegisterServiceLayer(IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddScoped<IPatientService, PatientService>();
    }
}