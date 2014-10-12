using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
