using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplicatorService;

namespace Replicator
{
    internal class ReplicatorDtoBuilder
    {
        internal ReplicatorDto BuildReplicatorDto(object changedObject, string key, string propertyName)
        {
            return new ReplicatorDto()
            {
                ObjectKey = key,
                Value = changedObject.GetType().GetProperty(propertyName).GetValue(changedObject, null),
                PropertyName = propertyName
            };
        }
    }
}
