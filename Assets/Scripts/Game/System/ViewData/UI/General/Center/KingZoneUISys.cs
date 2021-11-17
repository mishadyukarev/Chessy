using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.King, LevelTypes.First))
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
