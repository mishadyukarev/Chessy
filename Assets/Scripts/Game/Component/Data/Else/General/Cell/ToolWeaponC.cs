using System;

namespace Chessy.Game
{
    public struct ToolWeaponC
    {
        public TWTypes ToolWeapon;
        public bool Is(TWTypes tWType) => ToolWeapon == tWType;
        public bool HaveToolWeap => ToolWeapon != default;


        public LevelTypes LevelTWType;
        public bool Is(LevelTypes levelTWType) => LevelTWType == levelTWType;


        private int _shieldProt;
        public int ShieldProt => _shieldProt;
        public void AddShieldProtect(LevelTypes levelTWType)
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
                ToolWeapon = TWTypes.None;
            }
        }
        public void SyncShield(int shieldProt) => _shieldProt = shieldProt;
    }
}