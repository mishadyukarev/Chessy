using ECS;

namespace Game.Game
{
    public sealed class CellEnvFertilizerE : CellEnvironmentE
    {
        public int AmountExtractFarm(in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            var extract = 10;

            if (buildUpgEs.HaveUpgrade(buildEs.BuildingE(Idx), UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                extract += (int)(extract * 0.5f);
            }

            if (extract > Resources.Amount)
                extract = Resources.Amount;

            return extract;
        }


        public CellEnvFertilizerE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Fertilizer, ResourceTypes.Food, idx, world)
        {
        }

        public void SetNew()
        {
            ResourcesRef.Amount = CellEnvironmentValues.RandomResources(EnvironmentTypes.Fertilizer);
        }
        public void SetMax()
        {
            ResourcesRef.Amount = CellEnvironmentValues.MaxResources(EnvironmentTypes.Fertilizer);
        }
        public void ExtractFarm(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE(Idx).Owner.Player).AddFarmExtractFertilize(this, buildUpgEs, cellEs.BuildEs);

            ResourcesRef.Amount -= AmountExtractFarm(buildUpgEs, cellEs.BuildEs);
        }
    }
}