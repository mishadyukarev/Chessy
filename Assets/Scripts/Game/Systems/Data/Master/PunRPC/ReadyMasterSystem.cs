using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class ReadyMasterSystem : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);


            ReadyDataUIC.SetIsReady(sender.IsMasterClient, !ReadyDataUIC.IsReady(sender.IsMasterClient));

            if (ReadyDataUIC.IsReady(true) && ReadyDataUIC.IsReady(false))
            {
                ReadyDataUIC.IsStartedGame = true;
            }

            else
            {
                ReadyDataUIC.IsStartedGame = false;
            }
        }
    }
}