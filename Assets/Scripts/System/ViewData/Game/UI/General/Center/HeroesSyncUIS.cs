namespace Game.Game
{
    sealed class HeroesSyncUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal HeroesSyncUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var isActiveKingZone = CenterKingUIE.Paren.IsActiveSelf;
            var curPlayerI = Es.WhoseMoveE.CurPlayerI;

            if (!isActiveKingZone && !CenterUpgradeUIE.Paren.IsActiveSelf
                && Es.AvailableCenterHero(curPlayerI).HaveCenterHero.Have)
            {
                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(!Es.InventorUnitsEs.HaveHero(curPlayerI, out var hero));
            }
            else
            {
                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}