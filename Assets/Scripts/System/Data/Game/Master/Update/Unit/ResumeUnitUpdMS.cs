using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct ResumeUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var condUnit_0 = ref CellUnitEntities.Else(idx_0).ConditionC;

                //if (Unit<UnitCellEC>(idx_0).CanResume(out var resume, out var env))
                //{
                //    if (Environment<AmountC>(env, idx_0).Amount == Max(env))
                //    {
                //        condUnit_0.Condition = ConditionUnitTypes.Protected;
                //    }
                //    else
                //    {
                //        Environment<AmountC>(env, idx_0).Amount += resume;
                //    }
                //}
                //else if (!Unit<UnitCellEC>(idx_0).CanExtract(out resume, out env, out var res))
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
