using System.ComponentModel;

namespace Replicator
{
    public interface IReplicationManager
    {
        void AttachObject(INotifyPropertyChanged newObject, string key);
    }
}