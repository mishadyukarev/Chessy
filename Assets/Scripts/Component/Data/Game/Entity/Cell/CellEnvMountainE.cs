using ECS;

namespace Game.Game
{
    public sealed class CellEnvMountainE : CellEnvironmentE
    {
        public CellEnvMountainE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Mountain, ResourceTypes.None, idx, world)
        {
        }
    }
}