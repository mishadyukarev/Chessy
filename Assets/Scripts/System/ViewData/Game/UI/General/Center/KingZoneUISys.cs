using static Game.Game.EntityCenterKingUIPool;

namespace Game.Game
{
    struct KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InvUnitsC.Have(UnitTypes.King, LevelTypes.First, WhoseMoveC.CurPlayerI))
            {
                Button<ButtonUIC>().SetActiveParent(true);
            }
            else
            {
                Button<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
