using ECS;

namespace Game.Game
{
    public sealed class CellEnvFertilizerE : CellEnvironmentE
    {
        public int AmountExtractFarm(in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            var extract = 10;

            if (buildUpgEs.HaveUpgrade(buildEs.BuildingE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                extract += (int)(extract * 0.5f);
            }

            if (extract > ResourcesC.Amount)
                extract = ResourcesC.Amount;

            return extract;
        }


        internal CellEnvFertilizerE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Fertilizer, ResourceTypes.Food, idx, world)
        {
        }

        public void AddAfterBuildingFarm() => Add(CellEnvironmentValues.AddingAfterBuildingFarm);
        public void ExtractFarm(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE.OwnerC.Player).AddFarmExtractFertilize(this, buildUpgEs, cellEs.BuildEs);

            Take(AmountExtractFarm(buildUpgEs, cellEs.BuildEs));
        }
        public void AddFromIceWall() => Add(CellEnvironmentValues.AddingFromIceWall(EnvT));
    }
}