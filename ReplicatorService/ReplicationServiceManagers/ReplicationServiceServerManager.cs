using System;
using System.Configuration;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ReplicatorService.ReplicationServiceManagers
{
    public class ReplicationServiceServerManager : ReplicationServiceManagerBase
    {
        private ReplicatorService _replicatorService;
        private ServiceHost _host;

        protected override void InitInternal()
        {
            var t = new Task(LaunchHost);
            t.Start();
        }

        private void LaunchHost()
        {
            var group = GetServiceModelSectionGroup();

            if (group != null)
            {
                var service = group.Services.Services[0];
                var baseAddress = service.Endpoints[0].Address.AbsoluteUri.Replace(
                    service.Endpoints[0].Address.AbsolutePath, String.Empty);
                _replicatorService = new ReplicatorService(ReplicatorServiceCallback);
                _host = new ServiceHost(_replicatorService, new[] { new Uri(baseAddress) });

                _host.Open();
            }
            else
            {
                throw new ConfigurationErrorsException("Нет необходомой конфигурации system.serviceModel");
            }
        }

        public override void SendUpdates(ReplicatorDto replicatorDto)
        {
            _replicatorService.SendUpdates(replicatorDto);
        }
    }
}
