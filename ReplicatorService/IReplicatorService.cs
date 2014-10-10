﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ReplicatorService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IReplicatorServiceCallback))]
    public interface IReplicatorService
    {
        [OperationContract]
        void RegisterForUpdates();

        [OperationContract(IsOneWay = true)]
        void Unregister();

        [OperationContract]
        void SendUpdates(ReplicatorDto replicatorDto);
    }
}
