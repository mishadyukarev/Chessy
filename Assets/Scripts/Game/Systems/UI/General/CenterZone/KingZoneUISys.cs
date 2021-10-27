using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, UnitTypes.King, LevelUnitTypes.Wood))
            {
                KingZoneViewUIC.EnableZone();
            }
            else
            {
                KingZoneViewUIC.DisableZone();
            }
        }
    }
}
