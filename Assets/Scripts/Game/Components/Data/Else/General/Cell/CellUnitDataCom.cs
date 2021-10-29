using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellUnitDataCom
    {
        public UnitTypes UnitType;
        public bool HaveUnit => UnitType != UnitTypes.None;
        public void DefUnitType() => UnitType = default;
        public bool Is(UnitTypes unitType) => UnitType.Is(unitType);
        public bool Is(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
        public bool IsMelee
        {
            get
            {
                switch (UnitType)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return true;
                    case UnitTypes.Pawn: return true;
                    case UnitTypes.Rook: return false;
                    case UnitTypes.Bishop: return false;
                    case UnitTypes.Scout: return true;
                    default: throw new Exception();
                }
            }
        }

        public LevelUnitTypes LevelUnitType;


        public CondUnitTypes CondUnitType { get; set; }
        public void DefCondType() => CondUnitType = default;
        public bool Is(CondUnitTypes condUnitType) => CondUnitType == condUnitType;
        public bool Is(CondUnitTypes[] condUnitTypes)
        {
            foreach (var conditionUnitType in condUnitTypes)
                if (Is(conditionUnitType)) return true;
            return false;
        }





        private Dictionary<StatTypes, bool> _effects;
        public void Set(StatTypes statType, bool isActive)
        {
            if (_effects.ContainsKey(statType)) _effects[statType] = isActive;
            else throw new Exception();
        }
        public void DefStat(StatTypes statType) => Set(statType, default);
        public bool Have(StatTypes statType)
        {
            if (_effects.ContainsKey(statType)) return _effects[statType];
            else throw new Exception();
        }



        public void ReplaceUnit(CellUnitDataCom newUnit)
        {
            UnitType = newUnit.UnitType;
            LevelUnitType = newUnit.LevelUnitType;
            _effects[StatTypes.Health] = newUnit.Have(StatTypes.Health);
            _effects[StatTypes.Damage] = newUnit.Have(StatTypes.Damage);
            _effects[StatTypes.Steps] = newUnit.Have(StatTypes.Steps);
            CondUnitType = newUnit.CondUnitType;
        }

        public CellUnitDataCom(bool needNew) : this()
        {
            if (needNew)
            {
                _effects = new Dictionary<StatTypes, bool>();
                _effects.Add(StatTypes.Health, default);
                _effects.Add(StatTypes.Damage, default);
                _effects.Add(StatTypes.Steps, default);
            }
        }
    }
}
