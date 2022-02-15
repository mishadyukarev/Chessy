﻿using static Game.Game.DownScoutUIEs;

namespace Game.Game
{
    sealed class ScoutSyncUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ScoutSyncUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayer = Es.WhoseMovePlayerTC.CurPlayerI;

            var isActive = Es.Units(UnitTypes.Scout, LevelTypes.First, curPlayer).HaveUnits;
            var cooldown = Es.ScoutHeroCooldownE(UnitTypes.Scout, curPlayer).CooldownC.Amount;


            Scout<ButtonUIC>().SetActive(isActive);

            if (isActive && cooldown > 0)
            {
                Cooldown<TextUIC>().SetActiveParent(true);
                Cooldown<TextUIC>().Text = cooldown.ToString();
            }
            else
            {
                Cooldown<TextUIC>().SetActiveParent(false);
            }
        }
    }
}