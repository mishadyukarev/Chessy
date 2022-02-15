using ECS;

namespace Game.Game
{
    public sealed class CellEnvFertilizerE : CellEnvironmentE
    {

        internal CellEnvFertilizerE(in CellEs cellEs, in EcsWorld world) : base(EnvironmentTypes.Fertilizer, ResourceTypes.Food, cellEs, world)
        {
        }

        public void AddAfterBuildingFarm() => Add(CellEnvironmentValues.AddingAfterBuildingFarm);
        public void AddFromIceWall() => Add(CellEnvironmentValues.AddingFromIceWall(EnvironmentT));
        public void AddFromNearRiver() => Add(1);
        public void AddFromCloud() => Add(1);
        public void TakeDry() => Take(0.1f);
    }
}