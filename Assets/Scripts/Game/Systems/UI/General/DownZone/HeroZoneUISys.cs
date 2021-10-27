using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class HeroZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            HeroZoneUIC.SetActiveScout(InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, UnitTypes.Scout, LevelUnitTypes.Wood));
        }
    }
}