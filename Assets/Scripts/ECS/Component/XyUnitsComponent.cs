﻿using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct XyUnitsComponent
    {
        private Dictionary<UnitTypes, Dictionary<bool, List<int[]>>> _amountUnitsInGame;

        internal XyUnitsComponent(Dictionary<UnitTypes, Dictionary<bool, List<int[]>>> dict)
        {
            _amountUnitsInGame = dict;

            for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var dict1 = new Dictionary<bool, List<int[]>>();

                dict1.Add(true, new List<int[]>());
                dict1.Add(false, new List<int[]>());

                _amountUnitsInGame.Add(unitType, dict1);
            }
        }

        internal int[] GetXyUnitInGame(UnitTypes unitType, bool key, int index) => _amountUnitsInGame[unitType][key][index];
        internal List<int[]> GetLixtXyUnits(UnitTypes unitType, bool key) => _amountUnitsInGame[unitType][key].Copy();
        internal void SetAmountUnitInGame(UnitTypes unitType, bool key, List<int[]> list) => _amountUnitsInGame[unitType][key] = list.Copy();
        internal void AddAmountUnitInGame(UnitTypes unitType, bool key, int[] xyAdding) => _amountUnitsInGame[unitType][key].Add(xyAdding);
        internal void RemoveAmountUnitsInGame(UnitTypes unitType, bool key, int[] xyTaking)
        {
            if (!_amountUnitsInGame[unitType][key].TryFindCellInListAndRemove(xyTaking)) throw new Exception();
        }


        internal int GetAmountUnitsInGame(UnitTypes unitType, bool key) => _amountUnitsInGame[unitType][key].Count;
        internal int GetAmountUnitsInGame(bool key, params UnitTypes[] unitTypes)
        {
            int amountUnits = default;
            foreach (var unitType in unitTypes) amountUnits += GetAmountUnitsInGame(unitType, key);
            return amountUnits;
        }
        internal int GetAmountAllUnitsInGame() => GetAmountAllUnitsInGame(true) + GetAmountAllUnitsInGame(false);
        internal int GetAmountAllUnitsInGame(bool key)
        {
            return GetAmountUnitsInGame(UnitTypes.King, key)
                + GetAmountUnitsInGame(UnitTypes.Pawn, key)
                + GetAmountUnitsInGame(UnitTypes.PawnSword, key)
                + GetAmountUnitsInGame(UnitTypes.Rook, key)
                + GetAmountUnitsInGame(UnitTypes.RookCrossbow, key)
                + GetAmountUnitsInGame(UnitTypes.Bishop, key)
                + GetAmountUnitsInGame(UnitTypes.BishopCrossbow, key);
        }

        internal bool IsSettedKing(bool key) => GetLixtXyUnits(UnitTypes.King, key).Count > 0;
    }
}