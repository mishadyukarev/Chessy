using System;

namespace Chessy.Game
{
    public struct LevelC
    {
        private LevelUnitTypes _levelUnit;
        public LevelUnitTypes Level => _levelUnit;
        public bool Is(LevelUnitTypes level) => _levelUnit == level;


        public void SetLevel(LevelUnitTypes level)
        {
            if (level == LevelUnitTypes.None) throw new Exception();
            //if (_levelUnit == level) throw new Exception();

            _levelUnit = level;
        }
        public void Sync(LevelUnitTypes level) => _levelUnit = level;   
    }
}