using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers
{
    internal class SelectorWorker : MainGeneralWorker
    {
        internal static bool HaveAnySelectorUnit => EGGM.SelectorEnt_UnitTypeCom.UnitType != UnitTypes.None;
        internal static UnitTypes SelectorUnitType => EGGM.SelectorEnt_UnitTypeCom.UnitType;

        internal static void SetXy(SelectorCellTypes selectorCellType, params int[] xy)
        {
            switch (selectorCellType)
            {
                case SelectorCellTypes.None:
                    throw new Exception();

                case SelectorCellTypes.Current:
                    EGGM.XyCurrentCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                case SelectorCellTypes.Selected:
                    EGGM.XySelectedCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                case SelectorCellTypes.Previous:
                    EGGM.XyPreviousCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                case SelectorCellTypes.PreviousVision:
                    EGGM.XyPreviousVisionCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static int[] GetXy(SelectorCellTypes selectorCellType)
        {
            switch (selectorCellType)
            {
                case SelectorCellTypes.None:
                    throw new Exception();

                case SelectorCellTypes.Current:
                    return EGGM.XyCurrentCellEnt_XyCellCom.XyCell;

                case SelectorCellTypes.Selected:
                    return EGGM.XySelectedCellEnt_XyCellCom.XyCell;

                case SelectorCellTypes.Previous:
                    return EGGM.XyPreviousCellEnt_XyCellCom.XyCell;

                case SelectorCellTypes.PreviousVision:
                    return EGGM.XyPreviousVisionCellEnt_XyCellCom.XyCell;

                default:
                    throw new Exception();
            }
        }

        internal static List<int[]> GetAllCells(AvailableCellTypes availableCellType)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    return EGGM.AvailableCellsSettingEnt_AvailCellsCom.AvailableCells;

                case AvailableCellTypes.Shift:
                    return EGGM.AvailableCellsShiftEnt_AvailCellsCom.AvailableCells;

                case AvailableCellTypes.SimpleAttack:
                    return EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.AvailableCells;

                case AvailableCellTypes.UniqueAttack:
                    return EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.AvailableCells;

                default:
                    throw new Exception();
            }
        }
        internal static void SetAllCells(AvailableCellTypes availableCellType, List<int[]> list)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    EGGM.AvailableCellsSettingEnt_AvailCellsCom.SetList(list);
                    break;

                case AvailableCellTypes.Shift:
                    EGGM.AvailableCellsShiftEnt_AvailCellsCom.SetList(list);
                    break;

                case AvailableCellTypes.SimpleAttack:
                    EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.SetList(list);
                    break;

                case AvailableCellTypes.UniqueAttack:
                    EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.SetList(list);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ClearAvailableCells(AvailableCellTypes availableCellType)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    EGGM.AvailableCellsSettingEnt_AvailCellsCom.Clear();
                    break;

                case AvailableCellTypes.Shift:
                    EGGM.AvailableCellsShiftEnt_AvailCellsCom.Clear();
                    break;

                case AvailableCellTypes.SimpleAttack:
                    EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.Clear();
                    break;

                case AvailableCellTypes.UniqueAttack:
                    EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.Clear();
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddAvailableCell(AvailableCellTypes availableCellType, int[] xy)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    EGGM.AvailableCellsSettingEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                case AvailableCellTypes.Shift:
                    EGGM.AvailableCellsShiftEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                case AvailableCellTypes.SimpleAttack:
                    EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                case AvailableCellTypes.UniqueAttack:
                    EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static int[] GetCellByIndex(AvailableCellTypes availableCellType, int index)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    return EGGM.AvailableCellsSettingEnt_AvailCellsCom.GetCellByIndex(index);

                case AvailableCellTypes.Shift:
                    return EGGM.AvailableCellsShiftEnt_AvailCellsCom.GetCellByIndex(index);

                case AvailableCellTypes.SimpleAttack:
                    return EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.GetCellByIndex(index);

                case AvailableCellTypes.UniqueAttack:
                    return EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.GetCellByIndex(index);

                default:
                    throw new Exception();
            }
        }
        internal static void RemoveAt(AvailableCellTypes availableCellType, int index)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    EGGM.AvailableCellsSettingEnt_AvailCellsCom.RemoveAt(index);
                    break;

                case AvailableCellTypes.Shift:
                    EGGM.AvailableCellsShiftEnt_AvailCellsCom.RemoveAt(index);
                    break;

                case AvailableCellTypes.SimpleAttack:
                    EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.RemoveAt(index);
                    break;

                case AvailableCellTypes.UniqueAttack:
                    EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.RemoveAt(index);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static bool TryFindCell(AvailableCellTypes availableCellType, params int[] xy)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    return EGGM.AvailableCellsSettingEnt_AvailCellsCom.TryFindCell(xy);

                case AvailableCellTypes.Shift:
                    return EGGM.AvailableCellsShiftEnt_AvailCellsCom.TryFindCell(xy);

                case AvailableCellTypes.SimpleAttack:
                    return EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.TryFindCell(xy);

                case AvailableCellTypes.UniqueAttack:
                    return EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.TryFindCell(xy);

                default:
                    throw new Exception();
            }
        }
        internal static int GetAmountCells(AvailableCellTypes availableCellType)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    return EGGM.AvailableCellsSettingEnt_AvailCellsCom.CountCells;

                case AvailableCellTypes.Shift:
                    return EGGM.AvailableCellsShiftEnt_AvailCellsCom.CountCells;

                case AvailableCellTypes.SimpleAttack:
                    return EGGM.AvailableCellsSimpleAttackEnt_AvailCellsCom.CountCells;

                case AvailableCellTypes.UniqueAttack:
                    return EGGM.AvailableCellsUniqueAttackEnt_AvailCellsCom.CountCells;

                default:
                    throw new Exception();
            }
        }
    }
}