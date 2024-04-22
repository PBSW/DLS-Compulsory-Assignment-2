using Microsoft.AspNetCore.Mvc;
using PS_Application.Interfaces;
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
}