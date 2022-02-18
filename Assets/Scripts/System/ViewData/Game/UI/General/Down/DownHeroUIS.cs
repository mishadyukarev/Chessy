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

            var myHeroT = Es.PlayerE(curPlayerI).AvailableHeroTC.Unit;

            if (myHeroT != UnitTypes.None && Es.PlayerE(curPlayerI).UnitsInfoE(myHeroT).HaveInInventor)
            {
                Parent.SetActive(true);

                var cooldown = Es.PlayerE(curPlayerI).UnitsInfoE(myHeroT).ScoutHeroCooldownC.Cooldown;

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