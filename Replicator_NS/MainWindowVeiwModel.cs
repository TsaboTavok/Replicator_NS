using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Replicator;
using Replicator_NS.Annotations;

namespace Replicator_NS
{
    public class MainWindowVeiwModel : IDisposable , INotifyPropertyChanged
    {
        public ICommand AddClientCommand { get; set; }

        public ReplicationObject ReplicationObject { get; set; }

        public ReplicationObject ReplicationObject2 { get; set; }

        public string Status
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public bool ServerOnline
        {
            get { return _serverOnline; }
            set
            {
                if (value.Equals(_serverOnline)) return;
                _serverOnline = value;
                OnPropertyChanged();
            }
        }

        private IReplicationManager _replicationManager;
        private string _status;
        private bool _serverOnline;

        public MainWindowVeiwModel()
        {
            AddClientCommand = new AddClientCommand();
            ReplicationObject = new ReplicationObject();
            ReplicationObject2 = new ReplicationObject();

            InitReplication();
        }

        private void InitReplication()
        {
            try
            {
                var managerResolver = new ReplicationManagerResolver();
                _replicationManager = managerResolver.Resolve();
                _replicationManager.OnServerShutdown += OnReplicationManagerOnOnServerShutdown;
                _replicationManager.AttachObject(ReplicationObject, "object1");
                _replicationManager.AttachObject(ReplicationObject2, "object2");
                Status = managerResolver.IsServer()
                    ? "Сервер"
                    : "Клиент";
                ServerOnline = true;
            }
            catch (Exception)
            {
                Status = "Ошибка инициализации сервиса";
                ServerOnline = false;
            }
        }

        private void OnReplicationManagerOnOnServerShutdown()
        {
            Status = "Сервер отключился";
            ServerOnline = false;
        }

        public void Dispose()
        {
            if (_replicationManager != null)
            {
                _replicationManager.OnServerShutdown -= OnReplicationManagerOnOnServerShutdown;
                _replicationManager.Dispose();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
