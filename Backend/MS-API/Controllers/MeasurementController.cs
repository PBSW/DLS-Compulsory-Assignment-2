using Microsoft.AspNetCore.Mvc;
using MS_Application.Interfaces;
using Shared.DTOs.Create;
using Shared.DTOs.Update;
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
    [Route("api/measurement/{ssn}")]
    public async Task<IActionResult> GetPatientMeasurementsBySSNAsync([FromRoute] string ssn)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetMeasurementByIdAsync");
        Monitoring.Log.Debug("Getting Measurement by SSN");
        
        try
        {
            return Ok(await _service.GetPatientMeasurementsAsync(ssn));
        } catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    [Route("api/measurement")]
    public async Task<IActionResult> UpdateMeasurementAsync([FromBody] MeasurementUpdate request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("UpdateMeasurementAsync");
        Monitoring.Log.Debug("Updating Measurement");
        
        try
        {
            return Ok(await _service.UpdateMeasurementAsync(request));
        } catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
}