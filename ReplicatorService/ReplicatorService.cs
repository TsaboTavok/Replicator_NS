using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReplicatorService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class ReplicatorService : IReplicatorService
    {
        private List<IReplicatorServiceCallback> _addedReplicatorCallbackList;

        public ReplicatorService(IReplicatorServiceCallback locaCallback)
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
    }
}
