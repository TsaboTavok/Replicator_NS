using System;
using System.ComponentModel;

namespace Replicator
{
    public interface IReplicationManager : IDisposable
    {
        void AttachObject(INotifyPropertyChanged newObject, string key);

        event ServerShutdownEvent OnServerShutdown;
    }
}