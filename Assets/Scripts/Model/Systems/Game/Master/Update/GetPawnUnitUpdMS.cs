//namespace Chessy.Game
//{
//    sealed class GetPawnUnitUpdMS : SystemAbstract, IEcsRunSystem
//    {
//        internal GetPawnUnitUpdMS(in Chessy.Game.Model.Entity.EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
//            {
//                //if (Es.MaxPawnsE(playerT).MaxPawns > Es.Units(UnitTypes.Pawn, LevelTypes.First, playerT).AmountUnits
//                //    + Es.WhereWorker.AmountPaws(playerT))
//                //{

//                //    Es.ForNextUnitE(playerT).Steps += 0.5f;
//                //    if (Es.ForNextUnitE(playerT).CanGetUnit)
//                //    {
//                //        Es.Units(UnitTypes.Pawn, LevelTypes.First, playerT).AddUnit();
//                //        Es.ForNextUnitE(playerT).Reset();
//                //    }
//                //}

//            }
//        }
//    }
//}