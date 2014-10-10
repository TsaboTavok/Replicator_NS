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
            var server = new ReplicationServiceTestServerHelper();
        }

        [TestMethod]
        [ExpectedException(typeof(EndpointNotFoundException))]
        public void Test_Init_Client_Without_Host()
        {
            var client = new ReplicationServiceTestClientHelper();
        }

        [TestMethod]
        public void Test_Service_Simple_Communications_From_Sever()
        {
            var server = new ReplicationServiceTestServerHelper();
            var client1 = new ReplicationServiceTestClientHelper();
            var client2 = new ReplicationServiceTestClientHelper();

            server.Manager.SendUpdates(new ReplicatorDto());

            client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            //Пока отпраляем всем, делаем тест зеленым.
            server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
        }


        [TestMethod]
        public void Test_Service_Simple_Communications_From_Client()
        {
            var server =  new ReplicationServiceTestServerHelper();
            var client1 = new ReplicationServiceTestClientHelper();
            var client2 = new ReplicationServiceTestClientHelper();

            client2.Manager.SendUpdates(new ReplicatorDto());

            client1.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            server.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
            //Пока отпраляем всем, делаем тест зеленым.
            client2.CallbackMock.Verify(r => r.UpdatesCallback(It.IsAny<ReplicatorDto>()), Times.Once);
        }
    }
}
