using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetHeroDownS : SystemModelGameAbs, IClickUI
    {
        public GetHeroDownS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            eMGame.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                eMGame.Sound(ClipTypes.Click).Invoke();

                var curPlayer = eMGame.CurPlayerITC.Player;

                var myHeroT = eMGame.PlayerInfoE(curPlayer).MyHeroTC.Unit;

                if (eMGame.PlayerInfoE(curPlayer).HaveHeroInInventor)
                {
                    if (!eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).HeroCooldownC.HaveCooldown)
                    {
                        eMGame.SelectedE.UnitC.Set(myHeroT, LevelTypes.First);
                        eMGame.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
            }
            else
            {
                eMGame.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                eMGame.MistakeC.Timer = 0;
                eMGame.Sound(ClipTypes.WritePensil).Action.Invoke();
            }
        }
    }
}