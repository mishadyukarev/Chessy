using ECS;

namespace Game.Game
{
    public sealed class CellEnvYoungForestE : CellEnvironmentE
    {
        internal CellEnvYoungForestE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.YoungForest, ResourceTypes.None, idx, world)
        {

        }

        public void TrySetAfterFireForest()
        {
            if (UnityEngine.Random.Range(0f, 1f) < CellEnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
            {
                SetRandomResources();
            }
        }
    }
}