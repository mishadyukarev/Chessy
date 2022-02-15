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
            var curPlayerI = Es.WhoseMovePlayerTC.CurPlayerI;

            if (Es.HaveHeroInInventor(curPlayerI, out var hero))
            {
                Parent.SetActive(true);

                var cooldown = Es.ScoutHeroCooldownE(hero, curPlayerI).CooldownC.Amount;

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