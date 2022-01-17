using ECS;

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
                    .Add(new AmountC(idx));
            }
        }
    }

    public interface ICellUnitHpE { }
}