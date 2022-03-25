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

            eMGame.PlayerInfoE(playerSend).IsReadyC = !eMGame.PlayerInfoE(playerSend).IsReadyC;

            if (eMGame.PlayerInfoE(PlayerTypes.First).IsReadyC
                && eMGame.PlayerInfoE(PlayerTypes.Second).IsReadyC)
            {
                eMGame.IsStartedGame = true;
            }

            else
            {
                eMGame.IsStartedGame = false;
            }
        }
    }
}