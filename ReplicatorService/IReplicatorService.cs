using System.ServiceModel;
using ReplicatorService.Callback;

namespace ReplicatorService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IReplicatorServiceCallback))]
    public interface IReplicatorService
    {
        [OperationContract(IsOneWay = true)]
        void RegisterForUpdates();

        [OperationContract(IsOneWay = true)]
        void Unregister();

        [OperationContract(IsOneWay = true)]
        void SendUpdates(ReplicatorDto replicatorDto);
    }
}
