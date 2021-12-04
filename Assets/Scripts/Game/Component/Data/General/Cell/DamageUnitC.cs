using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct DamageUnitC : IUnitCell
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

        public int DamageAttack(AttackTypes attack)
        {
            var tw = UnitToolWeapon<ToolWeaponC>(_idx).ToolWeapon;
            var haveEff = Unit<EffectsC>(_idx).Have(UnitStatTypes.Damage);
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
                var condition = Unit<ConditionC>(_idx).Condition;

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

        internal DamageUnitC(in byte idx) => _idx = idx;
    }
}