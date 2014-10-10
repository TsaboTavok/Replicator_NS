using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReplicatorService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ReplicatorService : IReplicatorService
    {
        public void RegisterForUpdates()
        {
            throw new NotImplementedException();
        }

        public void Unregister()
        {
            throw new NotImplementedException();
        }

        public void SendUpdates(ReplicatorDto replicatorDto)
        {
            throw new NotImplementedException();
        }
    }
}
