namespace Game.Game
{
    sealed class UpdateExtractUnitMS : SystemCellAbstract, IEcsRunSystem
    {
        public UpdateExtractUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = Es.CellEs.UnitEs;
            var envEs = Es.CellEs.EnvironmentEs;


            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
                ref var condUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;

                ref var buil_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;


                //if (unitEs.Main(idx_0).CanExtract(UnitEs.StatEs.Hp(idx_0), envEs.AdultForest( idx_0), out var resume, out var env, out var res))
                //{
                //    Es.InventorResourcesEs.Resource(res, ownUnit_0.Player).Resources.Amount += resume;
                //    Es.CellEs.EnvironmentEs.Environment(env, idx_0).Resources.Amount -= resume;

                //    if (env == EnvironmentTypes.AdultForest)
                //    {
                //        if (Es.CellEs.EnvironmentEs.Environment(env, idx_0).HaveEnvironment)
                //        {
                //            if (buil_0.Is(BuildingTypes.Camp) || !buil_0.Have)
                //            {
                //                Es.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.Woodcutter, ownUnit_0.Player);
                //                Es.WhereBuildingEs.HaveBuild(BuildingTypes.Woodcutter, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                //            }

                //            else if (!buil_0.Is(BuildingTypes.Woodcutter))
                //            {
                //                condUnit_0.Condition = ConditionUnitTypes.Protected;
                //            }
                //        }

                //        else
                //        {
                //            Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                //            Es.CellEs.BuildEs.Build(idx_0).Remove();
                //            Es.CellEs.EnvironmentEs.Environment(env, idx_0).Destroy(TrailEs.Trails(idx_0));

                //            if (UnityEngine.Random.Range(0, 100) < 50)
                //            {
                //                Es.CellEs.EnvironmentEs.YoungForest( idx_0).SetNew();
                //            }
                //        }
                //    }
                //}
                //else if (!Unit<UnitCellEC>(idx_0).CanResume(out resume, out env))
                //{
                //    if (EntPool.CellUnitHpEs.HaveMax(idx_0))
                //    {
                //        if (unit_0.Have && EntitiesPool.CellUnitStepEs.HaveMin(idx_0))
                //        {
                //            condUnit_0.Condition = ConditionUnitTypes.Protected;
                //        }
                //    }
                //}
            }
        }
    }
}