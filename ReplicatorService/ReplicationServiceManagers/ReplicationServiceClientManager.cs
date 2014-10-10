using System.Configuration;
using System.ServiceModel;

namespace ReplicatorService.ReplicationServiceManagers
{
    public class ReplicationServiceClientManager : ReplicationServiceManagerBase
    {
        private IReplicatorService _pipeProxy;

        protected override void InitInternal()
        {
            StartClient();
        }

        protected override void SendUpdatesInternal(ReplicatorDto replicatorDto)
        {
            _pipeProxy.SendUpdates(replicatorDto);
        }

        private void StartClient()
        {
            var group = GetServiceModelSectionGroup();

            if (group != null)
            {
                var pipeFactory = new DuplexChannelFactory<IReplicatorService>(ReplicatorServiceCallback, group.Client.Endpoints[0].Name);
                _pipeProxy = pipeFactory.CreateChannel();
                ((IClientChannel)_pipeProxy).Open();
                _pipeProxy.RegisterForUpdates();
            }
            else
            {
                throw new ConfigurationErrorsException("Нет необходомой конфигурации system.serviceModel");
            }
        }

        public override void DisposeInternal()
        {
            try
            {
                _pipeProxy.Unregister();
            }
            catch (CommunicationException ex)
            {
            }
        }
    }
}
