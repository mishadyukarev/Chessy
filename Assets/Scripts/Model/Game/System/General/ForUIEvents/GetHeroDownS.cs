using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Common.Enum;

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

                var myHeroT = eMG.PlayerInfoE(curPlayer).MyHeroTC.UnitT;

                if (eMG.PlayerInfoE(curPlayer).HaveHeroInInventor)
                {
                    if (!eMG.PlayerInfoE(eMG.CurPlayerITC.PlayerT).HeroCooldownC.HaveCooldown)
                    {
                        eMG.SelectedUnitE.UnitTC.UnitT = myHeroT;
                        eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;


                        eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                    }
                }
            }
            else
            {
                eMG.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                eMG.MistakeC.Timer = 0;
                eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
            }

            eMG.NeedUpdateView = true;
        }
    }
}