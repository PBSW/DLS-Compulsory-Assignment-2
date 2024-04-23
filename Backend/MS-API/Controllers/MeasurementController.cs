
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS_Application.Interfaces;
using Shared.Monitoring;

namespace MS_API.Controllers;

[ApiController]
public class MeasurementController
{
    private readonly IMeasurementService _service;
    
    public MeasurementController(IMeasurementService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("api/measurement")]
    public async Task<IActionResult> CreateMeasurementAsync([FromBody] MeasurementCreate request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("CreateMeasurementAsync");
        Monitoring.Log.Debug("Creating Measurement");
        
        try
        {
            return Ok(_service.CreateMeasurementAsync(request));
        } catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
}