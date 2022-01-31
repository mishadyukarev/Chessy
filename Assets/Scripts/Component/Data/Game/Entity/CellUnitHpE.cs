using ECS;

namespace Game.Game
{
    public sealed class CellUnitHpE : EntityAbstract
    {
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public bool IsHpDeathAfterAttack => Health.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMax => Health.Amount >= CellUnitHpValues.MAX_HP;
        public bool IsAlive => Health.Amount > 0;

        public CellUnitHpE(in EcsWorld gameW) : base(gameW) { }

        internal void Shift(in CellUnitHpE hpE_from)
        {
            Health = hpE_from.Health;
            hpE_from.Health.Amount = 0;
        }

        public void Attack(in int damage)
        {
            Health.Amount -= damage;
            if (IsHpDeathAfterAttack) Health.Amount = 0;
        }

        public void SetMax()
        {
            Health.Amount = CellUnitHpValues.MAX_HP;
        }
    }
}