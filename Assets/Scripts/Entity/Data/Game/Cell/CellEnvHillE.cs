using ECS;

namespace Game.Game
{
    public sealed class CellEnvHillE : CellEnvironmentE
    {
        public bool CanExtractPawn(in CellUnitEs unitEs, in CellEnvironmentEs envEs)
        {
            return unitEs.UnitE.UnitTC.Is(UnitTypes.Pawn) && unitEs.UnitE.ConditionTC.Is(ConditionUnitTypes.Relaxed)
                && unitEs.ExtraToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Pick)
                && HaveEnvironment && !envEs.AdultForest.HaveEnvironment;
        }
        public float AmountExtractPawnPick()
        {
            return 0.1f;
        }
        public float AmountExtractCity()
        {
            return 0.1f;
        }

        internal CellEnvHillE(in CellEs cellEs, in EcsWorld world) : base(EnvironmentTypes.Hill, ResourceTypes.Ore, cellEs, world)
        {
        }

        public void ExtractPawnPick(in CellUnitE unitE, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractPawnPick();

            invResEs.Resource(Resource, unitE.PlayerTC.Player).ResourceC.Add(extract);
            Take(extract);
        }
        public void ExtractCity(in CellEs cellEs_from, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractCity();

            invResEs.Resource(Resource, cellEs_from.BuildEs.BuildingE.Owner).ResourceC.Add(extract);
            Take(extract);
        }
        public void AddFromMountain() => Add(0.1f);
    }
}