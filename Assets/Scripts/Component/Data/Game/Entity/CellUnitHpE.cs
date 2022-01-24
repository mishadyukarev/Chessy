using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitHpE : CellAbstE
    {
        public ref AmountC Hp => ref Ent.Get<AmountC>();

        public bool IsHpDeathAfterAttack => Hp.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMax => Hp.Amount >= UnitHpValues.MAX_HP;

        public CellUnitHpE(in EcsWorld gameW, in byte idx) : base(gameW, idx) { }

        public void TakeAttack(in int damage)
        {
            Hp.Take(damage);
            if (IsHpDeathAfterAttack) Hp.Reset();
        }


    }
}