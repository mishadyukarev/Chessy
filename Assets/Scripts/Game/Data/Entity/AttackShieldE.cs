using System;

namespace Game.Game
{
    public struct AttackShieldE
    {
        readonly ActionC _actionC;
        public IdxC IdxC;
        public DamageC DamageC;

        public AttackShieldE(in Action action) : this()
        {
            _actionC.Action = action;
        }

        public void AttackShield(in float damage, in byte idx_cell)
        {
            DamageC.Damage = damage;
            IdxC.Idx = idx_cell;
            _actionC.Invoke();
        }
    }
}