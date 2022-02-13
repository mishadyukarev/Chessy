using ECS;

namespace Game.Game
{
    public sealed class CellEnvFertilizerE : CellEnvironmentE
    {

        internal CellEnvFertilizerE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Fertilizer, ResourceTypes.Food, idx, world)
        {
        }

        public void AddAfterBuildingFarm() => Add(CellEnvironmentValues.AddingAfterBuildingFarm);
        public void AddFromIceWall() => Add(CellEnvironmentValues.AddingFromIceWall(EnvT));
        public void AddFromNearRiver() => Add(100);
        public void AddFromCloud() => Add(100);
        public void TakeDry() => Take(10);
    }
}