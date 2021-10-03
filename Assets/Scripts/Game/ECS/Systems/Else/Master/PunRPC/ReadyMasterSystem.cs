using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class ReadyMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyUIFilter = default;

        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            ref var readyDataUICom = ref _readyUIFilter.Get1(0);


            readyDataUICom.SetIsReady(sender.IsMasterClient, !readyDataUICom.IsReady(sender.IsMasterClient));

            if (readyDataUICom.IsReady(true) && readyDataUICom.IsReady(false))
            {
                readyDataUICom.IsStartedGame = true;
            }

            else
            {
                readyDataUICom.IsStartedGame = false;
            }
        }
    }
}