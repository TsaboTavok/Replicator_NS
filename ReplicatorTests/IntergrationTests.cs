using Microsoft.VisualStudio.TestTools.UnitTesting;
using Replicator;
using ReplicatorTests.Helpers;

namespace ReplicatorTests
{
    [TestClass]
    public class IntergrationTests
    {
        [TestMethod]
        public void TestPocoReplication()
        {
            var replicationManagerFactory = new ReplicationManagerFactory();
            var serverReplicationManager = replicationManagerFactory.CreateServer();
            var clientReplicationManager = replicationManagerFactory.CreateClient();

            var testObjectServer = new TestObject();
            var testObjectClient = new TestObject();
        }
    }
}
