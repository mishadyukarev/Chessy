using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Game.Else.Info.Units
{
    internal struct InfoUnitsDataContainer
    {
        private static EcsEntity _kingInfoEnt;
        private static EcsEntity _pawnInfoEnt;
        private static EcsEntity _pawnSwordInfoEnt;
        private static EcsEntity _rookInfoEnt;
        private static EcsEntity _rookCrossbowInfoEnt;
        private static EcsEntity _bishopInfoInGameEnt;
        private static EcsEntity _bishopCrossbowInfoEnt;

        internal InfoUnitsDataContainer(EcsWorld gameWorld)
        {
            _kingInfoEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

            _pawnInfoEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

            _pawnSwordInfoEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

            _rookInfoEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

            _rookCrossbowInfoEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

            _bishopInfoInGameEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

            _bishopCrossbowInfoEnt = gameWorld.NewEntity()
                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));
        }

        #region Condition

        private static List<int[]> GetUnitsInStandardCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return _kingInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        case UnitTypes.Pawn:
                            return _pawnInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        case UnitTypes.PawnSword:
                            return _pawnSwordInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        case UnitTypes.Rook:
                            return _rookInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        case UnitTypes.RookCrossbow:
                            return _rookCrossbowInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        case UnitTypes.Bishop:
                            return _bishopInfoInGameEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        case UnitTypes.BishopCrossbow:
                            return _bishopCrossbowInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInNone[key];

                        default:
                            throw new Exception();
                    }

                case ConditionUnitTypes.Protected:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return _kingInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        case UnitTypes.Pawn:
                            return _pawnInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        case UnitTypes.PawnSword:
                            return _pawnSwordInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        case UnitTypes.Rook:
                            return _rookInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        case UnitTypes.RookCrossbow:
                            return _rookCrossbowInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        case UnitTypes.Bishop:
                            return _bishopInfoInGameEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        case UnitTypes.BishopCrossbow:
                            return _bishopCrossbowInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInProtect[key];

                        default:
                            throw new Exception();
                    }

                case ConditionUnitTypes.Relaxed:
                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            return _kingInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        case UnitTypes.Pawn:
                            return _pawnInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        case UnitTypes.PawnSword:
                            return _pawnSwordInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        case UnitTypes.Rook:
                            return _rookInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        case UnitTypes.RookCrossbow:
                            return _rookCrossbowInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        case UnitTypes.Bishop:
                            return _bishopInfoInGameEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        case UnitTypes.BishopCrossbow:
                            return _bishopCrossbowInfoEnt.Get<XyUnitsInConditionComponent>().UnitsInRelax[key];

                        default:
                            throw new Exception();
                    }

                default:
                    throw new Exception();
            }
        }

        internal static int[] GetXyInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index) => (int[])GetUnitsInStandardCondition(protectRelaxType, unitType, key)[index].Clone();
        internal static int GetAmountUnitsInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key) => GetUnitsInStandardCondition(protectRelaxType, unitType, key).Count;
        internal static bool TryFindUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xy) => GetUnitsInStandardCondition(protectRelaxType, unitType, key).TryFindCell(xy);
        internal static void AddUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xyAdding) => GetUnitsInStandardCondition(protectRelaxType, unitType, key).Add(xyAdding);
        internal static void RemoveUnitInCondition(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int[] xyTaking)
        {
            if (!GetUnitsInStandardCondition(protectRelaxType, unitType, key).TryFindCellInListAndRemove(xyTaking))
                throw new Exception();
        }
        internal static void ReplaceCondition(ConditionUnitTypes preConditionType, ConditionUnitTypes newConditionType, UnitTypes unitType, bool isMasterKey, int[] xy)
        {
            RemoveUnitInCondition(preConditionType, unitType, isMasterKey, xy);
            AddUnitInCondition(newConditionType, unitType, isMasterKey, xy);
        }

        internal static void RemoveUnitInConditionByIndex(ConditionUnitTypes protectRelaxType, UnitTypes unitType, bool key, int index)
            => GetUnitsInStandardCondition(protectRelaxType, unitType, key).RemoveAt(index);

        #endregion
    }
}
