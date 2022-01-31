using ECS;
using System;

namespace Game.Game
{
    public sealed class CellEnvAdultForestE : CellEnvironmentE
    {
        public int AmountExtractWoodcutter(in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
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

        public int AmountExtractPawn(in CellUnitEs unitEs)
        {
            var ration = 0f;

            switch (unitEs.Main(Idx).LevelTC.Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            var extract = (int)(MaxResources * ration);

            if (extract > Resources.Amount) extract = Resources.Amount;

            return extract;
        }


        public CellEnvAdultForestE(in byte idx, in EcsWorld world) : base(EnvironmentTypes.AdultForest, ResourceTypes.Wood, idx, world)
        {
        }

        public void Destroy(in CellTrailE[] trailEs, in WhereEnviromentEs whereEnviromentEs)
        {
            base.Destroy(whereEnviromentEs);
            foreach (var trailE in trailEs) trailE.Destroy();
        }
        public void Fire()
        {
            ResourcesRef.Amount = CellEnvironmentValues.MaxResources(EnvT) / 2;
        }
        public void ExtractWoodcutter(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.UnitEs.Main(Idx).OwnerC.Player).AddWoodcutterExtractAdultForest(this, buildUpgEs, cellEs.BuildEs);

            ResourcesRef.Amount -= AmountExtractWoodcutter(buildUpgEs, cellEs.BuildEs);
        }
        public void ExtractPawn(in CellUnitEs unitEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, unitEs.Main(Idx).OwnerC.Player).AddPawnExtractAdultForest(unitEs, this);
            ResourcesRef.Amount -= AmountExtractPawn(unitEs);
        }
    }
}