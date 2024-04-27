using FluentValidation;
using FeatureHubSDK;
using Microsoft.EntityFrameworkCore;
using PS_Application;
using PS_Application.Interfaces;
using PS_Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


var featurehub = builder.Configuration.GetSection("FeatureHub");
IFeatureHubConfig config = new EdgeFeatureHubConfig(featurehub.GetValue<string>("Host"), featurehub.GetValue<string>("ApiKey"));

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s + "\n");
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s + "\n");
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s + "\n");
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s + "\n");

builder.Services.Add(ServiceDescriptor.Singleton(typeof(IFeatureHubConfig), config));

config.Init();

//Database
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
));

// Dependency Resolvers
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientMeasurements, PatientMeasurements>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
