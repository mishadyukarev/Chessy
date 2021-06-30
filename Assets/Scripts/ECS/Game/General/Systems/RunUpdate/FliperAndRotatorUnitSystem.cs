using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.General.Systems
{
    internal class FliperAndRotatorUnitSystem : SystemGeneralReduction
    {
        public override void Init()
        {
            base.Init();


        }

        public override void Run()
        {
            base.Run();

            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner && _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMine)
                        {
                            var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;

                            var standartX = _eGM.CellUnitEnt_CellUnitCom(x, y).StandartX;
                            var standartY = _eGM.CellUnitEnt_CellUnitCom(x, y).StandartY;
                            var standartZ = _eGM.CellUnitEnt_CellUnitCom(x, y).StandartZ;

                            if (_cellM.CellBaseOperations.CompareXY(_eGM.SelectorEnt_SelectorCom.XySelectedCell, new int[] { x, y }))
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
