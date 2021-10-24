using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class HeroZoneUISys : IEcsRunSystem
    {
        private EcsFilter<HeroZoneUICom> _heroZoneUICom = default;
        private EcsFilter<InventorUnitsCom> _inventUnits = default;

        public void Run()
        {
            ref var heroZoneUICom = ref _heroZoneUICom.Get1(0);
            ref var invUnitsCom = ref _inventUnits.Get1(0);

            heroZoneUICom.SetActiveScout(invUnitsCom.HaveUnitInInv(WhoseMoveCom.CurPlayer, UnitTypes.Scout, LevelUnitTypes.Wood));
        }
    }
}