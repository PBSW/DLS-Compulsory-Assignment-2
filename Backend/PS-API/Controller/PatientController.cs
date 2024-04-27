using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using PS_Application.Interfaces;
using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.Monitoring;

namespace PS_API.Controller;

[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [Route("api/patient")]
    public async Task<IActionResult> CreatePatientAsync([FromBody] PatientCreate request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("CreatePatientAsync");
        Monitoring.Log.Debug("Creating Patient");

        try
        {
            return Ok(await _service.CreatePatientAsync(request));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }

    
    [HttpGet]
    [Route("api/patient")]
    public async Task<IActionResult> GetAllPatientsAsync()
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetAllPatientsAsync");
        Monitoring.Log.Debug("Getting all Patients");

        try
        {
            return Ok(await _service.GetAllPatientsAsync());
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/patient/{ssn}")]
    public async Task<IActionResult> GetPatientBySSNAsync(string ssn)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetPatientBySSNAsync");
        Monitoring.Log.Debug("Getting Patient by SSN");

        try
        {
            return Ok(await _service.GetPatientBySSNAsync(ssn));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/patient/check/{ssn}")]
    public async Task<IActionResult> IsPatientAsync(string ssn)
    {
        // Monitoring and Logging
        Monitoring.Log.Debug("Checking if Patient exists");

        var headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
        var propagator = new TraceContextPropagator();
        var parrentContext = propagator.Extract(default, headers, (carrier, key) =>
        {
            return new List<string>(new[] { headers.ContainsKey(key) ? headers[key].ToString() : String.Empty });
        });

        Baggage.Current = parrentContext.Baggage;
        using var activity = Monitoring.ActivitySource.StartActivity("PatientService.API.IsPatientAsynch got message", ActivityKind.Consumer, parrentContext.ActivityContext);

        
        try
        {
            return Ok(await _service.IsPatientAsync(ssn));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }

    }
    
    [HttpDelete]
    [Route("api/patient")]
    public async Task<IActionResult> DeletePatientAsync([FromBody] PatientDelete request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("DeletePatientAsync");
        Monitoring.Log.Debug("Deleting Patient");

        try
        {
            return Ok(await _service.DeletePatientAsync(request));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
}