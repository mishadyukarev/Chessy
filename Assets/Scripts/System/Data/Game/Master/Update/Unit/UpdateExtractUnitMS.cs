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
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;
                ref var condUnit_0 = ref CellUnitEs.Else(idx_0).ConditionC;

                ref var buil_0 = ref CellBuildEs.Build(idx_0).BuildTC;


                if (CellUnitEs.CanExtract(idx_0, out var resume, out var env, out var res))
                {
                    InventorResourcesE.Resource(res, ownUnit_0.Player).Amount += resume;
                    Environment(env, idx_0).Resources.Amount -= resume;

                    if (env == EnvironmentTypes.AdultForest)
                    {
                        if (Environment(env, idx_0).Resources.Have)
                        {
                            if (buil_0.Is(BuildingTypes.Camp) || !buil_0.Have)
                            {
                                CellBuildEs.SetNew(BuildingTypes.Woodcutter, ownUnit_0.Player, idx_0);
                            }

                            else if (!buil_0.Is(BuildingTypes.Woodcutter))
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }

                        else
                        {
                            CellBuildEs.Remove(idx_0);
                            Remove(env, idx_0);

                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                SetNew(EnvironmentTypes.YoungForest, idx_0);
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