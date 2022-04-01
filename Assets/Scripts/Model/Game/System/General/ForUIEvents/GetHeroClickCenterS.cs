﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    public sealed class GetHeroClickCenterS : SystemModelGameAbs
    {
        internal GetHeroClickCenterS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        public void Get(in UnitTypes unitT)
        {
            if (eMG.CurPlayerITC.Is(eMG.WhoseMove.PlayerT))
            {
                eMC.SoundActionC(Common.Enum.ClipCommonTypes.Click).Invoke();

                eMG.RpcPoolEs.GetHeroToMaster(unitT);
            }
            else eMG.SoundActionC(ClipTypes.Mistake).Action.Invoke();

            eMG.NeedUpdateView = true;
        }
    }
}