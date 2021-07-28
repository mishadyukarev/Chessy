using Assets.Scripts.Abstractions.Enums;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems
{
    internal class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellUnitsDataWorker.HaveAnyUnit(xy))
                    {
                        var unitType = CellUnitsDataWorker.UnitType(xy);

                        var standartX = CellWorker.GetEulerAngle(XyzTypes.X, xy);
                        var standartY = CellWorker.GetEulerAngle(XyzTypes.Y, xy);
                        var standartZ = CellWorker.GetEulerAngle(XyzTypes.Z, xy);

                        if (CellUnitsDataWorker.HaveOwner(xy))
                        {
                            if (CellUnitsDataWorker.IsMine(xy))
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
