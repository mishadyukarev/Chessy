using System;

namespace Game.Game
{
    public struct UnitAttackUnitE
    {
        readonly ActionC _actionC;
        public IdxC IdxC;
        public DamageC DamageC;
        public PlayerTC WhoKiller;

        public UnitAttackUnitE(in Action action) : this()
        {
            _actionC.Action = action;
        }

        public void Attack(in float damage, in PlayerTypes whoKiller, in byte idx_cell)
        {
            DamageC.Damage = damage;
            WhoKiller.Player = whoKiller;
            IdxC.Idx = idx_cell;
            _actionC.Invoke();
        }
    }
}