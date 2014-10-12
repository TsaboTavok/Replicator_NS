using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReplicatorService;
using ReplicatorTests.Helpers;

namespace ReplicatorTests
{
    [TestClass]
    public class ReplicatorServiceTests
    {
        [TestMethod]
        public void Test_Init_Server()
        {
            var server = new ReplicationServiceTestServerHelper();
            server.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(EndpointNotFoundException))]
        public void Test_Init_Client_Without_Host()
        {
            var client = new ReplicationServiceTestClientHelper();
            client.Dispose();
        }

        [TestMethod]
        public void Test_Service_Simple_Communications_From_Sever()
        {
            using(var server =  new ReplicationServiceTestServerHelper())
            using(var client1 = new ReplicationServiceTestClientHelper())
            using (var client2 = new ReplicationServiceTestClientHelper())
            {
                server.Manager.SendUpdates(new ReplicatorDto());

                client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
                client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
                server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
            }
        }


        [TestMethod]
        public void Test_Service_Simple_Communications_From_Client()
        {
            using(var server =  new ReplicationServiceTestServerHelper())
            using(var client1 = new ReplicationServiceTestClientHelper())
            using (var client2 = new ReplicationServiceTestClientHelper())
            {
                client2.Manager.SendUpdates(new ReplicatorDto());

                client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
                server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
                client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
            }
        }

        [TestMethod]
        public void Test_Client_Disconnects()
        {
            using (var server = new ReplicationServiceTestServerHelper())
            using (var client1 = new ReplicationServiceTestClientHelper())
            using (var client2 = new ReplicationServiceTestClientHelper())
            {
                var client3 = new ReplicationServiceTestClientHelper();
                client3.Manager.Dispose();

                client2.Manager.SendUpdates(new ReplicatorDto());

                client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
                server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
                client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
                client3.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);
            }
        }

        [TestMethod]
        public void Test_Host_Disconnects_First()
        {
            var server = new ReplicationServiceTestServerHelper();
            var client1 = new ReplicationServiceTestClientHelper();
            var client2 = new ReplicationServiceTestClientHelper();
            
            client2.Manager.SendUpdates(new ReplicatorDto());

            client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Never);

            server.Dispose();
            client1.Dispose();
            client2.Dispose();
        }
    }
}
