using System;
using Moq;
using ReplicatorService;
using ReplicatorService.ReplicationServiceManagers;

namespace ReplicatorTests.Helpers
{
    public abstract class ReplicationServiceTestHelperBase : IDisposable
    {
        protected ReplicationServiceTestHelperBase()
        {
            Manager = CreateReplicationServiceManager();
            CallbackMock = new Mock<IReplicatorServiceCallback>();
            Manager.Init(CallbackMock.Object);
        }

        protected abstract ReplicationServiceManagerBase CreateReplicationServiceManager();

        public ReplicationServiceManagerBase Manager { get; private set; }
        public Mock<IReplicatorServiceCallback> CallbackMock { get; private set; }

        public void Dispose()
        {
            Manager.Dispose();
        }
    }

    public class ReplicationServiceTestServerHelper : ReplicationServiceTestHelperBase
    {
        public ReplicationServiceTestServerHelper()
        {
        }

        protected override ReplicationServiceManagerBase CreateReplicationServiceManager()
        {
            return  new ReplicationServiceServerManager();
        }
    }
    public class ReplicationServiceTestClientHelper : ReplicationServiceTestHelperBase
    {
        public ReplicationServiceTestClientHelper()
        {
        }

        protected override ReplicationServiceManagerBase CreateReplicationServiceManager()
        {
            return new ReplicationServiceClientManager();
        }
    }
}
