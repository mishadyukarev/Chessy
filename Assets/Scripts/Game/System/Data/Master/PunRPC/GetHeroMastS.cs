using Leopotam.Ecs;

namespace Chessy.Game
{
    public class GetHeroMastS : IEcsRunSystem
    {
        public void Run()
        {
            UnitDoingMC.Get(out var unit);

            InvUnitsC.AddUnit(WhoseMoveC.WhoseMove, unit, LevelUnitTypes.First);
        }
    }
}