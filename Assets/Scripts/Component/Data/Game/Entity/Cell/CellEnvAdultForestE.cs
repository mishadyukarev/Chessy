using ECS;

namespace Game.Game
{
    public sealed class CellEnvAdultForestE : CellEnvironmentE
    {
        public int AmountExtractPawn(in CellUnitE unitE)
        {
            var extract = (int)(MaxResources * CellEnvironmentValues.RatioExtractPawnFromMaxResource(unitE.Level, EnvT));

            if (extract > ResourcesC.Amount) extract = ResourcesC.Amount;

            return extract;
        }

        public bool CanExtractPawn(in CellUnitEs unitEs)
        {
            if (HaveEnvironment
                && unitEs.UnitE.Is(UnitTypes.Pawn)
                && unitEs.UnitE.Is(ConditionUnitTypes.Relaxed)
                && unitEs.UnitE.HaveMaxHp)
            {
                return true;
            }
            else return false;
        }

        public bool CanExtractWoodcutter(in CellBuildingEs buildEs)
        {
            if (buildEs.BuildingE.HaveBuilding
                && buildEs.BuildingE.Is(BuildingTypes.Woodcutter)
                && HaveEnvironment) return true;
            else return false;
        }


        internal CellEnvAdultForestE(in byte idx, in EcsWorld gameW) : base(EnvironmentTypes.AdultForest, ResourceTypes.Wood, idx, gameW)
        {
        }

        public void Destroy(in CellTrailE[] trailEs)
        {
            Destroy();
            foreach (var trailE in trailEs) trailE.Destroy();
        }
        public void Fire() => Take(CellEnvironmentValues.FireAdultForest);
        public void ExtractPawn(in CellUnitE unitE, in InventorResourcesEs invResEs)
        {
            var extract = AmountExtractPawn(unitE);

            invResEs.Resource(Resource, unitE.Owner).Add(extract);
            Take(extract);
        }
    }
}