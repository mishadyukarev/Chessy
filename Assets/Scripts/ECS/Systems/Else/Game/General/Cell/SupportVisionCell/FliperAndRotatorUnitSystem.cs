using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems
{
    internal class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        public void Run()
        {
            //for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            //    for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            //    {
            //        var xy = new int[] { x, y };

            //        if (CellUnitsDataSystem.HaveAnyUnit(xy))
            //        {
            //            var unitType = CellUnitsDataSystem.UnitType(xy);

            //            var standartX = CellViewSystem.GetEulerAngle(XyzTypes.X, xy);
            //            var standartY = CellViewSystem.GetEulerAngle(XyzTypes.Y, xy);
            //            var standartZ = CellViewSystem.GetEulerAngle(XyzTypes.Z, xy);

            //            if (CellUnitsDataSystem.HaveOwner(xy))
            //            {
            //                if (CellUnitsDataSystem.IsMine(xy))
            //                {
            //                    //if (_eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected).Compare(new int[] { x, y }))
            //                    //{
            //                    //    switch (unitType)
            //                    //    {
            //                    //        case UnitTypes.None:
            //                    //            break;

            //                    //        case UnitTypes.King:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
            //                    //            break;

            //                    //        case UnitTypes.Pawn:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
            //                    //            break;

            //                    //        case UnitTypes.PawnSword:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
            //                    //            break;

            //                    //        case UnitTypes.Rook:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ - 90);
            //                    //            break;

            //                    //        case UnitTypes.RookCrossbow:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ - 90);
            //                    //            break;

            //                    //        case UnitTypes.Bishop:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
            //                    //            break;

            //                    //        case UnitTypes.BishopCrossbow:
            //                    //            _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(true, unitType, XyTypes.X);
            //                    //            break;

            //                    //        default:
            //                    //            break;
            //                    //    }
            //                    //}

            //                    //else
            //                    //{
            //                    //    _eGM.CellUnitEnt_CellUnitCom(x, y).Flip(false, unitType, XyTypes.X);

            //                    //    _eGM.CellUnitEnt_CellUnitCom(x, y).SetRotation(unitType, 0, 0, standartZ);
            //                    //}
            //                }
            //            }
            //        }
            //    }
        }
    }
}
