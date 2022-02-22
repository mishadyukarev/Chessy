using System;

namespace Game.Game
{
    public struct AttackShieldE
    {
        readonly Action<float, byte> _attackShield;

        public AttackShieldE(in Action<float, byte> action) : this()
        {
            _attackShield = action;
        }

        public void Attack(in float damage, in byte idx_cell) => _attackShield(damage, idx_cell);
    }
}