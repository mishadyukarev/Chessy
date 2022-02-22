using System;

namespace Game.Game
{
    public struct UnitAttackUnitE
    {
        readonly Action<float, PlayerTypes, byte> _attack;

        public UnitAttackUnitE(in Action<float, PlayerTypes, byte> action) : this()
        {
            _attack = action;
        }

        public void Attack(in float damage, in PlayerTypes whoKiller, in byte idx_cell)
        {
            _attack(damage, whoKiller, idx_cell);
        }
    }
}