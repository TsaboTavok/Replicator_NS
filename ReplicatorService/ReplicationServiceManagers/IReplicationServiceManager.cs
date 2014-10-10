namespace ReplicatorService.ReplicationServiceManagers
{
    public interface IReplicationServiceManager
    {
        void Init(IReplicatorServiceCallback replicatorServiceCallback);
        void SendUpdates(ReplicatorDto replicatorDto);
    }
}