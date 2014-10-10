using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ReplicatorService
{
    public interface IReplicatorServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdatesCallback(ReplicatorDto replicatorDto);
    }
}
