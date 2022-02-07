namespace Game.Game
{
    sealed class HeroesSyncUIS : SystemViewAbstract, IEcsRunSystem
    {
        public HeroesSyncUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var isActiveKingZone = CenterKingUIE.Paren.IsActiveSelf;
            var curPlayerI = Es.WhoseMoveE.CurPlayerI;

            if (!isActiveKingZone && !CenterUpgradeUIE.Paren.IsActiveSelf
                && Es.AvailableCenterHero(curPlayerI).HaveCenterHero.Have)
            {
                VEs.UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(!Es.InventorUnitsEs.HaveHero(curPlayerI, out var hero));
            }
            else
            {
                VEs.UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}