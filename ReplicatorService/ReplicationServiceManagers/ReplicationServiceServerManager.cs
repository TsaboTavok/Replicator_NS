using System;
using System.Configuration;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace ReplicatorService.ReplicationServiceManagers
{
    public class ReplicationServiceServerManager : ReplicationServiceManagerBase
    {
        private ReplicatorService _replicatorService;
        private ServiceHost _host;
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        protected override void InitInternal()
        {
            var t = new Task(LaunchHost);
            t.Start();
            _autoResetEvent.WaitOne();
            if (t.IsFaulted)
            {
                t.Wait();
            }
        }

        private void LaunchHost()
        {
            try
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
            finally
            {
                _autoResetEvent.Set();
            }
        }

        public override void SendUpdates(ReplicatorDto replicatorDto)
        {
            _replicatorService.SendUpdates(replicatorDto);
        }

        public override void DisposeInternal()
        {
            _host.Close();
        }
    }
}
