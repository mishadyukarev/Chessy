using static Game.Game.CenterHerosUIE;
using static Game.Game.CenterUpgradeUIE;

namespace Game.Game
{
    struct HeroesSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            var isActiveKingZone = CenterKingUIE.Paren.IsActiveSelf;
            var curPlayerI = Entities.WhoseMove.CurPlayerI;

            if (!isActiveKingZone && !CenterUpgradeUIE.Paren.IsActiveSelf
                && AvailableCenterHeroEs.HaveAvailHero(curPlayerI).Have)
            {
                Parent.SetActive(!InventorUnitsE.HaveHero(curPlayerI, out var hero));
            }
            else
            {
                Parent.SetActive(false);
            }
        }
    }
}