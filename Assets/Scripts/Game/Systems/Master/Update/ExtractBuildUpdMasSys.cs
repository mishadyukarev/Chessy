using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class ExtractBuildUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC> _cellUnitStatFilt = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, UnitEffectsC> _cellUnitOthFilt = default;
        private EcsFilter<CellUnitDataCom, ToolWeaponC> _cellUnitTWFilt = default;

        private EcsFilter<CellEnvDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellbuildFilt = default;

        public void Run()
        {

            //if (builC_0.HaveBuild)
            //{
            //    if (builC_0.Is(BuildTypes.Farm))
            //    {
            //        minus = UpgBuildsC.GetExtractOneBuild(ownUnitC_0.Owner, BuildTypes.Farm);

            //        envrC_0.TakeAmountRes(EnvTypes.Fertilizer, minus);
            //        InventResC.AddAmountRes(ownUnitC_0.Owner, ResTypes.Food, minus);

            //        if (!envrC_0.HaveRes(EnvTypes.Fertilizer))
            //        {
            //            envrC_0.Reset(EnvTypes.Fertilizer);
            //            WhereEnvC.Remove(EnvTypes.Fertilizer, idx_0);

            //            WhereBuildsC.Remove(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
            //            builC_0.NoneBuild();
            //        }
            //    }

            //    else if (builC_0.Is(BuildTypes.Woodcutter))
            //    {
            //        minus = UpgBuildsC.GetExtractOneBuild(ownUnitC_0.Owner, BuildTypes.Woodcutter);

            //        envrC_0.TakeAmountRes(EnvTypes.AdultForest, minus);
            //        InventResC.AddAmountRes(ownUnitC_0.Owner, ResTypes.Wood, minus);

            //        if (!envrC_0.HaveRes(EnvTypes.AdultForest))
            //        {
            //            envrC_0.Reset(EnvTypes.AdultForest);
            //            WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);

            //            SpawnNewSeed(idx_0);

            //            WhereBuildsC.Remove(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
            //            builC_0.NoneBuild();

            //            if (fireC_0.HaveFire)
            //            {
            //                fireC_0.HaveFire = false;
            //            }
            //        }
            //    }

            //    else if (builC_0.Is(BuildTypes.Mine))
            //    {
            //        minus = UpgBuildsC.GetExtractOneBuild(ownUnitC_0.Owner, BuildTypes.Mine);

            //        envrC_0.TakeAmountRes(EnvTypes.Hill, minus);
            //        InventResC.AddAmountRes(ownUnitC_0.Owner, ResTypes.Ore, minus);

            //        if (!envrC_0.HaveRes(EnvTypes.Hill))
            //        {
            //            WhereBuildsC.Remove(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
            //            builC_0.NoneBuild();
            //        }
            //    }
            //}
        }
    }
}