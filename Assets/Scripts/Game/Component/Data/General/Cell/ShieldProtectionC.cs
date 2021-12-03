using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct ShieldProtectionC : ITWCellE
    {
        int _protection;
        byte _idx;

        public int Protection => _protection;


        public ShieldProtectionC(byte idx) : this() => _idx = idx;


        internal void Set(ShieldProtectionC shieldC) => _protection = shieldC._protection;
        internal void Set(int shieldProt) => _protection = shieldProt;
        internal void Reset() => _protection = 0;

        public void Take(int taking = 1)
        {
            _protection -= taking;

            if (_protection <= 0)
            {
                UnitToolWeapon<ToolWeaponC>(_idx).Reset();
            }
        }
    }
}