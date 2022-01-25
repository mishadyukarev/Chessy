using ECS;

namespace Game.Game
{
    public sealed class CellUnitHpE : EntityAbstract
    {
        public ref AmountC AmountC => ref Ent.Get<AmountC>();

        public bool IsHpDeathAfterAttack => AmountC.Amount <= UnitDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMax => AmountC.Amount >= UnitHpValues.MAX_HP;

        public CellUnitHpE(in EcsWorld gameW) : base(gameW) { }

        public void TakeAttack(in int damage)
        {
            AmountC.Take(damage);
            if (IsHpDeathAfterAttack) AmountC.Reset();
        }
    }
}