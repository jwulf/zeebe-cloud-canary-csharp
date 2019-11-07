using System;

public struct ZeebeCanaryOptions
{
    public string ChirpUrl;
    public int HeartbeatPeriodSeconds;
    public string CanaryId;
    // ZBConfig;
    public Boolean Debug;
    public string SquawkUrl;
}
