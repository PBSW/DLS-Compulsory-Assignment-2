using Microsoft.AspNetCore.Mvc;
using MS_Application.Interfaces;
using Shared.DTOs.Create;
using Shared.Monitoring;

namespace MS_API.Controllers;

[ApiController]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementService _service;
    
    public MeasurementController(IMeasurementService service)
    {
        _service = service ?? throw new NullReferenceException("MeasurementController service is null");
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
            return Ok(await _service.CreateMeasurementAsync(request));
        } catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/measurement")]
    public async Task<IActionResult> GetAllMeasurementsAsync()
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetAllMeasurementsAsync");
        Monitoring.Log.Debug("Getting all Measurements");
        
        try
        {
            return Ok(await _service.GetAllMeasurementsAsync());
        } catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("api/measurement/{id}")]
    public async Task<IActionResult> GetMeasurementBySSNAsync([FromQuery] string ssn)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetMeasurementByIdAsync");
        Monitoring.Log.Debug("Getting Measurement by ID");
        
        try
        {
            return Ok(await _service.GetMeasurementBySSNAsync(ssn));
        } catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
}