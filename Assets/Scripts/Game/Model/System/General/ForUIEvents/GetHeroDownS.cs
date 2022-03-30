using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetHeroDownS : SystemModelGameAbs, IClickUI
    {
        internal GetHeroDownS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Click()
        {
            e.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                e.Sound(ClipTypes.Click).Invoke();

                var curPlayer = e.CurPlayerITC.Player;

                var myHeroT = e.PlayerInfoE(curPlayer).MyHeroTC.Unit;

                if (e.PlayerInfoE(curPlayer).HaveHeroInInventor)
                {
                    if (!e.PlayerInfoE(e.CurPlayerITC.Player).HeroCooldownC.HaveCooldown)
                    {
                        e.SelectedUnitE.UnitTC.Unit = myHeroT;
                        e.SelectedUnitE.LevelTC.Level = LevelTypes.First;


                        e.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
            }
            else
            {
                e.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                e.MistakeC.Timer = 0;
                e.Sound(ClipTypes.WritePensil).Action.Invoke();
            }

            e.NeedUpdateView = true;
        }
    }
}