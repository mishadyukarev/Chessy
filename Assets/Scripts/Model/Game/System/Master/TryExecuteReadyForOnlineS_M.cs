using Chessy.Game.Model.Entity;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class TryExecuteReadyForOnlineS_M : SystemModel
    {
        internal TryExecuteReadyForOnlineS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryReady(in Player sender)
        {
            var playerSend = sender.GetPlayer();

            eMG.PlayerInfoE(playerSend).IsReady = !eMG.PlayerInfoE(playerSend).IsReady;

            if (eMG.PlayerInfoE(PlayerTypes.First).IsReady
                && eMG.PlayerInfoE(PlayerTypes.Second).IsReady)
            {
                eMG.IsStartedGame = true;
            }

            else
            {
                eMG.IsStartedGame = false;
            }
        }
    }
}