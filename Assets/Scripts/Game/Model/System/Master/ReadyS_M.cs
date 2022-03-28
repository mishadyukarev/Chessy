using Chessy.Game.Entity.Model;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class ReadyS_M : SystemModelGameAbs
    {
        public ReadyS_M(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Ready(in Player sender)
        {
            var playerSend = sender.GetPlayer();

            e.PlayerInfoE(playerSend).IsReadyC = !e.PlayerInfoE(playerSend).IsReadyC;

            if (e.PlayerInfoE(PlayerTypes.First).IsReadyC
                && e.PlayerInfoE(PlayerTypes.Second).IsReadyC)
            {
                e.IsStartedGame = true;
            }

            else
            {
                e.IsStartedGame = false;
            }
        }
    }
}