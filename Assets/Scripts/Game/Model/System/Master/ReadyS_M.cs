using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class ReadyS_M : SystemModelGameAbs
    {
        internal ReadyS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Ready(in Player sender)
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