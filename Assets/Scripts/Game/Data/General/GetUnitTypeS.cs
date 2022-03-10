//namespace Chessy.Game.System.Model
//{
//    public sealed class GetUnitTypeS : SystemAbstract, IEcsRunSystem
//    {
//        internal GetUnitTypeS(in EntitiesModel eM) : base(eM) { }

//        public void Run()
//        {
//            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
//            {
//                if (E.UnitTC(idx_0).HaveUnit)
//                {
//                    var isMelee = true;

//                    if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
//                    {
//                        if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
//                        {
//                            isMelee = false;
//                        }
//                    }
//                    else
//                    {
//                        switch (E.UnitTC(idx_0).Unit)
//                        {
//                            case UnitTypes.Elfemale:
//                                isMelee = false;
//                                break;

//                            case UnitTypes.Snowy:
//                                isMelee = false;
//                                break;

//                            case UnitTypes.Undead:
//                                break;

//                            case UnitTypes.Hell:
//                                break;

//                            default:
//                                break;
//                        }
//                    }

//                    E.UnitMainE(idx_0).IsMelee = isMelee;
//                }
//            }
//        }
//    }
//}