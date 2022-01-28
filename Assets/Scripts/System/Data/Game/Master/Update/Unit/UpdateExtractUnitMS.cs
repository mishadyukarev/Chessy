using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpdateExtractUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Entities.CellEs.Idxs)
            {
                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;
                ref var condUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;

                ref var buil_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;


                if (Entities.CellEs.UnitEs.CanExtract(idx_0, out var resume, out var env, out var res))
                {
                    InventorResourcesE.Resource(res, ownUnit_0.Player).Amount += resume;
                    Entities.CellEs.EnvironmentEs.Environment(env, idx_0).Resources.Amount -= resume;

                    if (env == EnvironmentTypes.AdultForest)
                    {
                        if (Entities.CellEs.EnvironmentEs.Environment(env, idx_0).Resources.Have)
                        {
                            if (buil_0.Is(BuildingTypes.Camp) || !buil_0.Have)
                            {
                                Entities.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.Woodcutter, ownUnit_0.Player);
                                Entities.WhereBuildingEs.HaveBuild(BuildingTypes.Woodcutter, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                            }

                            else if (!buil_0.Is(BuildingTypes.Woodcutter))
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }

                        else
                        {
                            Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                            Entities.CellEs.BuildEs.Build(idx_0).Remove();
                            Entities.CellEs.EnvironmentEs.Environment(env, idx_0).Remove();

                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).SetNew();
                            }
                        }
                    }
                }
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