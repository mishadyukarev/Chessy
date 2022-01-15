using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.CellEnvironmentE;

namespace Game.Game
{
    struct ResumeUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);

                if (Unit<UnitCellEC>(idx_0).CanResume(out var resume, out var env))
                {
                    if (Environment<EnvCellEC>(env, idx_0).HaveMax())
                    {
                        condUnit_0.Set(ConditionUnitTypes.Protected);
                    }
                    else
                    {
                        Environment<AmountResourcesC>(env, idx_0).Resources += resume;
                    }
                }
                else if (!Unit<UnitCellEC>(idx_0).CanExtract(out resume, out env, out var res))
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
