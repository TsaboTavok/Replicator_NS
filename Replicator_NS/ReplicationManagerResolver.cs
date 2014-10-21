using System;
using System.Linq;
using Replicator;

namespace Replicator_NS
{
    public class ReplicationManagerResolver
    {
        public IReplicationManager Resolve()
        {
            var replicationManagerFactory = new ReplicationManagerFactory();
            if (IsServer())
            {
                return replicationManagerFactory.CreateServer();
            }
            return replicationManagerFactory.CreateClient();
        }

        public bool IsServer()
        {
            if (Environment.GetCommandLineArgs().Contains("-client"))
            {
                return false;
            }
            return true;
        }
    }
}
