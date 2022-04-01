﻿using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    public sealed class GetKingClickS : SystemModelGameAbs, IClickUI
    {
        internal GetKingClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click()
        {
            eMG.CellsC.Selected = 0;

            if (eMG.CurPlayerITC.Is(eMG.WhoseMove.PlayerT))
            {
                eMC.SoundActionC(ClipCommonTypes.Click).Invoke();

                if (eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).HaveKingInInventor)
                {
                    eMG.SelectedUnitE.UnitTC.UnitT = UnitTypes.King;
                    eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;

                    eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                }
            }
            else eMG.SoundActionC(ClipTypes.Mistake).Action.Invoke();

            eMG.NeedUpdateView = true;
        }
    }
}