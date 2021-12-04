using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct HpUnitC : IUnitCell
    {
        readonly byte _idx;

        public const int MAX = 100;


        UnitTypes Unit
        {
            get => Unit<UnitC>(_idx).Unit;
            set => Unit<UnitC>(_idx).Unit = value;
        }
        LevelTypes Level
        {
            get => Unit<LevelC>(_idx).Level;
            set => Unit<LevelC>(_idx).Level = value;
        }
        PlayerTypes Owner
        {
            get => Unit<OwnerC>(_idx).Owner;
            set => Unit<OwnerC>(_idx).Owner = value;
        }

        public bool HaveMax => Unit<HpC>(_idx).HP >= MAX;


        internal HpUnitC(in byte idx) => _idx = idx;


        public void SetMax() => Unit<HpC>(_idx).HP = 100;

        public void Take(in UniqueAbilTypes uniq)
        {
            var damage = 0;

            switch (uniq)
            {
                case UniqueAbilTypes.CircularAttack: damage = 25; break;
                default: throw new Exception();
            }

            Unit<HpC>(_idx).Take(damage);
        }
        public void TakeAttack(in int damage)
        {
            Unit<HpC>(_idx).Take(damage);
            if (Unit<WaterUnitC>(_idx).IsHpDeathAfterAttack) Unit<HpC>(_idx).SetMinHp();
        }
        public void TakeFire()
        {
            Unit<HpC>(_idx).Take(40);


            if (!Unit<HpC>(_idx).Have)
            {
                Unit<UnitCellC>(_idx).Kill(Level, Owner);
            }
        }
        public void Sync(in int hp) => Unit<HpC>(_idx).HP = hp;
    }
}