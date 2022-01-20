using static Game.Game.CenterHerosUIE;
using static Game.Game.CenterUpgradeUIE;
using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    struct HeroesSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            if (!Button<ButtonUIC>().IsActiveParent && !Water<ButtonUIC>().IsActiveParent 
                && AvailableCenterHeroEs.HaveAvailHero<HaveAvailableHeroC>(WhoseMoveE.CurPlayerI).Have)
            {
                Parent.SetActive(!InventorUnitsE.Units(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveE.CurPlayerI).Have);
            }
            else
            {
                Parent.SetActive(false);
            }
        }
    }
}