using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class GetHeroDownS : SystemModel, IClickUI
    {
        internal GetHeroDownS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click()
        {
            eMG.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
            {
                eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

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
                sMG.MistakeSs.SetMistakeS.Set(MistakeTypes.NeedWaitQueue, 0);
                eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
            }

            eMG.NeedUpdateView = true;
        }
    }
}