
using System;
using System.Threading;
using Zeebe.Client;
using Zeebe.Client.Api.Worker;

public class Canary
{
    private string SquawkUrl;
    private string ChirpUrl;
    private int HeartbeatPeriodSeconds;
    private string CanaryId;
    // private zbc;
    // private squawkTimer: NodeJS.Timeout;
    private Boolean Debug;
    private IZeebeClient zeebeClient;
    private IJobWorker worker;

    public Canary(ZeebeCanaryOptions options)
    {
        this.CanaryId = options.CanaryId;
        this.ChirpUrl = options.ChirpUrl;
        this.Debug = options.Debug;
        this.HeartbeatPeriodSeconds = options.HeartbeatPeriodSeconds;
        this.SquawkUrl = options.SquawkUrl;

        this.zeebeClient = Zeebe.Client.ZeebeClient
                .Builder()
                .UseGatewayAddress("localhost:26500")
                .UsePlainText()
                .Build();
        using (var signal = new EventWaitHandle(false, EventResetMode.AutoReset))
        {
            this.startWorker();
            signal.WaitOne();
        }
    }

    private IJobWorker startWorker()
    {
        return this.zeebeClient.NewWorker()
            .JobType("chirp-" + this.CanaryId)
            .Handler((client, job) =>
            {
                // business logic
                client.NewCompleteJobCommand(job.Key).Send();
            })
            .MaxJobsActive(1)
            .Name(this.CanaryId)
            .PollInterval(TimeSpan.FromSeconds(30))
            .Timeout(TimeSpan.FromSeconds(10))
            .Open();
    }
}