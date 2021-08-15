using Assets.Scripts.Workers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component
{
    internal struct UnitsInGameInfoComponent
    {
        private Dictionary<UnitTypes, Dictionary<bool, List<byte>>> _amountUnitsInGame;

        internal UnitsInGameInfoComponent(Dictionary<UnitTypes, Dictionary<bool, List<byte>>> dict)
        {
            _amountUnitsInGame = dict;

            for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                var dict1 = new Dictionary<bool, List<byte>>();

                dict1.Add(true, new List<byte>());
                dict1.Add(false, new List<byte>());

                _amountUnitsInGame.Add(unitType, dict1);
            }
        }

        internal byte GetIdxUnitInGameByIndexList(UnitTypes unitType, bool key, int index) => _amountUnitsInGame[unitType][key][index];
        internal List<byte> GetLixtXyUnits(UnitTypes unitType, bool key) => _amountUnitsInGame[unitType][key].Copy();
        internal void SetAmountUnitInGame(UnitTypes unitType, bool key, List<byte> list) => _amountUnitsInGame[unitType][key] = list.Copy();
        internal void AddAmountUnitInGame(UnitTypes unitType, bool key, byte idxAdding) => _amountUnitsInGame[unitType][key].Add(idxAdding);
        internal void RemoveAmountUnitsInGame(UnitTypes unitType, bool key, byte xyTaking)
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
                + GetAmountUnitsInGame(UnitTypes.Rook, key)
                + GetAmountUnitsInGame(UnitTypes.Bishop, key);
        }

        internal bool IsSettedKing(bool key) => GetLixtXyUnits(UnitTypes.King, key).Count > 0;
    }
}
