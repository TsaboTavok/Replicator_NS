using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReplicatorTests.Annotations;

namespace ReplicatorTests.Helpers
{
    public class TestObject : INotifyPropertyChanged
    {
        private bool _boolProperty;
        private string _stringProperty;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public string StringProperty
        {
            get { return _stringProperty; }
            set
            {
                if (value == _stringProperty) return;
                _stringProperty = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
