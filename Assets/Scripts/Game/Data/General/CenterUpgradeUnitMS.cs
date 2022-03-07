//namespace Chessy.Game
//{
//    sealed class CenterUpgradeUnitMS : SystemAbstract, IEcsRunSystem
//    {
//        internal CenterUpgradeUnitMS(in EntitiesModel ents) : base(ents)
//        {

//        }

//        public void Run()
//        {
//            //var unit = E.RpcPoolEs.CenterUpgradeUnitE.UnitTC.Unit;

//            //if (unit != UnitTypes.None)
//            //{
//            //    var sender = E.RpcPoolEs.SenderC.Player;
//            //    var whoseMove = E.WhoseMove.Player;


//            //    if (unit == UnitTypes.Scout)
//            //    {
//            //        E.UnitInfo(whoseMove, LevelTypes.First, unit).MaxSteps += UnitStep_Values.CENTER_BONUS_SCOUT;
//            //    }

//            //    else
//            //    {


//            //        switch (unit)
//            //        {
//            //            case UnitTypes.King:
//            //                E.UnitInfo(whoseMove, LevelTypes.First, unit).HaveCenterUpgrade = true;
//            //                E.UnitInfo(whoseMove, LevelTypes.First, unit).MaxSteps += UnitStep_Values.CENTER_KING_BONUS;
//            //                break;

//            //            case UnitTypes.Pawn:
//            //                E.UnitInfo(whoseMove, LevelTypes.First, unit).HaveCenterUpgrade = true;
//            //                E.UnitInfo(whoseMove, LevelTypes.First, unit).MaxSteps += UnitStep_Values.CENTER_PAWN_BONUS;
//            //                break;

//            //            default:
//            //                break;
//            //        }

//            //    }

//            //    E.PlayerE(whoseMove).HaveFraction = false;
//            //    //E.UnitInfo(whoseMove, LevelTypes.First, unit).HaveCenterUpgrade = false;

//            //    E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickUpgrade);


//            //    E.RpcPoolEs.CenterUpgradeUnitE.UnitTC.Unit = UnitTypes.None;
//            }
//        }
//    }
//}