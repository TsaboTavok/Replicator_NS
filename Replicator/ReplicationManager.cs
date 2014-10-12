using System;
using System.Collections.Generic;
using System.ComponentModel;
using ReplicatorService;
using ReplicatorService.Callback;
using ReplicatorService.ReplicationServiceManagers;

namespace Replicator
{
    internal class ReplicationManager : IReplicatorServiceCallback, IReplicationManager
    {
        private readonly object _locker = new object();
        private readonly Dictionary<string, INotifyPropertyChanged> _guidDictionary = new Dictionary<string, INotifyPropertyChanged>();
        private readonly Dictionary<INotifyPropertyChanged, string> _objectsDictionary = new Dictionary<INotifyPropertyChanged, string>();

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
                    newObject.PropertyChanged += newObject_PropertyChanged;
                    _objectsDictionary.Add(newObject, key);
                    _guidDictionary.Add(key, newObject);
                }
            }
        }

        void newObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var changedObject = sender as INotifyPropertyChanged;
            if (changedObject != null && _objectsDictionary.ContainsKey(changedObject))
            {
                var dto = _dtoBuilder.BuildReplicatorDto(sender, _objectsDictionary[changedObject], e.PropertyName);
                _serviceManager.SendUpdates(dto);
            }
        }

        public void UpdatesCallback(ReplicatorDto replicatorDto)
        {
            if (_guidDictionary.ContainsKey(replicatorDto.ObjectKey))
            {
                var changingObject =_guidDictionary[replicatorDto.ObjectKey];
                changingObject.GetType().GetProperty(replicatorDto.PropertyName).SetValue(changingObject, replicatorDto.Value);
            }
        }
    }
}
