﻿namespace Game.Game
{
    sealed class HeroesSyncUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal HeroesSyncUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var isActiveKingZone = CenterKingUIE.Paren.IsActiveSelf;
            var curPlayerI = E.CurPlayerITC.Player;

            if (!isActiveKingZone && !CenterUpgradeUIE.Paren.IsActiveSelf
                && E.PlayerE(curPlayerI).HaveCenterHero)
            {
                //var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else
            {
                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}