using ECS;
using System;

namespace Game.Game
{
    public sealed class CellEnvAdultForestE : CellEnvironmentE
    {
        public int AmountExtractWoodcutter(in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
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

        public int AmountExtractPawn(in CellUnitEs unitEs)
        {
            var ration = 0f;

            switch (unitEs.LevelE.LevelTC.Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            var extract = (int)(MaxResources * ration);

            if (extract > ResourcesC.Amount) extract = ResourcesC.Amount;

            return extract;
        }

        public bool CanExtractPawnAdultForest(in CellUnitEs unitEs)
        {
            if (HaveEnvironment
                && unitEs.MainE.UnitTC.Is(UnitTypes.Pawn)
                && unitEs.ConditionE.ConditionTC.Is(ConditionUnitTypes.Relaxed)
                && unitEs.StatEs.Hp.HaveMax)
            {
                return true;
            }
            else return false;
        }


        internal CellEnvAdultForestE(in byte idx, in EcsWorld gameW) : base(EnvironmentTypes.AdultForest, ResourceTypes.Wood, idx, gameW)
        {
        }

        public void AddFromIceWall() => Add(CellEnvironmentValues.AddingFromIceWall(EnvT));

        public void Destroy(in CellTrailE[] trailEs, in WhereEnviromentEs whereEnviromentEs)
        {
            Destroy(whereEnviromentEs);
            foreach (var trailE in trailEs) trailE.Destroy();
        }
        public void Fire() => Take(CellEnvironmentValues.FireAdultForest);
        public void ExtractWoodcutter(in CellEs cellEs, in BuildingUpgradeEs buildUpgEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, cellEs.UnitEs.OwnerE.OwnerC.Player).AddWoodcutterExtractAdultForest(this, buildUpgEs, cellEs.BuildEs);

            Take(AmountExtractWoodcutter(buildUpgEs, cellEs.BuildEs));
            if (!HaveEnvironment) cellEs.TrailEs.DestroyAll();
        }
        public void ExtractPawn(in CellUnitEs unitEs, in InventorResourcesEs invResEs)
        {
            invResEs.Resource(ResourceT, unitEs.OwnerE.OwnerC.Player).AddPawnExtractAdultForest(unitEs, this);
            Take(AmountExtractPawn(unitEs));
        }
    }
}