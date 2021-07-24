using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Game.Else
{
    internal sealed class AvailableCellsEntsWorker
    {
        private static AvailableCellEntsContainer _availableCellEntsContainer;

        internal AvailableCellsEntsWorker(AvailableCellEntsContainer availableCellEntsContainer)
        {
            _availableCellEntsContainer = availableCellEntsContainer;
        }

        internal static List<int[]> GetAllCells(AvailableCellTypes availableCellType)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    return _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.AvailableCells;

                case AvailableCellTypes.Shift:
                    return _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.AvailableCells;

                case AvailableCellTypes.SimpleAttack:
                    return _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.AvailableCells;

                case AvailableCellTypes.UniqueAttack:
                    return _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.AvailableCells;

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
                    _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.SetList(list);
                    break;

                case AvailableCellTypes.Shift:
                    _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.SetList(list);
                    break;

                case AvailableCellTypes.SimpleAttack:
                    _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.SetList(list);
                    break;

                case AvailableCellTypes.UniqueAttack:
                    _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.SetList(list);
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
                    _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.Clear();
                    break;

                case AvailableCellTypes.Shift:
                    _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.Clear();
                    break;

                case AvailableCellTypes.SimpleAttack:
                    _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.Clear();
                    break;

                case AvailableCellTypes.UniqueAttack:
                    _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.Clear();
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
                    _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                case AvailableCellTypes.Shift:
                    _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                case AvailableCellTypes.SimpleAttack:
                    _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.AddAvailableCell(xy);
                    break;

                case AvailableCellTypes.UniqueAttack:
                    _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.AddAvailableCell(xy);
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
                    return _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.GetCellByIndex(index);

                case AvailableCellTypes.Shift:
                    return _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.GetCellByIndex(index);

                case AvailableCellTypes.SimpleAttack:
                    return _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.GetCellByIndex(index);

                case AvailableCellTypes.UniqueAttack:
                    return _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.GetCellByIndex(index);

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
                    _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.RemoveAt(index);
                    break;

                case AvailableCellTypes.Shift:
                    _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.RemoveAt(index);
                    break;

                case AvailableCellTypes.SimpleAttack:
                    _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.RemoveAt(index);
                    break;

                case AvailableCellTypes.UniqueAttack:
                    _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.RemoveAt(index);
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
                    return _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.TryFindCell(xy);

                case AvailableCellTypes.Shift:
                    return _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.TryFindCell(xy);

                case AvailableCellTypes.SimpleAttack:
                    return _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.TryFindCell(xy);

                case AvailableCellTypes.UniqueAttack:
                    return _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.TryFindCell(xy);

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
                    return _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.CountCells;

                case AvailableCellTypes.Shift:
                    return _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.CountCells;

                case AvailableCellTypes.SimpleAttack:
                    return _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.CountCells;

                case AvailableCellTypes.UniqueAttack:
                    return _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.CountCells;

                default:
                    throw new Exception();
            }
        }
    }
}
