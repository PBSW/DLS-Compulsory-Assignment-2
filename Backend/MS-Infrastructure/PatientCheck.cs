using MS_Application.Interfaces;
using RestSharp;
using Shared.DTOs.Response;
using Shared.Monitoring;

namespace MS_Infrastructure;

public class PatientCheck : IPatientCheck
{
    private readonly string BaseUrl = "http://patient-service:8080";

    public async Task<bool> CheckPatientExistsAsync(string ssn)
    {
        // Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("GetMeasurementsAsync");
        Monitoring.Log.Debug("Fetching measurements from MeasurementService");

        // Set up HTTP client
        var client = new RestClient(BaseUrl);
        var request = new RestRequest($"/api/patient/check/{ssn}", Method.Get);
        request.AddHeader("Content-Type", "application/json");

        // Execute HTTP request and await the result
        var response = await client.ExecuteAsync<bool>(request);

        // Check if the HTTP response was successful
        if (response.IsSuccessful)
        {
            return response.Data; // Directly return the deserialized MeasurementDTO object
        }

        // Log and throw an exception if the HTTP request failed or the user was not successfully fetched
        Monitoring.Log.Error(
            $"Failed to fetch measurements for {ssn} from MeasurementService: {response.ErrorMessage}");
        throw new Exception("Unable to connect to MeasurementService: " + response.ErrorMessage);
    }
}