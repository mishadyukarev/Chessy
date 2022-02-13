using ECS;

namespace Game.Game
{
    public sealed class CellEnvHillE : CellEnvironmentE
    {
        public bool CanExtractPawn(in CellUnitEs unitEs, in CellEnvironmentEs envEs)
        {
            return unitEs.UnitE.Is(UnitTypes.Pawn) && unitEs.UnitE.Is(ConditionUnitTypes.Relaxed)
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
        public void ExtractPawnPick(in CellUnitE unitE, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractPawnPick();

            invResEs.Resource(Resource, unitE.Owner).Add(extract);
            Take(extract);
        }
        public void ExtractCity(in CellEs cellEs_from, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractCity();

            invResEs.Resource(Resource, cellEs_from.BuildEs.BuildingE.Owner).Add(extract);
            Take(extract);
        }
        public void AddFromMountain() => Add(10);
    }
}