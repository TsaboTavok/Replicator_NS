using System;

namespace ReplicatorService.ReplicationServiceManagers
{
    public interface IReplicationServiceManager : IDisposable
    {
        void Init(IReplicatorServiceCallback replicatorServiceCallback);
        void SendUpdates(ReplicatorDto replicatorDto);
    }
}