namespace Game.Game
{
    sealed class HeroesSyncUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal HeroesSyncUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var isActiveKingZone = CenterKingUIE.Paren.IsActiveSelf;
            var curPlayerI = Es.CurPlayerI.Player;

            if (!isActiveKingZone && !CenterUpgradeUIE.Paren.IsActiveSelf
                && Es.PlayerE(curPlayerI).HaveCenterHeroC)
            {
                var myHeroT = Es.PlayerE(curPlayerI).AvailableHeroTC.Unit;

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(!Es.PlayerE(curPlayerI).UnitsInfoE(myHeroT).HaveInInventor);
            }
            else
            {
                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}