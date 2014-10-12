using System;
using System.Runtime.Serialization;

namespace ReplicatorService
{
    [DataContract]
    public class ReplicatorDto
    {
        [DataMember]
        internal Guid CallerGuid { get; set; }

        [DataMember]
        public string ObjectKey { get; set; }

        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public string PropertyName { get; set; }
    }
}