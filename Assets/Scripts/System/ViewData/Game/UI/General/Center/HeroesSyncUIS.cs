using static Game.Game.CenterHeroUIE;
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
                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent
                    (!InventorUnitsE.Units<AmountC>(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveE.CurPlayerI).Have);
            }
            else
            {
                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(false);
            }
        }
    }
}