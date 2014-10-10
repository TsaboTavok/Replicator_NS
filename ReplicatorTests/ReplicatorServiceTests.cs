using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReplicatorService;
using ReplicatorService.ReplicationServiceManagers;

namespace ReplicatorTests
{
    [TestClass]
    public class ReplicatorServiceTests
    {
        [TestMethod]
        public void Test_Init_Server()
        {
            var serviceManagerServer = new ReplicationServiceManager();
            var replicatorServiceServerCallback = new Mock<IReplicatorServiceCallback>();
            serviceManagerServer.Init(replicatorServiceServerCallback.Object);
            serviceManagerServer.HostServer();
        }

        [TestMethod]
        [ExpectedException(typeof(CommunicationException))]
        public void Test_Init_Client()
        {
            var serviceManagerClient = new ReplicationServiceManager();
            var replicatorServiceClientCallback = new Mock<IReplicatorServiceCallback>();
            serviceManagerClient.Init(replicatorServiceClientCallback.Object);
            serviceManagerClient.CreateClient();
        }

        [TestMethod]
        public void Test_Service_Simple_Communications()
        {
            var serviceManagerServer = new ReplicationServiceManager();
            var replicatorServiceServerCallback = new Mock<IReplicatorServiceCallback>();
            serviceManagerServer.Init(replicatorServiceServerCallback.Object);
            serviceManagerServer.HostServer();
                
            var serviceManagerClient = new ReplicationServiceManager();
            var replicatorServiceClientCallback = new Mock<IReplicatorServiceCallback>();
            serviceManagerClient.Init(replicatorServiceClientCallback.Object);
            serviceManagerClient.CreateClient();

            serviceManagerClient.SendUpdates(new ReplicatorDto());

            replicatorServiceServerCallback.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            replicatorServiceClientCallback.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
        }
    }
}
