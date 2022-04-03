﻿using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class GetHeroDownS : SystemModelGameAbs, IClickUI
    {
        internal GetHeroDownS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click()
        {
            eMG.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (eMG.CurPlayerITC.Is(eMG.WhoseMove.PlayerT))
            {
                eMC.SoundActionC(ClipCommonTypes.Click).Invoke();

                var curPlayer = eMG.CurPlayerITC.PlayerT;

                var myHeroT = eMG.PlayerInfoE(curPlayer).GodInfoE.UnitT;

                if (eMG.PlayerInfoE(curPlayer).GodInfoE.HaveHeroInInventor)
                {
                    if (!eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).GodInfoE.CooldownC.HaveCooldown)
                    {
                        eMG.SelectedUnitE.UnitTC.UnitT = myHeroT;
                        eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;


                        eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                    }
                }
            }
            else
            {
                sMG.SetMistakeS.Set(MistakeTypes.NeedWaitQueue, 0);
                eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
            }

            eMG.NeedUpdateView = true;
        }
    }
}