using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems
{
    internal class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellUnitsDataContainer.HaveAnyUnit(xy))
                    {
                        var unitType = CellUnitsDataContainer.UnitType(xy);

                        var standartX = CellViewContainer.GetEulerAngle(XyzTypes.X, xy);
                        var standartY = CellViewContainer.GetEulerAngle(XyzTypes.Y, xy);
                        var standartZ = CellViewContainer.GetEulerAngle(XyzTypes.Z, xy);

                        if (CellUnitsDataContainer.HaveOwner(xy))
                        {
                            if (CellUnitsDataContainer.IsMine(xy))
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
