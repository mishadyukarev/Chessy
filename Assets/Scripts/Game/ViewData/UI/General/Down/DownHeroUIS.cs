using static Game.Game.DownHeroUIE;

namespace Game.Game
{
    sealed class DownHeroUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownHeroUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayerI = E.CurPlayerITC.Player;

            var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

            if (myHeroT != UnitTypes.None && E.UnitInfo(curPlayerI, LevelTypes.First, myHeroT).HaveInInventor)
            {
                Parent.SetActive(true);

                var cooldown = E.UnitInfo(curPlayerI, LevelTypes.First, myHeroT).ScoutHeroCooldownC.Cooldown;

                for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
                {
                    Image(unit).SetActive(false);
                }

                Image(myHeroT).SetActive(true);

                if (cooldown > 0)
                {
                    Cooldown.SetActiveParent(true);
                    Cooldown.Text = cooldown.ToString();
                }
                else
                {
                    Cooldown.SetActiveParent(false);
                }
            }
            else
            {
                Parent.SetActive(false);
            }

        }
    }
}