using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class ReadyS_M : SystemModelGameAbs
    {
        public ReadyS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Ready(in Player sender)
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