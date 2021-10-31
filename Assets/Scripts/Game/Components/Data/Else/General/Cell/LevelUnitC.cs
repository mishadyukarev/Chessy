using System;

namespace Scripts.Game
{
    public struct LevelUnitC
    {
        private LevelUnitTypes _levelUnitType;
        public LevelUnitTypes LevelUnitType => _levelUnitType;

        public void SetNewLevel(LevelUnitTypes levelUnitType)
        {
            if (_levelUnitType == levelUnitType) throw new Exception("it's got yet");
            _levelUnitType = levelUnitType;
        }
        public void NoneLevelUnit()
        {
            if(_levelUnitType == default) throw new Exception();
            _levelUnitType = default;
        }
        public void SyncLevelUnit(LevelUnitTypes levelUnit) => _levelUnitType = levelUnit;
        public bool Is(LevelUnitTypes levelUnitType) => _levelUnitType == levelUnitType;
    }
}