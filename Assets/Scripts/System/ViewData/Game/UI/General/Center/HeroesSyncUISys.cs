using static Game.Game.EntityCenterHeroUIPool;
using static Game.Game.EntityCenterPickUpgUIPool;
using static Game.Game.EntityCenterKingUIPool;

namespace Game.Game
{
    struct HeroesSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!Button<ButtonUIC>().IsActiveParent && !Water<ButtonUIC>().IsActiveParent && !WhereUnitsC.HaveMyHeroInGame)
            {
                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(!InvUnitsC.Have(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI));
            }
            else
            {
                Unit<ButtonUIC>(UnitTypes.Elfemale).SetActiveParent(false);
            }
        }
    }
}