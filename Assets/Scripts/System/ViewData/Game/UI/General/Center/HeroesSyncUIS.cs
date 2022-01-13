using static Game.Game.CenterHeroUIE;
using static Game.Game.CenterUpgradeUIE;
using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    struct HeroesSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            if (!Button<ButtonUIC>().IsActiveParent && !Water<ButtonUIC>().IsActiveParent /*&& !WhereUnitsC.HaveMyHeroInGame*/)
            {
                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent
                    (!EntInventorUnits.Units<AmountC>(UnitTypes.Elfemale, LevelTypes.First, EntWhoseMove.CurPlayerI).Have);
            }
            else
            {
                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(false);
            }
        }
    }
}