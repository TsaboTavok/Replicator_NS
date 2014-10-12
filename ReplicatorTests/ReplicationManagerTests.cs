using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Replicator;
using ReplicatorService;
using ReplicatorService.ReplicationServiceManagers;
using ReplicatorTests.Helpers;

namespace ReplicatorTests
{
    [TestClass]
    public class ReplicationManagerTests
    {
        private ReplicationManager _replicationManager;
        private TestObject _testObject;
        private Mock<IReplicationServiceManager> _serviceManagerMock;

        [TestInitialize]
        public void SetUp()
        {
            _serviceManagerMock = new Mock<IReplicationServiceManager>();
            _replicationManager = new ReplicationManager(_serviceManagerMock.Object);
            _testObject = new TestObject();

            Assert.AreEqual(false,_testObject.BoolProperty);
        }

        [TestMethod]
        public void TestAttachItem()
        {
            _replicationManager.AttachObject(_testObject,"testObject");
            _testObject.BoolProperty = true;

            _serviceManagerMock.Verify(s=>s.SendUpdates(It.IsAny<ReplicatorDto>()), Times.Once);
        }

        [TestMethod]
        public void TestItemUpdates()
        {
            _replicationManager.AttachObject(_testObject, "testObject");

            _replicationManager.UpdatesCallback(new ReplicatorDto()
            {
                ObjectKey = "testObject",
                PropertyName = "BoolProperty",
                Value = true,
            });

            Assert.AreEqual(true, _testObject.BoolProperty);
        }
    }
}
