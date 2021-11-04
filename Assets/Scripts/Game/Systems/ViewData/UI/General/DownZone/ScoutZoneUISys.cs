using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class ScoutZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            HeroZoneUIC.SetActiveScout(InvUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, UnitTypes.Scout, LevelUnitTypes.Wood));
        }
    }
}