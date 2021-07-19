using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Static.CellBaseOperations;

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
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                    {
                        var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;

                        var standartX = _eGM.CellEnt_CellBaseCom(x, y).GetEulerAngle(XyzTypes.X);
                        var standartY = _eGM.CellEnt_CellBaseCom(x, y).GetEulerAngle(XyzTypes.Y);
                        var standartZ = _eGM.CellEnt_CellBaseCom(x, y).GetEulerAngle(XyzTypes.Z);

                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMine)
                            {
                                if (_eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected).Compare(new int[] { x, y }))
                                {
                                    switch (unitType)
                                    {
                                        case UnitTypes.None:
                                            break;

                                        case UnitTypes.King:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                            break;

                                        case UnitTypes.Pawn:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                            break;

                                        case UnitTypes.PawnSword:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                            break;

                                        case UnitTypes.Rook:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ - 90);
                                            break;

                                        case UnitTypes.RookCrossbow:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ - 90);
                                            break;

                                        case UnitTypes.Bishop:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                            break;

                                        case UnitTypes.BishopCrossbow:
                                            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
                                            break;

                                        default:
                                            break;
                                    }
                                }

                                else
                                {
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(false, unitType, XyTypes.X);

                                    _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ);
                                }
                            }
                        }
                    }
                }
        }
    }
}
