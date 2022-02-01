using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    sealed class UpdExtractMineMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdExtractMineMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            //for (byte idx_0 = 0; idx_0 < CellEs.Count; idx_0++)
            //{
            //    if (BuildEs.BuildingE(idx_0).CanExtractFertilizer(EnvironmentEs))
            //    {
            //        EnvironmentEs.Fertilizer(idx_0).ExtractFarm(CellEs, Es.BuildingUpgradeEs, Es.InventorResourcesEs);

            //        if (!EnvironmentEs.Fertilizer(idx_0).HaveEnvironment)
            //        {
            //            BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);
            //        }
            //    }
            //}
        }
    }
}