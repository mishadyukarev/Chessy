using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class HeroZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            HeroZoneUIC.SetActiveScout(InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, UnitTypes.Scout, LevelUnitTypes.Wood));
        }
    }
}