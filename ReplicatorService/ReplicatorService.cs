﻿using System;
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

        public ReplicatorService()
        {
            _addedReplicatorCallbackList = new List<IReplicatorServiceCallback>();
        }

        public void RegisterForUpdates()
        {
            var callback = OperationContext.Current.
                     GetCallbackChannel<IReplicatorServiceCallback>();
            if (callback != null)
            {
                this._addedReplicatorCallbackList.Add(callback);
            }
        }

        public void Unregister()
        {
            throw new NotImplementedException();
        }

        public void SendUpdates(ReplicatorDto replicatorDto)
        {
            _addedReplicatorCallbackList.ForEach(c=>c.UpdatesCallback(replicatorDto));
        }
    }
}
