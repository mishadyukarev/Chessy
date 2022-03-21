﻿namespace Chessy.Game
{
    sealed class CenterHeroesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterHeroesUIS( in EntitiesViewUI entsUI, in Chessy.Game.Entity.Model.EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var isActiveKingZone = UIE.CenterEs.KingE.Paren.IsActiveSelf;
            var curPlayerI = E.CurPlayerITC.Player;

            if (!isActiveKingZone && E.PlayerInfoE(curPlayerI).AvailableHeroTC.Is(UnitTypes.None))
            {
                //var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

                UIE.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else
            {
                UIE.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}