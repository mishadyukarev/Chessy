using static Game.Game.DownHeroUIE;

namespace Game.Game
{
    sealed class DownHeroUIS : SystemViewAbstract, IEcsRunSystem
    {
        public DownHeroUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var curPlayerI = Es.WhoseMove.CurPlayerI;

            if (Es.InventorUnitsEs.HaveHero(curPlayerI, out var hero))
            {
                Parent.SetActive(true);

                var cooldown = Es.ScoutHeroCooldownE(hero, curPlayerI).Cooldown.Amount;

                for (var unit = UnitTypes.Elfemale; unit <= UnitTypes.Snowy; unit++)
                {
                    Image(unit).SetActive(false);
                }

                Image(hero).SetActive(true);

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