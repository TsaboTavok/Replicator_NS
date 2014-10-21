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
