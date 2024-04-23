using Microsoft.AspNetCore.Mvc;
using PS_Application.Interfaces;
using Shared.DTOs.Delete;
using Shared.DTOs.Requests;
using Shared.Monitoring;

namespace PS_API.Controller;

[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [Route("api/patient")]
    [HttpPost]
    public async Task<IActionResult> CreatePatientAsync(PatientCreate request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("CreatePatientAsync");
        Monitoring.Log.Debug("Creating Patient");

        try
        {
            return Ok(await _patientService.CreatePatientAsync(request));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }

    [Route("api/patient")]
    [HttpGet]
    public async Task<IActionResult> GetAllPatientsAsync()
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("GetAllPatientsAsync");
        Monitoring.Log.Debug("Getting all Patients");

        try
        {
            return Ok(await _patientService.GetAllPatientsAsync());
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }

    [Route("api/patient")]
    [HttpDelete]
    public async Task<IActionResult> DeletePatientAsync(PatientDelete request)
    {
        // Monitoring and Logging
        Monitoring.ActivitySource.StartActivity("DeletePatientAsync");
        Monitoring.Log.Debug("Deleting Patient");

        try
        {
            return Ok(await _patientService.DeletePatientAsync(request));
        }
        catch (Exception e)
        {
            Monitoring.Log.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
}