using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            ScoutViewUIC.SetActiveScout(InvUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, UnitTypes.Scout, LevelUnitTypes.Wood));
        }
    }
}