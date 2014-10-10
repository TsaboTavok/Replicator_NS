using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ReplicatorService;
using ReplicatorService.ReplicationServiceManagers;

namespace ReplicatorTests.Helpers
{
    public class ReplicationServiceHelperBase
    {
        public ReplicationServiceHelperBase()
        {
            Manager = new ReplicationServiceManager();
            CallbackMock = new Mock<IReplicatorServiceCallback>();
            Manager.Init(CallbackMock.Object);
        }

        public ReplicationServiceManager Manager { get; private set; }
        public Mock<IReplicatorServiceCallback> CallbackMock { get; private set; }
    }

    public class ReplicationServiceServerHelper : ReplicationServiceHelperBase
    {
        public ReplicationServiceServerHelper()
            : base()
        {
            Manager.HostServer();
        }
    }
    public class ReplicationServiceClientHelper : ReplicationServiceHelperBase
    {
        public ReplicationServiceClientHelper()
            : base()
        {
            Manager.CreateClient();
        }
    }
}
