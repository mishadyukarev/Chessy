using ECS;

namespace Game.Game
{
    public sealed class CellEnvAdultForestE : CellEnvironmentE
    {

        public CellEnvAdultForestE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.AdultForest, ResourceTypes.Wood, idx, world)
        {
        }

        public void Destroy(in CellTrailE[] trailEs, in WhereEnviromentEs whereEnviromentEs)
        {
            base.Destroy(whereEnviromentEs);
            foreach (var trailE in trailEs) trailE.Destroy();
        }
        public void Fire()
        {
            Resources.Take(CellEnvironmentValues.MaxResources(EnvironmentTC.Environment) / 2);
        }
    }
}