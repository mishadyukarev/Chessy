using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Static.CellBaseOperations;
using static Assets.Scripts.CellUnitWorker;
using static Assets.Scripts.CellWorker;

namespace Assets.Scripts.ECS.Game.General.Systems
{
    internal class FliperAndRotatorUnitSystem : SystemGeneralReduction
    {
        public override void Run()
        {
            base.Run();

            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (HaveAnyUnitOnCell(xy))
                    {
                        var unitType = UnitType(xy);

                        var standartX = GetEulerAngle(XyzTypes.X, xy);
                        var standartY = GetEulerAngle(XyzTypes.Y, xy);
                        var standartZ = GetEulerAngle(XyzTypes.Z, xy);

                        if (HaveOwner(xy))
                        {
                            if (IsMine(xy))
                            {
                                //if (_eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected).Compare(new int[] { x, y }))
                                //{
                                //    switch (unitType)
                                //    {
                                //        case UnitTypes.None:
                                //            break;

                                //        case UnitTypes.King:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                //            break;

                                //        case UnitTypes.Pawn:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                //            break;

                                //        case UnitTypes.PawnSword:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                //            break;

                                //        case UnitTypes.Rook:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ - 90);
                                //            break;

                                //        case UnitTypes.RookCrossbow:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ - 90);
                                //            break;

                                //        case UnitTypes.Bishop:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                //            break;

                                //        case UnitTypes.BishopCrossbow:
                                //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                //            break;

                                //        default:
                                //            break;
                                //    }
                                //}

                                //else
                                //{
                                //    _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(false, unitType, XyTypes.X);

                                //    _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ);
                                //}
                            }
                        }
                    }
                }
        }
    }
}
