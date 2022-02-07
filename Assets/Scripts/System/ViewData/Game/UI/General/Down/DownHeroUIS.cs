using static Game.Game.DownHeroUIE;

namespace Game.Game
{
    sealed class DownHeroUIS : SystemViewAbstract, IEcsRunSystem
    {
        internal DownHeroUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var curPlayerI = Es.WhoseMoveE.CurPlayerI;

            if (Es.InventorUnitsEs.HaveHero(curPlayerI, out var hero))
            {
                Parent.SetActive(true);

                var cooldown = Es.ScoutHeroCooldownE(hero, curPlayerI).Cooldown.Amount;

                for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
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