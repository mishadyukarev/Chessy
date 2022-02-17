using static Game.Game.DownHeroUIE;

namespace Game.Game
{
    sealed class DownHeroUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownHeroUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayerI = Es.CurPlayerI.Player;

            var myHeroT = Es.ForPlayerE(curPlayerI).AvailableHeroTC.Unit;

            if (Es.ForPlayerE(curPlayerI).UnitsInfoE(myHeroT).HaveInInventor && myHeroT != UnitTypes.None)
            {
                Parent.SetActive(true);

                var cooldown = Es.ForPlayerE(curPlayerI).UnitsInfoE(myHeroT).ScoutHeroCooldownC.Cooldown;

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