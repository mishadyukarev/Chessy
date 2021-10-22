using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class KingZoneUISys : IEcsRunSystem
    {
        private EcsFilter<KingZoneViewUIComp> _kingZoneFilter = default;
        private EcsFilter<InventorUnitsComponent> _invUnitFil = default;

        public void Run()
        {
            ref var kingZoneViewCom = ref _kingZoneFilter.Get1(0);


            if (_invUnitFil.Get1(0).HaveUnitInInv(WhoseMoveCom.CurPlayer, UnitTypes.King, LevelUnitTypes.Wood))
            {
                kingZoneViewCom.EnableZone();
            }
            else
            {
                kingZoneViewCom.DisableZone();
            }
        }
    }
}
