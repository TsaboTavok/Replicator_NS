using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReplicatorService;
using ReplicatorService.ReplicationServiceManagers;
using ReplicatorTests.Helpers;

namespace ReplicatorTests
{
    [TestClass]
    public class ReplicatorServiceTests
    {
        [TestMethod]
        public void Test_Init_Server()
        {
            var server = new ReplicationServiceServerHelper();
        }

        [TestMethod]
        [ExpectedException(typeof(EndpointNotFoundException))]
        public void Test_Init_Client_Without_Host()
        {
            var client = new ReplicationServiceClientHelper();
        }

        [TestMethod]
        public void Test_Service_Simple_Communications_From_Sever()
        {
            var server = new ReplicationServiceServerHelper();
            var client1 = new ReplicationServiceClientHelper();
            var client2 = new ReplicationServiceClientHelper();

            server.Manager.SendUpdates(new ReplicatorDto());

            client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
        }


        [TestMethod]
        public void Test_Service_Simple_Communications_From_Client()
        {
            var server =  new ReplicationServiceServerHelper();
            var client1 = new ReplicationServiceClientHelper();
            var client2 = new ReplicationServiceClientHelper();

            client2.Manager.SendUpdates(new ReplicatorDto());

            client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
        }
    }
}
