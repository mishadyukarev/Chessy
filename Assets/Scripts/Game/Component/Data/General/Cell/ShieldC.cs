using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct ShieldC : ITWCellE
    {
        readonly byte _idx;

        internal ShieldC(byte idx) : this() => _idx = idx;


        public void Take(int taking = 1)
        {
            UnitToolWeapon<ProtectionC>(_idx).Take(taking);

            if (UnitToolWeapon<ProtectionC>(_idx).Protection <= 0)
            {
                UnitToolWeapon<ToolWeaponC>(_idx).Reset();
            }
        }
    }
}