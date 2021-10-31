using System;

namespace Scripts.Game
{
    public struct ToolWeaponC
    {
        public ToolWeaponTypes ToolWeapType;
        public bool Is(ToolWeaponTypes tWType) => ToolWeapType == tWType;
        public bool HaveToolWeap => ToolWeapType != default;


        public LevelTWTypes LevelTWType;
        public bool Is(LevelTWTypes levelTWType) => LevelTWType == levelTWType;


        private int _shieldProt;
        public int ShieldProt => _shieldProt;
        public void AddShieldProtect(LevelTWTypes levelTWType)
        {
            switch (levelTWType)
            {
                case LevelTWTypes.None: throw new Exception();
                case LevelTWTypes.Wood: _shieldProt = 1; return;
                case LevelTWTypes.Iron: _shieldProt = 3; return;
                default: throw new Exception();
            }
        }
        public void TakeShieldProtect(int taking = 1)
        {
            _shieldProt -= taking;
            if (ShieldProt <= 0)
            {
                ToolWeapType = ToolWeaponTypes.None;
            }
        }

        public void Set(ToolWeaponC tWC)
        {
            ToolWeapType = tWC.ToolWeapType;
            LevelTWType = tWC.LevelTWType;
            _shieldProt = tWC.ShieldProt;
        }
    }
}