﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplicatorService.ReplicationServiceManagers;

namespace Replicator
{
    public class ReplicatorManagerFactory
    {
        public IReplicationManager CreateClient()
        {
            var serviceManager = new ReplicationServiceServerManager();
            var replicationManager = new ReplicationManager(serviceManager);
            return replicationManager;

        }
        public IReplicationManager CreateServer()
        {
            var serviceManager = new ReplicationServiceClientManager();
            var replicationManager = new ReplicationManager(serviceManager);
            return replicationManager;
        }
    }
}