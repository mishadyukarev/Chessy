using ECS;

namespace Game.Game
{
    public sealed class CellEnvYoungForestE : CellEnvironmentE
    {
        public CellEnvYoungForestE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.YoungForest, ResourceTypes.None, idx, world)
        {

        }
    }
}