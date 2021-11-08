using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ReadyMasterSystem : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var playerSend = sender.GetPlayerType();


            ReadyDataUIC.SetIsReady(playerSend, !ReadyDataUIC.IsReady(playerSend));

            if (ReadyDataUIC.IsReady(PlayerTypes.First) && ReadyDataUIC.IsReady(PlayerTypes.Second))
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