using System;
using System.Threading.Tasks;
using Zeebe.Client.Api.Responses;

namespace CSharpCanary
{
    class Program
    {
        static void Main(string[] args)
        {

            var CanaryId = Environment.GetEnvironmentVariable("CANARY_ID") ?? "default";
            var ChirpUrl = Environment.GetEnvironmentVariable("CHIRP_URL");
            var Debug = Environment.GetEnvironmentVariable("DEBUG") == "true";
            var HeartbeatPeriodSeconds = Environment.GetEnvironmentVariable("CANARY_HEARTBEAT_SEC") ?? "300";
            var SquawkUrl = Environment.GetEnvironmentVariable("SQUAWK_URL");
            var CanaryOptions = new ZeebeCanaryOptions()
            {
                CanaryId = CanaryId,
                ChirpUrl = ChirpUrl,
                Debug = Debug,
                HeartbeatPeriodSeconds = Convert.ToInt16(HeartbeatPeriodSeconds),
                SquawkUrl = SquawkUrl
            };
            var Canary = new Canary(CanaryOptions);
        }
    }
}
