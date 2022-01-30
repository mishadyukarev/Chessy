using static Game.Game.CenterHerosUIE;

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
            var curPlayerI = Es.WhoseMove.CurPlayerI;

            if (!isActiveKingZone && !CenterUpgradeUIE.Paren.IsActiveSelf
                && Es.AvailableCenterHero(curPlayerI).HaveCenterHero.Have)
            {
                Parent.SetActive(!Es.InventorUnitsEs.HaveHero(curPlayerI, out var hero));
            }
            else
            {
                Parent.SetActive(false);
            }
        }
    }
}