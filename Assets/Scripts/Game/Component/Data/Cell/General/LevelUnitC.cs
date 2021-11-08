using System;

namespace Chessy.Game
{
    public struct LevelUnitC
    {
        private LevelUnitTypes _levelUnitType;
        public LevelUnitTypes Level => _levelUnitType;

        public void SetLevel(LevelUnitTypes levelUnit)
        {
            if (levelUnit == LevelUnitTypes.None) throw new Exception();
            _levelUnitType = levelUnit;
        }
        public void Sync(LevelUnitTypes levelUnit) => _levelUnitType = levelUnit;
        public bool Is(LevelUnitTypes levelUnitType) => _levelUnitType == levelUnitType;
    }
}