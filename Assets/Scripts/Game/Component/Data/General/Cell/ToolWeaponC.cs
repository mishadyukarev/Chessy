using System;

namespace Game.Game
{
    public struct ToolWeaponC
    {
        public TWTypes TW;
        public bool Is(TWTypes tW) => TW == tW;
        public bool HaveTW => TW != default;


        public LevelTypes Level;
        public bool Is(LevelTypes level) => Level == level;


        private int _shieldProt;
        public int ShieldProt => _shieldProt;
        public void SetShieldProtect(LevelTypes levelTWType)
        {
            switch (levelTWType)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First: _shieldProt = 1; return;
                case LevelTypes.Second: _shieldProt = 3; return;
                default: throw new Exception();
            }
        }
        public void TakeShieldProtect(int taking = 1)
        {
            _shieldProt -= taking;
            if (ShieldProt <= 0)
            {
                TW = TWTypes.None;
            }
        }
        public void SyncShield(int shieldProt) => _shieldProt = shieldProt;
    }
}