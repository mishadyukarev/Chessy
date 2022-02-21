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
            var curPlayerI = E.CurPlayerI.Player;

            var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

            if (myHeroT != UnitTypes.None && E.PlayerE(curPlayerI).UnitsInfoE(myHeroT).HaveInInventor)
            {
                Parent.SetActive(true);

                var cooldown = E.PlayerE(curPlayerI).UnitsInfoE(myHeroT).ScoutHeroCooldownC.Cooldown;

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