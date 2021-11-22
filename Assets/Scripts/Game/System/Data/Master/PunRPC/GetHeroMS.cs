using Game.Common;
using Leopotam.Ecs;

namespace Game.Game
{
    public class GetHeroMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);

            InvUnitsC.AddUnit(unit, LevelTypes.First, WhoseMoveC.WhoseMove);
        }
    }
}