using ECS;

namespace Game.Game
{
    public sealed class CellEnvYoungForestE : CellEnvironmentE
    {
        internal CellEnvYoungForestE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.YoungForest, ResourceTypes.None, idx, world)
        {

        }

        public void TrySetAfterFireForest(in WhereEnviromentEs whereEnviromentEs)
        {
            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                SetNewRandom(whereEnviromentEs);
            }
        }
    }
}