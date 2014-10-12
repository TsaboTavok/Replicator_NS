using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Replicator;

namespace Replicator_NS
{
    public class MainWindowVeiwModel : IDisposable
    {
        public ICommand AddClientCommand { get; set; }

        public ReplicationObject ReplicationObject { get; set; }

        public string Status { get; set; }

        private IReplicationManager _replicationManager;

        public MainWindowVeiwModel()
        {
            var managerResolver = new ReplicationManagerResolver();

            _replicationManager = managerResolver.Resolve();
            Status = managerResolver.IsServer()
                                        ? "Сервер"
                                        : "Клиент";

            AddClientCommand = new AddClientCommand();
            ReplicationObject = new ReplicationObject();
            _replicationManager.AttachObject(ReplicationObject,"object1");

        }

        public void Dispose()
        {
            _replicationManager.Dispose();
        }
    }
}
