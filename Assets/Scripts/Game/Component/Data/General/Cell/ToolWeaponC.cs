using System;

namespace Game.Game
{
    public struct ToolWeaponC
    {
        public TWTypes ToolWeapon;
        public bool Is(TWTypes tW) => ToolWeapon == tW;
        public bool HaveToolWeap => ToolWeapon != default;


        public LevelTypes LevelTWType;
        public bool Is(LevelTypes level) => LevelTWType == level;


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
                ToolWeapon = TWTypes.None;
            }
        }
        public void SyncShield(int shieldProt) => _shieldProt = shieldProt;
    }
}