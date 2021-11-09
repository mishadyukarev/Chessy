using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            ScoutViewUIC.SetActiveScout(InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.Scout, LevelUnitTypes.First));
        }
    }
}