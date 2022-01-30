using ECS;

namespace Game.Game
{
    public sealed class CellUnitHpE : EntityAbstract
    {
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public bool IsHpDeathAfterAttack => Health.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMax => Health.Amount >= CellUnitHpValues.MAX_HP;

        public CellUnitHpE(in EcsWorld gameW) : base(gameW) { }

        public void TakeAttack(in int damage)
        {
            Health.Take(damage);
            if (IsHpDeathAfterAttack) Health.Reset();
        }

        public void Shift(in CellUnitHpE hpE_from)
        {
            Health = hpE_from.Health;
            hpE_from.Health.Reset();
        }

        public void SetMax()
        {
            Health.Amount = CellUnitHpValues.MAX_HP;
        }
    }
}