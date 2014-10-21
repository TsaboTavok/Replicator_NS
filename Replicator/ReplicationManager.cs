using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ReplicatorService;
using ReplicatorService.Callback;
using ReplicatorService.ReplicationServiceManagers;

namespace Replicator
{
    internal class ReplicationManager : IReplicatorServiceCallback, IReplicationManager, IDisposable
    {
        private readonly object _locker = new object();
        private readonly Dictionary<string, INotifyPropertyChanged> _guidDictionary = new Dictionary<string, INotifyPropertyChanged>();
        private readonly Dictionary<INotifyPropertyChanged, string> _objectsDictionary = new Dictionary<INotifyPropertyChanged, string>();
        private bool _supressed = false; 

        private readonly IReplicationServiceManager _serviceManager;
        private readonly ReplicatorDtoBuilder _dtoBuilder;

        internal ReplicationManager(IReplicationServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            serviceManager.Init(this);
            _dtoBuilder = new ReplicatorDtoBuilder();
        }

        public void AttachObject(INotifyPropertyChanged newObject, string key)
        {
            lock (_locker)
            {
                if (!_objectsDictionary.ContainsKey(newObject))
                {
                    newObject.PropertyChanged += trackedObject_PropertyChanged;
                    _objectsDictionary.Add(newObject, key);
                    _guidDictionary.Add(key, newObject);
                }
            }
        }

        void trackedObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            lock (_locker)
            {
                var changedObject = sender as INotifyPropertyChanged;
                if (changedObject != null && _objectsDictionary.ContainsKey(changedObject))
                {
                    if (!_supressed)
                    {
                        var dto = _dtoBuilder.BuildReplicatorDto(sender, _objectsDictionary[changedObject],
                            e.PropertyName);
                        _serviceManager.SendUpdates(dto);
                    }
                    else
                    {
                        _supressed = false;
                    }
                }
            }
        }

        public void UpdatesCallback(ReplicatorDto replicatorDto)
        {
            lock (_locker)
            {
                if (_guidDictionary.ContainsKey(replicatorDto.ObjectKey))
                {
                    var changingObject = _guidDictionary[replicatorDto.ObjectKey];
                    _supressed = true;
                    changingObject.GetType()
                        .GetProperty(replicatorDto.PropertyName)
                        .SetValue(changingObject, replicatorDto.Value);
                }
            }   
        }

        public event ServerShutdownEvent OnServerShutdown;

        public void ServerShutdownCallback()
        {
            var subs = OnServerShutdown;
            if (subs != null)
            {
                subs();
            }
            Dispose();
        }

        public void Dispose()
        {
            foreach (var obj in _objectsDictionary.Keys)
            {
                obj.PropertyChanged -= trackedObject_PropertyChanged;
            }

            _objectsDictionary.Clear();
            _guidDictionary.Clear();
            _serviceManager.Dispose();
        }
    }
}
