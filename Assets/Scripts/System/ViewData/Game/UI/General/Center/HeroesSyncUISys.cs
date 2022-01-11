using static Game.Game.EntityCenterHeroUIPool;
using static Game.Game.EntityCenterPickUpgUIPool;
using static Game.Game.EntityCenterKingUIPool;

namespace Game.Game
{
    struct HeroesSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!Button<ButtonVC>().IsActiveParent && !Water<ButtonVC>().IsActiveParent && !WhereUnitsC.HaveMyHeroInGame)
            {
                Unit<ButtonVC>(UnitTypes.Elfemale).SetActiveParent(!InvUnitsC.Have(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI));
            }
            else
            {
                Unit<ButtonVC>(UnitTypes.Elfemale).SetActiveParent(false);
            }
        }
    }
}