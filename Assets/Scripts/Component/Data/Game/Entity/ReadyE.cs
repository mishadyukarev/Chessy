using ECS;
using Photon.Realtime;

namespace Game.Game
{
    public sealed class ReadyE : EntityAbstract
    {
        public ref IsReadyC IsReadyC => ref Ent.Get<IsReadyC>();

        public ReadyE(in EcsWorld gameW) : base(gameW)
        {

        }

        public void Ready_Master(in Player sender, in Entities e)
        {
            var playerSend = sender.GetPlayer();

            e.Ready(playerSend).IsReadyC.IsReady = !e.Ready(playerSend).IsReadyC.IsReady;

            if (e.Ready(PlayerTypes.First).IsReadyC.IsReady
                && e.Ready(PlayerTypes.Second).IsReadyC.IsReady)
            {
                e.GameInfo.IsStartedGameC.IsStartedGame = true;
            }

            else
            {
                e.GameInfo.IsStartedGameC.IsStartedGame = false;
            }
        }
    }
}