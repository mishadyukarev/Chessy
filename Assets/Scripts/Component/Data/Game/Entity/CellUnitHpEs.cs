using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitHpEs
    {
        static Entity[] _hps;

        public const int MAX_HP = 100;

        public static ref AmountC Hp(in byte idx) => ref _hps[idx].Get<AmountC>();

        public static bool IsHpDeathAfterAttack(in byte idx) => Hp(idx).Amount <= DamageUnitValues.HP_FOR_DEATH_AFTER_ATTACK;
        public static bool HaveMax(in byte idx) => Hp(idx).Amount >= MAX_HP;


        public CellUnitHpEs(in EcsWorld gameW)
        {
            _hps = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _hps.Length; idx++)
            {
                _hps[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }

        public static void SetMaxHp(in byte idx) => Hp(idx).Amount = 100;
        public static void Take(in byte idx, in UniqueAbilityTypes uniq)
        {
            var damage = 0;

            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: damage = 25; break;
                default: throw new Exception();
            }

            Hp(idx).Take(damage);
        }
        public static void TakeAttack(in byte idx, in int damage)
        {
            Hp(idx).Take(damage);
            if (IsHpDeathAfterAttack(idx)) Hp(idx).Reset();
        }
        public static void TakeFire(in byte idx) => Hp(idx).Take(40);
    }

    public interface ICellUnitHpE { }
}