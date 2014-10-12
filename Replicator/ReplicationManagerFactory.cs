using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplicatorService.ReplicationServiceManagers;

namespace Replicator
{
    public class ReplicationManagerFactory
    {
        public IReplicationManager CreateServer()
        {
            var serviceManager = new ReplicationServiceServerManager();
            return new ReplicationManager(serviceManager);
        }

        public IReplicationManager CreateClient()
        {
            var clientManager = new ReplicationServiceClientManager();
            return new ReplicationManager(clientManager);
        }
    }
}
