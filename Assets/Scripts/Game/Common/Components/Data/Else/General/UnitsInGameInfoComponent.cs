//using Assets.Scripts.Workers;
//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Component
//{
//    public struct UnitsInGameInfoComponent
//    {
//        private Dictionary<UnitTypes, Dictionary<bool, List<byte>>> _amountUnitsInGame;

//        public UnitsInGameInfoComponent(Dictionary<UnitTypes, Dictionary<bool, List<byte>>> dict)
//        {
//            _amountUnitsInGame = dict;

//            for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
//            {
//                var dict1 = new Dictionary<bool, List<byte>>();

//                dict1.Add(true, new List<byte>());
//                dict1.Add(false, new List<byte>());

//                _amountUnitsInGame.Add(unitType, dict1);
//            }
//        }

//        public byte GetIdxUnitInGameByIndexList(UnitTypes unitType, bool key, int index) => _amountUnitsInGame[unitType][key][index];
//        public List<byte> GetLixtIdxUnits(UnitTypes unitType, bool key) => _amountUnitsInGame[unitType][key].Copy();
//        public void SetAmountUnitInGame(UnitTypes unitType, bool key, List<byte> list) => _amountUnitsInGame[unitType][key] = list.Copy();
//        public void AddAmountUnitInGame(UnitTypes unitType, bool key, byte idxAdding) => _amountUnitsInGame[unitType][key].Add(idxAdding);
//        public void RemoveAmountUnitsInGame(UnitTypes unitType, bool key, byte xyTaking)
//        {
//            if (!_amountUnitsInGame[unitType][key].Remove(xyTaking)) throw new Exception();
//        }


//        public int GetAmountUnitsInGame(UnitTypes unitType, bool key) => _amountUnitsInGame[unitType][key].Count;
//        public int GetAmountUnitsInGame(bool key, params UnitTypes[] unitTypes)
//        {
//            int amountUnits = default;
//            foreach (var unitType in unitTypes) amountUnits += GetAmountUnitsInGame(unitType, key);
//            return amountUnits;
//        }
//        public int GetAmountAllUnitsInGame() => GetAmountAllUnitsInGame(true) + GetAmountAllUnitsInGame(false);
//        public int GetAmountAllUnitsInGame(bool key)
//        {
//            return GetAmountUnitsInGame(UnitTypes.King, key)
//                + GetAmountUnitsInGame(UnitTypes.Pawn, key)
//                + GetAmountUnitsInGame(UnitTypes.Rook, key)
//                + GetAmountUnitsInGame(UnitTypes.Bishop, key);
//        }

//        public bool IsSettedKing(bool key) => GetLixtIdxUnits(UnitTypes.King, key).Count > 0;
//    }
//}
