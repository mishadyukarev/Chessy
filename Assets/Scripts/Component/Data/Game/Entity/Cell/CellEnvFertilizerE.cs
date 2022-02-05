using ECS;

namespace Game.Game
{
    public sealed class CellEnvFertilizerE : CellEnvironmentE
    {

        internal CellEnvFertilizerE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Fertilizer, ResourceTypes.Food, idx, world)
        {
        }

        public void AddAfterBuildingFarm() => Add(CellEnvironmentValues.AddingAfterBuildingFarm);
        public void ExtractFarm(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE.OwnerC.Player).AddFarmExtractFertilize(this, buildUpgEs, cellEs.BuildEs);

            Take(AmountExtractBuilding(buildUpgEs, cellEs.BuildEs));
        }
        public void AddFromIceWall() => Add(CellEnvironmentValues.AddingFromIceWall(EnvT));
        public void AddFromNearRiver() => Add(100);
        public void AddFromCloud() => Add(100);
        public void TakeDry() => Take(10);
    }
}