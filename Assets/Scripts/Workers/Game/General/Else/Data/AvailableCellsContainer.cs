using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Game.Else
{
    internal struct AvailableCellsContainer
    {
        private static EcsEntity _availableCellsSettingEnt;
        private static EcsEntity _availableCellsShiftEnt;
        private static EcsEntity _availableCellsSimpleAttackEnt;
        private static EcsEntity _availableCellsUniqueAttackEnt;

        internal AvailableCellsContainer(EcsWorld gameWorld)
        {
            _availableCellsSettingEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
            _availableCellsShiftEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
            _availableCellsSimpleAttackEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
            _availableCellsUniqueAttackEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
        }

        private static List<int[]> GetAllCells(AvailableCellTypes availableCellType)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    return _availableCellsSettingEnt.Get<AvailableCellsComponent>().AvailableCells;

                case AvailableCellTypes.Shift:
                    return _availableCellsShiftEnt.Get<AvailableCellsComponent>().AvailableCells;

                case AvailableCellTypes.SimpleAttack:
                    return _availableCellsSimpleAttackEnt.Get<AvailableCellsComponent>().AvailableCells;

                case AvailableCellTypes.UniqueAttack:
                    return _availableCellsUniqueAttackEnt.Get<AvailableCellsComponent>().AvailableCells;

                default:
                    throw new Exception();
            }
        }
        internal static void SetAllCellsCopy(AvailableCellTypes availableCellType, List<int[]> list)
        {
            switch (availableCellType)
            {
                case AvailableCellTypes.None:
                    throw new Exception();

                case AvailableCellTypes.SettingUnit:
                    _availableCellsSettingEnt.Get<AvailableCellsComponent>().AvailableCells = list.Copy();
                    break;

                case AvailableCellTypes.Shift:
                    _availableCellsShiftEnt.Get<AvailableCellsComponent>().AvailableCells = list.Copy();
                    break;

                case AvailableCellTypes.SimpleAttack:
                    _availableCellsSimpleAttackEnt.Get<AvailableCellsComponent>().AvailableCells = list.Copy();
                    break;

                case AvailableCellTypes.UniqueAttack:
                    _availableCellsUniqueAttackEnt.Get<AvailableCellsComponent>().AvailableCells = list.Copy();
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static List<int[]> GetAllCellsCopy(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Copy();

        internal static void AddAvailableCell(AvailableCellTypes availableCellType, int[] xy) => GetAllCells(availableCellType).Add(xy);
        internal static void ClearAvailableCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Clear();
        internal static int[] GetCellByIndex(AvailableCellTypes availableCellType, int index) => GetAllCells(availableCellType)[index];
        internal static void RemoveAt(AvailableCellTypes availableCellType, int index) => GetAllCells(availableCellType).RemoveAt(index);
        internal static bool TryFindCell(AvailableCellTypes availableCellType, int[] xy) => GetAllCells(availableCellType).TryFindCell(xy);
        internal static int GetAmountCells(AvailableCellTypes availableCellType) => GetAllCells(availableCellType).Count;
    }
}