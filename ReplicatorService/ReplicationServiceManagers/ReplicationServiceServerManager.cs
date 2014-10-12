using System;
using System.Configuration;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace ReplicatorService.ReplicationServiceManagers
{
    public class ReplicationServiceServerManager : ReplicationServiceManagerBase
    {
        private ReplicatorWcfService _replicatorWcfService;
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
                    _replicatorWcfService = new ReplicatorWcfService(ReplicatorServiceCallback);
                    _host = new ServiceHost(_replicatorWcfService, new[] { new Uri(baseAddress) });

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

        protected override void SendUpdatesInternal(ReplicatorDto replicatorDto)
        {
            _replicatorWcfService.SendUpdates(replicatorDto);
        }

        public override void DisposeInternal()
        {
            if(_host != null)
                _host.Close();
        }
    }
}
