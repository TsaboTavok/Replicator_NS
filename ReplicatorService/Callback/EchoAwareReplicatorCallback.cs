using System;

namespace ReplicatorService.Callback
{
    /// <summary> Wrapper коллбека, фильтрующий эхо </summary>
    internal class EchoAwareReplicatorCallback : IReplicatorServiceCallback
    {
        private readonly IReplicatorServiceCallback _callback;
        private readonly Guid _echoGuid;

        internal EchoAwareReplicatorCallback(IReplicatorServiceCallback callback, Guid echoGuid)
        {
            _callback = callback;
            _echoGuid = echoGuid;
        }

        public void UpdatesCallback(ReplicatorDto replicatorDto)
        {
            if (replicatorDto.CallerGuid != _echoGuid)
            {
                _callback.UpdatesCallback(replicatorDto);
            }
        }
    }
}
