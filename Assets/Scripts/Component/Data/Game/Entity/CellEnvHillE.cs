using ECS;

namespace Game.Game
{
    public sealed class CellEnvHillE : CellEnvironmentE
    {
        public int AmountExtractMine(in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            var extract = 10;

            if (buildUpgEs.HaveUpgrade(buildEs.BuildingE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                extract += (int)(extract * 0.5f);
            }

            if (extract > Resources.Amount)
                extract = Resources.Amount;

            return extract;
        }

        public CellEnvHillE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Hill, ResourceTypes.Ore, idx, world)
        {
        }
        public void AddEveryMove()
        {
            ResourcesRef.Amount++;
        }
        public void ExtractMine(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE.OwnerC.Player).AddFarmExtractHill(this, buildUpgEs, cellEs.BuildEs);


            ResourcesRef.Amount -= AmountExtractMine(buildUpgEs, cellEs.BuildEs);
        }
    }
}