using ECS;

namespace Game.Game
{
    public sealed class CellEnvFertilizerE : CellEnvironmentE
    {
        public CellEnvFertilizerE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Fertilizer, ResourceTypes.Food, idx, world)
        {
        }

        public void SetNew()
        {
            Resources.Amount = CellEnvironmentValues.RandomResources(EnvironmentTypes.Fertilizer);
        }
        public void SetMax()
        {
            Resources.Amount = CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer);
        }
    }
}