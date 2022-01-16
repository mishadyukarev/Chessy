using static Game.Game.CellEs;
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
                ref var buil_0 = ref Build<BuildingC>(idx_0);


                if (Unit<UnitCellEC>(idx_0).CanExtract(out var extract, out var env, out var res))
                {
                    EntInventorResources.Resource<AmountC>(res, ownUnit_0.Player).Amount += extract;
                    Environment<AmountResourcesC>(env, idx_0).Resources -= extract;

                    if (env == EnvTypes.AdultForest)
                    {
                        if (Environment<AmountResourcesC>(env, idx_0).Have)
                        {
                            if (buil_0.Is(BuildTypes.Camp) || !buil_0.Have)
                            {
                                buildCell_0.SetNew(BuildTypes.Woodcutter, ownUnit_0.Player);
                            }

                            else if (!buil_0.Is(BuildTypes.Woodcutter))
                            {
                                condUnit_0.Set(ConditionUnitTypes.Protected);
                            }
                        }

                        else
                        {
                            buildCell_0.Remove();
                            Environment<EnvCellEC>(env, idx_0).Remove();

                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).SetNew();
                            }
                        }
                    }
                }
                else if (!Unit<UnitCellEC>(idx_0).CanResume(out extract, out env))
                {
                    if (Unit<UnitCellEC>(idx_0).HaveMax)
                    {
                        if (unit_0.Have && Unit<UnitCellEC>(idx_0).HaveMin)
                        {
                            condUnit_0.Set(ConditionUnitTypes.Protected);
                        }
                    }
                }
            }
        }
    }
}