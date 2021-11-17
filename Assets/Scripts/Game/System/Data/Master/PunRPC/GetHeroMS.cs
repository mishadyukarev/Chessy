using Chessy.Common;
using Leopotam.Ecs;

namespace Chessy.Game
{
    public class GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);

            InvUnitsC.AddUnit(WhoseMoveC.WhoseMove, unit, LevelTypes.First);
        }
    }
}