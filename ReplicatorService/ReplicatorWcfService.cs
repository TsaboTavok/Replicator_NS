using System.Collections.Generic;
using System.ServiceModel;
using ReplicatorService.Callback;

namespace ReplicatorService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    internal class ReplicatorWcfService : IReplicatorService, IReplicatorServerService
    {
        private readonly List<IReplicatorServiceCallback> _addedReplicatorCallbackList;

        public ReplicatorWcfService(IReplicatorServiceCallback locaCallback)
        {
            _addedReplicatorCallbackList = new List<IReplicatorServiceCallback>();
            _addedReplicatorCallbackList.Add(locaCallback);
        }

        public void RegisterForUpdates()
        {
            var callback = OperationContext.Current.
                     GetCallbackChannel<IReplicatorServiceCallback>();
            if (callback != null)
            {
                _addedReplicatorCallbackList.Add(callback);
            }
        }

        public void Unregister()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IReplicatorServiceCallback>();
            _addedReplicatorCallbackList.Remove(callback);
        }

        public void SendUpdates(ReplicatorDto replicatorDto)
        {
            _addedReplicatorCallbackList.ForEach(c=>c.UpdatesCallback(replicatorDto));
        }

        public void NotifyServerShutdown()
        {
            _addedReplicatorCallbackList.ForEach(c => c.ServerShutdownCallback());
        }
    }
}
