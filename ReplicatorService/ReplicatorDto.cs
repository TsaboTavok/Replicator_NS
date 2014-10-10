using System;
using System.Runtime.Serialization;

namespace ReplicatorService
{
    [DataContract]
    public class ReplicatorDto
    {
        [DataMember]
        internal Guid CallerGuid { get; set; }

        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}