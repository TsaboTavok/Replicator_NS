using System.ServiceModel;

namespace ReplicatorService.Callback
{
    public interface IReplicatorServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdatesCallback(ReplicatorDto replicatorDto);

        [OperationContract(IsOneWay = true)]
        void ServerShutdownCallback();
    }
}
