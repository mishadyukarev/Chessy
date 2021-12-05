using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct WaterUnitC : IUnitCell
    {
        readonly byte _idx;


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
        float UpgadeWaterPercent => UnitUpgC.UpgWaterPercent(Unit, Level, Owner);


        public bool IsHpDeathAfterAttack => Unit<HpC>(_idx).HP <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK;

        public bool NeedWater => Unit<WaterC>(_idx).Water <= 100 * 0.4f;
        public int MaxWater => (int)(100 + 100 * UpgadeWaterPercent);
        public bool HaveMaxWater => Unit<WaterC>(_idx).Water >= MaxWater;




        internal WaterUnitC(in byte idx) => _idx = idx;


        public void SetMaxWater() => Unit<WaterC>(_idx).Set(MaxWater);
        public void ExecuteThirsty()
        {
            float percent = 0;
            switch (Unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: percent = 0.4f; break;
                case UnitTypes.Pawn: percent = 0.5f; break;
                case UnitTypes.Archer: percent = 0.5f; break;
                case UnitTypes.Scout: percent = 0.5f; break;
                case UnitTypes.Elfemale: percent = 0.5f; break;
                default: throw new Exception();
            }

            Unit<HpC>(_idx).Take((int)(HpUnitWC.MAX * percent));
        }
        public void TakeWater() => Unit<WaterC>(_idx).TakeWater((int)(100 * 0.15f));


        public void Sync(in int water) => Unit<WaterC>(_idx).Set(water);
    }
}