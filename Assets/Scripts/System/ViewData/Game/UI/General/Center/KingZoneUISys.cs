using static Game.Game.EntityCenterKingUIPool;

namespace Game.Game
{
    struct KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InvUnitsC.Have(UnitTypes.King, LevelTypes.First, WhoseMoveC.CurPlayerI))
            {
                Button<ButtonVC>().SetActiveParent(true);
            }
            else
            {
                Button<ButtonVC>().SetActiveParent(false);
            }
        }
    }
}
