namespace ReplicatorService
{
    public interface IReplicatorServerService
    {
        void SendUpdates(ReplicatorDto replicatorDto);

        void NotifyServerShutdown(); 
    }
}