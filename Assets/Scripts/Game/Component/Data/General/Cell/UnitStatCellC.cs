using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct UnitStatCellC : IUnitStatCell
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
        int Steps
        {
            get => UnitStat<StepC>(_idx).Steps;
            set => UnitStat<StepC>(_idx).Steps = value;
        }
        float UpgadePercent(UnitStatTypes stat) => UnitUpgC.UpgPercent(stat, Unit, Level, Owner);
        int UpgradeSteps => UnitUpgC.Steps(Unit, Level, Owner);
        bool HaveEffect(UnitStatTypes stat) => UnitEffects<EffectsC>(_idx).Have(stat);
        EnvC EnvC => Environment<EnvC>(_idx);
        TrailC TrailC => Trail<TrailC>(_idx);


        public bool IsHpDeathAfterAttack => UnitStat<HpC>(_idx).HP <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK;

        public bool NeedWater => UnitStat<WaterC>(_idx).Water <= 100 * 0.4f;
        public int MaxWater => (int)(100 + 100 * UpgadePercent(UnitStatTypes.Water));
        public bool HaveMaxWater => UnitStat<WaterC>(_idx).Water >= MaxWater;

        public int MaxAmountSteps => UnitValues.MaxAmountSteps(Unit, HaveEffect(UnitStatTypes.Steps), UpgradeSteps);
        public bool HaveMaxSteps => Steps >= MaxAmountSteps;
        public int StepsForDoing(DirectTypes dir_cur)
        {
            var needSteps = 1;

            if (EnvC.Have(EnvTypes.AdultForest))
            {
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.AdultForest);
                if (TrailC.Have(dir_cur.Invert())) needSteps -= 1;
            }

            if (EnvC.Have(EnvTypes.Hill))
                needSteps += UnitValues.NeedAmountSteps(EnvTypes.Hill);

            return needSteps;
        }
        public bool HaveStepsForDoing(DirectTypes dir_cur) => Steps >= StepsForDoing(dir_cur);

        public int DamageAttack(AttackTypes attack)
        {
            var tw = UnitToolWeapon<ToolWeaponC>(_idx).ToolWeapon;
            var haveEff = UnitEffects<EffectsC>(_idx).Have(UnitStatTypes.Damage);
            var upgPerc = UnitUpgC.UpgPercent(UnitStatTypes.Damage, Unit, Level, Owner);


            var standDamage = UnitValues.StandDamage(Unit, Level);

            float powerDamege = standDamage;

            powerDamege += standDamage * UnitValues.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitValues.UNIQUE_PERCENT_DAMAGE;

            if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell
        {
            get
            {
                var condition = UnitEffects<ConditionC>(_idx).Condition;

                var build = Build<BuildC>(_idx).Build;
                var envrs = Environment<EnvC>(_idx).Envronments;


                float powerDamege = DamageAttack(AttackTypes.Simple);

                var standDamage = UnitValues.StandDamage(Unit, Level);

                powerDamege += standDamage * UnitValues.Percent(condition);
                powerDamege += standDamage * UnitValues.ProtectionPercent(build);
                foreach (var item in envrs)
                {
                    if (item.Value) powerDamege += standDamage * UnitValues.ProtectionPercent(item.Key);
                }
                return (int)powerDamege;
            }
        }


        internal UnitStatCellC(in byte idx) => _idx = idx;


        public void SetMaxWater() => UnitStat<WaterC>(_idx).Set(MaxWater);
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

            UnitStat<HpC>(_idx).Take((int)(HpC.MAX_HP * percent));
        }
        public void TakeWater() => UnitStat<WaterC>(_idx).TakeWater((int)(100 * 0.15f));

        public void SetMaxSteps() => UnitStat<StepC>(_idx).Set(MaxAmountSteps);
        public void TakeStepsForDoing(DirectTypes dir_cur) => UnitStat<StepC>(_idx).Take(StepsForDoing(dir_cur));
        public void Sync(in int hp, in int steps, in int water)
        {
            UnitStat<HpC>(_idx).Set(hp);
            UnitStat<StepC>(_idx).Set(steps);
            UnitStat<WaterC>(_idx).Set(water);
        }
    }
}