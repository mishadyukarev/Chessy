//using Chessy.Game.Values;

//namespace Chessy.Game
//{
//    sealed class ResumeUnitUpdMS : SystemModelGameAbs, IEcsRunSystem
//    {
//        public ResumeUnitUpdMS(in Chessy.Game.Model.Entity.EntitiesModelGame ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//             for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
//            {
//                //var unit_0 = UnitEs(cell_0).Main.UnitC;
//                //ref var condUnit_0 = UnitEs(cell_0).Main.ConditionTC;

//                //if (Unit<UnitCellEC>(cell_0).CanResume(out var resume, out var env))
//                //{
//                //    if (Environment<AmountC>(env, cell_0).Amount == Max(env))
//                //    {
//                //        condUnit_0.Condition = ConditionUnitTypes.Protected;
//                //    }
//                //    else
//                //    {
//                //        Environment<AmountC>(env, cell_0).Amount += resume;
//                //    }
//                //}
//                //else if (!Unit<UnitCellEC>(cell_0).CanExtract(out resume, out env, out var res))
//                //{
//                //    if (EntPool.CellUnitHpEs.HaveMax(cell_0))
//                //    {
//                //        if (unit_0.Have && EntitiesPool.CellUnitStepEs.HaveMin(cell_0))
//                //        {
//                //            condUnit_0.Condition = ConditionUnitTypes.Protected;
//                //        }
//                //    }
//                //}
//            }
//        }
//    }
//}
