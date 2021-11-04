using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InvUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, UnitTypes.King, LevelUnitTypes.Wood))
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
