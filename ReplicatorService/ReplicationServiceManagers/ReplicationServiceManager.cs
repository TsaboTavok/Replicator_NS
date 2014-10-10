using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ReplicatorService.ReplicationServiceManagers
{
    public class ReplicationServiceManager
    {
        private ReplicatorService _replicatorService;
        private IReplicatorServiceCallback _replicatorServiceCallback;
        private ServiceHost _host;
        private IReplicatorService _pipeProxy;

        public void Init(IReplicatorServiceCallback replicatorServiceCallback)
        {
            _replicatorServiceCallback = replicatorServiceCallback;
        }

        public void HostServer()
        {
            var t = new Task(LaunchHost);
            t.Start();
        }
        public void CreateClient()
        {
            StartClient(_replicatorServiceCallback);
        }

        public void SendUpdates(ReplicatorDto replicatorDto)
        {
            _replicatorService.SendUpdates(replicatorDto);
        }

        private void LaunchHost()
        {
             var group = GetServiceModelSectionGroup();

             if (group != null)
             {

                 var service = group.Services.Services[0];
                 var baseAddress = service.Endpoints[0].Address.AbsoluteUri.Replace(
                     service.Endpoints[0].Address.AbsolutePath, String.Empty);
                 _replicatorService = new ReplicatorService();
                 _host = new ServiceHost(_replicatorService, new[] { new Uri(baseAddress) });

                 _host.Open();
             }
             else
             {
                 throw new ConfigurationErrorsException("Нет необходомой конфигурации system.serviceModel");
             }
        }

        private void StartClient(IReplicatorServiceCallback callback)
        {
            var group = GetServiceModelSectionGroup();

            if (group != null)
            {
                var pipeFactory = new DuplexChannelFactory<IReplicatorService>(callback, group.Client.Endpoints[0].Name);
                _pipeProxy = pipeFactory.CreateChannel();
                ((IClientChannel)_pipeProxy).Open();
                _pipeProxy.RegisterForUpdates();
            }
            else
            {
                throw new ConfigurationErrorsException("Нет необходомой конфигурации system.serviceModel");
            }
        }

        protected ServiceModelSectionGroup GetServiceModelSectionGroup()
        {
            var group =
                ServiceModelSectionGroup.GetSectionGroup(
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
            return @group;
        }
    }
}
