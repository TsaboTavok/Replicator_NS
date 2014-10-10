using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Threading.Tasks;
using ReplicatorService.Callback;

namespace ReplicatorService.ReplicationServiceManagers
{
    public abstract class ReplicationServiceManagerBase : IReplicationServiceManager
    {
        protected IReplicatorServiceCallback ReplicatorServiceCallback;
        private Guid _managerGuid;

        public void Init(IReplicatorServiceCallback replicatorServiceCallback)
        {
            _managerGuid = Guid.NewGuid();

            var echoAwareCallback = new EchoAwareReplicatorCallback(replicatorServiceCallback, _managerGuid);
            ReplicatorServiceCallback = echoAwareCallback;
            InitInternal();
        }

        protected virtual void InitInternal()
        {
        }

        public void SendUpdates(ReplicatorDto replicatorDto)
        {
            replicatorDto.CallerGuid = _managerGuid;
            SendUpdatesInternal(replicatorDto);
        }

        protected abstract void SendUpdatesInternal(ReplicatorDto replicatorDto);

        protected ServiceModelSectionGroup GetServiceModelSectionGroup()
        {
            var group =
                ServiceModelSectionGroup.GetSectionGroup(
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
            return @group;
        }
        
        public void Dispose()
        {
            DisposeInternal();
            GC.SuppressFinalize(this);
        }

        public abstract void DisposeInternal();

        ~ReplicationServiceManagerBase()
        {
            this.Dispose();
        }
    }
}
