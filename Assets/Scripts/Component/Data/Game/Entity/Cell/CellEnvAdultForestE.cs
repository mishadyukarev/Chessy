using ECS;

namespace Game.Game
{
    public sealed class CellEnvAdultForestE : CellEnvironmentE
    {
        public int AmountExtractPawn(in CellUnitEs unitEs)
        {
            var extract = (int)(MaxResources * CellEnvironmentValues.RatioExtractPawnFromMaxResource(unitEs.LevelE.LevelTC.Level, EnvT));

            if (extract > ResourcesC.Amount) extract = ResourcesC.Amount;

            return extract;
        }

        public bool CanExtractPawn(in CellUnitEs unitEs)
        {
            if (HaveEnvironment
                && unitEs.TypeE.UnitTC.Is(UnitTypes.Pawn)
                && unitEs.ConditionE.ConditionTC.Is(ConditionUnitTypes.Relaxed)
                && unitEs.StatEs.Hp.HaveMax)
            {
                return true;
            }
            else return false;
        }

        public bool CanExtractWoodcutter(in CellBuildEs buildEs)
        {
            if (buildEs.BuildingE.HaveBuilding
                && buildEs.BuildingE.BuildTC.Is(BuildingTypes.Woodcutter)
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
        public void ExtractWoodcutter(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.BuildEs.BuildingE.OwnerC.Player).AddWoodcutterExtractAdultForest(this, buildUpgEs, cellEs.BuildEs);

            Take(AmountExtractBuilding(buildUpgEs, cellEs.BuildEs));
            if (!HaveEnvironment) cellEs.TrailEs.DestroyAll();
        }
        public void ExtractPawn(in CellUnitEs unitEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, unitEs.OwnerE.OwnerC.Player).AddPawnExtractAdultForest(unitEs, this);
            Take(AmountExtractPawn(unitEs));
        }
    }
}