using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Replicator_NS.Annotations;

namespace Replicator_NS
{
    public class ReplicationObject : INotifyPropertyChanged
    {
        private string _stingProperty;
        private bool _boolProperty;
        private DateTime _dateTimeProperty;
        private int _intProperty;

        public string StingProperty
        {
            get { return _stingProperty; }
            set
            {
                if (value == _stingProperty) return;
                _stingProperty = value;
                OnPropertyChanged();
            }
        }

        public bool BoolProperty
        {
            get { return _boolProperty; }
            set
            {
                if (value.Equals(_boolProperty)) return;
                _boolProperty = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateTimeProperty
        {
            get { return _dateTimeProperty; }
            set
            {
                if (value.Equals(_dateTimeProperty)) return;
                _dateTimeProperty = value;
                OnPropertyChanged();
            }
        }

        public int IntProperty
        {
            get { return _intProperty; }
            set
            {
                if (value == _intProperty) return;
                _intProperty = value;
                OnPropertyChanged();
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
