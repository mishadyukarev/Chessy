using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct HpUnitWC : IUnitCell
    {
        readonly byte _idx;

        public const int MAX = 100;


        public bool HaveMax => Unit<HpC>(_idx).Hp >= MAX;


        internal HpUnitWC(in byte idx) => _idx = idx;


        public void SetMax() => Unit<HpC>(_idx).Hp = 100;

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
                Unit<UnitCellWC>(_idx).Kill();
            }
        }
        public void Sync(in int hp) => Unit<HpC>(_idx).Hp = hp;
    }
}