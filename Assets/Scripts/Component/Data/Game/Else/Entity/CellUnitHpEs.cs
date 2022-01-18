using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitHpEs
    {
        static Entity[] _hps;

        public const int MAX_HP = 100;

        public static ref C Hp<C>(in byte idx) where C : struct, ICellUnitHpE => ref _hps[idx].Get<C>();

        public static bool IsHpDeathAfterAttack(in byte idx) => Hp<AmountC>(idx).Amount <= DamageUnitValues.HP_FOR_DEATH_AFTER_ATTACK;
        public static bool HaveMax(in byte idx) => Hp<AmountC>(idx).Amount >= MAX_HP;


        public CellUnitHpEs(in EcsWorld gameW)
        {
            _hps = new Entity[CellValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _hps.Length; idx++)
            {
                _hps[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }

        public static void SetMaxHp(in byte idx) => Hp<AmountC>(idx).Amount = 100;
        public static void Take(in byte idx, in UniqueAbilityTypes uniq)
        {
            var damage = 0;

            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: damage = 25; break;
                default: throw new Exception();
            }

            Hp<AmountC>(idx).Take(damage);
        }
        public static void TakeAttack(in byte idx, in int damage)
        {
            Hp<AmountC>(idx).Take(damage);
            if (IsHpDeathAfterAttack(idx)) Hp<AmountC>(idx).Reset();
        }
        public static void TakeFire(in byte idx) => Hp<AmountC>(idx).Take(40);
    }

    public interface ICellUnitHpE { }
}