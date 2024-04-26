using PS_Application.Interfaces;
using RestSharp;
using Shared.DTOs.Response;
using Shared.Monitoring;

namespace PS_Infrastructure;

public class PatientMeasurements : IPatientMeasurements
{
    private readonly string BaseUrl = "http://measurement-service:8080";
    
    public async Task<List<MeasurementResponse>> GetMeasurementsAsync(string ssn)
    {
        // Monitoring and logging
        using var activity = Monitoring.ActivitySource.StartActivity("GetMeasurementsAsync");
        Monitoring.Log.Debug("Fetching measurements from MeasurementService");

        // Set up HTTP client
        var client = new RestClient(BaseUrl);
        var request = new RestRequest($"/api/measurement/{ssn}", Method.Get);
        request.AddHeader("Content-Type", "application/json");
        
        // Execute HTTP request and await the result
        var response = await client.ExecuteAsync<List<MeasurementResponse>>(request);

        // Check if the HTTP response was successful
        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data; // Directly return the deserialized MeasurementDTO object
        }

        // Log and throw an exception if the HTTP request failed or the user was not successfully fetched
        Monitoring.Log.Error($"Failed to fetch measurements for {ssn} from MeasurementService: {response.ErrorMessage}");
        throw new Exception("Unable to connect to MeasurementService: " + response.ErrorMessage);
    }
}