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

        private static List<int[]> GetAllCells(AvailableCellTypes availableCellType)
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
        internal static List<int[]> GetAllCellsCopy(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Copy();
        internal static void SetAllCellsCopy(AvailableCellTypes availableCellType, List<int[]> list)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    _availableCellEntsContainer.AvailableCellsSettingEnt_AvailCellsCom.AvailableCells = list.Copy();
                    break;

                case AvailableCellTypes.Shift:
                    _availableCellEntsContainer.AvailableCellsShiftEnt_AvailCellsCom.AvailableCells = list.Copy();
                    break;

                case AvailableCellTypes.SimpleAttack:
                    _availableCellEntsContainer.AvailableCellsSimpleAttackEnt_AvailCellsCom.AvailableCells = list.Copy();
                    break;

                case AvailableCellTypes.UniqueAttack:
                    _availableCellEntsContainer.AvailableCellsUniqueAttackEnt_AvailCellsCom.AvailableCells = list.Copy();
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddAvailableCell(AvailableCellTypes availableCellType, int[] xy) => GetAllCells(availableCellType).Add(xy);
        internal static void ClearAvailableCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Clear();       
        internal static int[] GetCellByIndex(AvailableCellTypes availableCellType, int index) => GetAllCells(availableCellType)[index];
        internal static void RemoveAt(AvailableCellTypes availableCellType, int index) => GetAllCells(availableCellType).RemoveAt(index);
        internal static bool TryFindCell(AvailableCellTypes availableCellType, int[] xy) => GetAllCells(availableCellType).TryFindCell(xy);
        internal static int GetAmountCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Count;
    }
}