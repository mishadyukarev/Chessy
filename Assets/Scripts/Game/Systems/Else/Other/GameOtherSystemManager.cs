using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class GameOtherSystemManager : SystemAbstManager
    {
        public GameOtherSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
        {

        }
    }
}