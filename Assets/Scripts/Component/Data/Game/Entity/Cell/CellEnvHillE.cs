using ECS;

namespace Game.Game
{
    public sealed class CellEnvHillE : CellEnvironmentE
    {
        public bool CanExtractMine(in CellBuildEs buildEs)
        {
            if (buildEs.BuildingE.HaveBuilding && buildEs.BuildingE.BuildTC.Is(BuildingTypes.Mine)
                && ResourcesC.Amount > 1) return true;
            else return false;
        }


        internal CellEnvHillE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Hill, ResourceTypes.Ore, idx, world)
        {
        }
        public void AddEveryMove() => Add();
        public void ExtractMine(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE.OwnerC.Player).AddFarmExtractHill(this, buildUpgEs, cellEs.BuildEs);

            Take(AmountExtractBuilding(buildUpgEs, cellEs.BuildEs));;
        }
        public void ExtractPawn() => Take(10);
    }
}