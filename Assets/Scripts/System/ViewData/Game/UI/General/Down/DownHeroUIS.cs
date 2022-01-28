﻿using static Game.Game.DownHeroUIE;

namespace Game.Game
{
    struct DownHeroUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayerI = Entities.WhoseMove.CurPlayerI;

            if (InventorUnitsE.HaveHero(curPlayerI, out var hero))
            {
                Parent.SetActive(true);

                var cooldown = Entities.ScoutHeroCooldownE(hero, curPlayerI).Cooldown.Amount;

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