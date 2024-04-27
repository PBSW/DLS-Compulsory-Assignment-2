using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using RestSharp;

namespace Shared.Monitoring;

public static class PropagationHelper
{
    public static void Inject(RestRequest request, Activity activity)
    {
        // Propagate the trace context
        var activityContext = activity?.Context ?? Activity.Current?.Context ?? default;
        var propagationContext = new PropagationContext(activityContext, Baggage.Current);
        var propagator = new TraceContextPropagator();
            
        propagator.Inject(propagationContext, request, (carrier, key, value) => carrier.AddHeader(key, value));
    }
}