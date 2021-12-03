using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct UnitShieldCellC : IShieldCell
    {
        readonly byte _idx;

        internal UnitShieldCellC(byte idx) : this() => _idx = idx;


        public void Take(int taking = 1)
        {
            UnitShield<ProtectionC>(_idx).Take(taking);

            if (UnitShield<ProtectionC>(_idx).Protection <= 0)
            {
                UnitToolWeapon<ToolWeaponC>(_idx).Reset();
            }
        }
    }
}