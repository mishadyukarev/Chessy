﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct ExtractUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var buil_0 = ref Build<BuildingTC>(idx_0);


                if (Unit<UnitCellEC>(idx_0).CanExtract(out var extract, out var env, out var res))
                {
                    InventorResourcesE.Resource<AmountC>(res, ownUnit_0.Player).Amount += extract;
                    Environment<AmountC>(env, idx_0).Amount -= extract;

                    if (env == EnvTypes.AdultForest)
                    {
                        if (Environment<AmountC>(env, idx_0).Have)
                        {
                            if (buil_0.Is(BuildingTypes.Camp) || !buil_0.Have)
                            {
                                CellBuildE.SetNew(BuildingTypes.Woodcutter, ownUnit_0.Player, idx_0);
                            }

                            else if (!buil_0.Is(BuildingTypes.Woodcutter))
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }

                        else
                        {
                            CellBuildE.Remove(idx_0);
                            Remove(env, idx_0);

                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                SetNew(EnvTypes.YoungForest, idx_0);
                            }
                        }
                    }
                }
                else if (!Unit<UnitCellEC>(idx_0).CanResume(out extract, out env))
                {
                    if (CellUnitHpEs.HaveMax(idx_0))
                    {
                        if (unit_0.Have && CellUnitStepEs.HaveMin(idx_0))
                        {
                            condUnit_0.Condition = ConditionUnitTypes.Protected;
                        }
                    }
                }
            }
        }
    }
}