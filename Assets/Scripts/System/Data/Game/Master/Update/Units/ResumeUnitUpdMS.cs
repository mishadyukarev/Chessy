namespace Game.Game
{
    sealed class ResumeUnitUpdMS : SystemAbstract, IEcsRunSystem
    {
        public ResumeUnitUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEsWorker.Idxs)
            {
                //var unit_0 = UnitEs(idx_0).Main.UnitC;
                //ref var condUnit_0 = UnitEs(idx_0).Main.ConditionTC;

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
