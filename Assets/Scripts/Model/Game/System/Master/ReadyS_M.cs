using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
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

            eMG.PlayerInfoE(playerSend).IsReadyC = !eMG.PlayerInfoE(playerSend).IsReadyC;

            if (eMG.PlayerInfoE(PlayerTypes.First).IsReadyC
                && eMG.PlayerInfoE(PlayerTypes.Second).IsReadyC)
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