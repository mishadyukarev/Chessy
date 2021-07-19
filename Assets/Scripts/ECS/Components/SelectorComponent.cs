
using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

public struct SelectorComponent
{
    private int[] _xyCurrentCell;
    private int[] _xyPreviousCell;
    private int[] _xySelectedCell;
    private int[] _xyPreviousVisionCell;

    private List<int[]> _availableCellsForSettingUnit;
    private List<int[]> _availableCellsForShift;
    private List<int[]> _availableCellsSimpleAttack;
    private List<int[]> _availableCellsUniqueAttack;


    internal bool IsSelected { get; set; }

    internal bool CanShiftUnit { get; set; }
    internal bool CanExecuteStartClick { get; set; }
    internal bool IsStartSelectedDirect { get; set; }


    internal void StartFill()
    {
        _xyCurrentCell = new int[XY_FOR_ARRAY];
        _xyPreviousCell = new int[XY_FOR_ARRAY];
        _xySelectedCell = new int[XY_FOR_ARRAY];
        _xyPreviousVisionCell = new int[XY_FOR_ARRAY];

        _availableCellsForSettingUnit = new List<int[]>();
        _availableCellsForShift = new List<int[]>();
        _availableCellsSimpleAttack = new List<int[]>();
        _availableCellsUniqueAttack = new List<int[]>();


        IsSelected = default;

        CanShiftUnit = default;
        CanExecuteStartClick = default;
        IsStartSelectedDirect = default;
    }



    internal void SetXy(SelectorCellTypes selectorCellType, params int[] xy)
    {
        switch (selectorCellType)
        {
            case SelectorCellTypes.None:
                throw new Exception();

            case SelectorCellTypes.Current:
                _xyCurrentCell = (int[])xy.Clone();
                break;

            case SelectorCellTypes.Selected:
                _xySelectedCell = (int[])xy.Clone();
                break;

            case SelectorCellTypes.Previous:
                _xyPreviousCell = (int[])xy.Clone();
                break;

            case SelectorCellTypes.PreviousVision:
                _xyPreviousVisionCell = (int[])xy.Clone();
                break;

            default:
                throw new Exception();
        }
    }
    internal int[] GetXy(SelectorCellTypes selectorCellType)
    {
        switch (selectorCellType)
        {
            case SelectorCellTypes.None:
                throw new Exception();

            case SelectorCellTypes.Current:
                return (int[])_xyCurrentCell.Clone();

            case SelectorCellTypes.Selected:
                return (int[])_xySelectedCell.Clone();

            case SelectorCellTypes.Previous:
                return (int[])_xyPreviousCell.Clone();

            case SelectorCellTypes.PreviousVision:
                return (int[])_xyPreviousVisionCell.Clone();

            default:
                throw new Exception();
        }
    }


    internal List<int[]> GetAllCells(AvailableCellTypes availableCellType)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                return _availableCellsForSettingUnit.Copy();

            case AvailableCellTypes.Shift:
                return _availableCellsForShift.Copy();

            case AvailableCellTypes.SimpleAttack:
                return _availableCellsSimpleAttack.Copy();

            case AvailableCellTypes.UniqueAttack:
                return _availableCellsUniqueAttack.Copy();

            default:
                throw new Exception();
        }
    }
    internal void AddAvailableCell(AvailableCellTypes availableCellType, params int[] xy)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                _availableCellsForSettingUnit.Add(xy);
                break;

            case AvailableCellTypes.Shift:
                _availableCellsForShift.Add(xy);
                break;

            case AvailableCellTypes.SimpleAttack:
                _availableCellsSimpleAttack.Add(xy);
                break;

            case AvailableCellTypes.UniqueAttack:
                _availableCellsUniqueAttack.Add(xy);
                break;

            default:
                throw new Exception();
        }
    }
    internal void ClearAvailableCells(AvailableCellTypes availableCellType)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                _availableCellsForSettingUnit.Clear();
                break;

            case AvailableCellTypes.Shift:
                _availableCellsForShift.Clear();
                break;

            case AvailableCellTypes.SimpleAttack:
                _availableCellsSimpleAttack.Clear();
                break;

            case AvailableCellTypes.UniqueAttack:
                _availableCellsUniqueAttack.Clear();
                break;

            default:
                throw new Exception();
        }
    }
    internal int[] GetCellByIndex(AvailableCellTypes availableCellType, int index)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                return _availableCellsForSettingUnit[index];

            case AvailableCellTypes.Shift:
                return _availableCellsForShift[index];

            case AvailableCellTypes.SimpleAttack:
                return _availableCellsSimpleAttack[index];

            case AvailableCellTypes.UniqueAttack:
                return _availableCellsUniqueAttack[index];

            default:
                throw new Exception();
        }
    }
    internal void RemoveAt(AvailableCellTypes availableCellType, int index)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                _availableCellsForSettingUnit.RemoveAt(index);
                break;

            case AvailableCellTypes.Shift:
                _availableCellsForShift.RemoveAt(index);
                break;

            case AvailableCellTypes.SimpleAttack:
                _availableCellsSimpleAttack.RemoveAt(index);
                break;

            case AvailableCellTypes.UniqueAttack:
                _availableCellsUniqueAttack.RemoveAt(index);
                break;

            default:
                throw new Exception();
        }
    }
    internal bool TryFindCell(AvailableCellTypes availableCellType, params int[] xy)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                return _availableCellsForSettingUnit.TryFindCell(xy);

            case AvailableCellTypes.Shift:
                return _availableCellsForShift.TryFindCell(xy);

            case AvailableCellTypes.SimpleAttack:
                return _availableCellsSimpleAttack.TryFindCell(xy);

            case AvailableCellTypes.UniqueAttack:
                return _availableCellsUniqueAttack.TryFindCell(xy);

            default:
                throw new Exception();
        }
    }
    internal int GetAmountCells(AvailableCellTypes availableCellType)
    {
        switch (availableCellType)
        {
            case AvailableCellTypes.None:
                throw new Exception();

            case AvailableCellTypes.SettingUnit:
                return _availableCellsForSettingUnit.Count;

            case AvailableCellTypes.Shift:
                return _availableCellsForShift.Count;

            case AvailableCellTypes.SimpleAttack:
                return _availableCellsSimpleAttack.Count;

            case AvailableCellTypes.UniqueAttack:
                return _availableCellsUniqueAttack.Count;

            default:
                throw new Exception();
        }
    }


    internal void GetCellsForShift(params int[] xy) => _availableCellsForShift.GetCellsForShift(xy);
    internal void GetCellsForSettingUnit(Player player) => _availableCellsForSettingUnit.GetStartCellsForSettingUnit(player);
    internal void GetCellsForAllAttack(Player player, params int[] xy) => CellUnitWorker.GetCellsForAttack(player, out _availableCellsSimpleAttack, out _availableCellsUniqueAttack, xy);
}
