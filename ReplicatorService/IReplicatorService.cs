using System.ServiceModel;
using ReplicatorService.Callback;

namespace ReplicatorService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IReplicatorServiceCallback))]
    public interface IReplicatorService
    {
        [OperationContract]
        void RegisterForUpdates();

        [OperationContract(IsOneWay = true)]
        void Unregister();

        [OperationContract]
        void SendUpdates(ReplicatorDto replicatorDto);
    }
}
