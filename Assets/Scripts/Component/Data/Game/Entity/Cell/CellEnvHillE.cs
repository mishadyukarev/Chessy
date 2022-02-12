using ECS;

namespace Game.Game
{
    public sealed class CellEnvHillE : CellEnvironmentE
    {
        public bool CanExtractPawn(in CellUnitEs unitEs, in CellEnvironmentEs envEs)
        {
            return unitEs.TypeE.Is(UnitTypes.Pawn) && unitEs.ConditionE.Is(ConditionUnitTypes.Relaxed)
                && unitEs.ExtraToolWeaponE.Is(ToolWeaponTypes.Pick)
                && HaveEnvironment && !envEs.AdultForest.HaveEnvironment;
        }
        public int AmountExtractPawnPick()
        {
            return 10;
        }
        public int AmountExtractCity()
        {
            return 10;
        }

        internal CellEnvHillE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.Hill, ResourceTypes.Ore, idx, world)
        {
        }

        public void AddEveryMove() => Add();
        public void ExtractMine(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE.OwnerC.Player).AddFarmExtractHill(this, buildUpgEs, cellEs.BuildEs);

            Take(AmountExtractBuilding(buildUpgEs, cellEs.BuildEs)); ;
        }
        public void ExtractPawnPick(in CellUnitEs unitEs, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractPawnPick();

            invResEs.Resource(ResourceT, unitEs.OwnerE.OwnerC.Player).Add(extract);
            Take(extract);
        }
        public void ExtractCity(in CellEs cellEs_from, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractCity();

            invResEs.Resource(ResourceT, cellEs_from.BuildEs.BuildingE.OwnerC.Player).Add(extract);
            Take(extract);
        }
        public void AddFromMountain() => Add(10);
    }
}