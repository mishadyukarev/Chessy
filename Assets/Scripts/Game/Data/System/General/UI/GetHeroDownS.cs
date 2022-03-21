namespace Chessy.Game.System.Model
{
    public struct GetHeroDownS
    {
        public void Get(in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            e.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                e.Sound(ClipTypes.Click).Invoke();

                var curPlayer = e.CurPlayerITC.Player;

                var myHeroT = e.PlayerInfoE(curPlayer).AvailableHeroTC.Unit;

                if (e.PlayerInfoE(curPlayer).HaveHeroInInventor)
                {
                    if (!e.PlayerInfoE(e.CurPlayerITC.Player).HeroCooldownC.HaveCooldown)
                    {
                        e.SelectedE.UnitC.Set(myHeroT, LevelTypes.First);
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
        }
    }
}