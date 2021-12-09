using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct DamageUnitC : IUnitCell
    {
        readonly byte _idx;
        readonly DamageUnitValues _values;

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
            var tw = UnitTW<ToolWeaponC>(_idx).ToolWeapon;
            var haveEff = Unit<EffectsC>(_idx).Have(UnitStatTypes.Damage);
            var upgPerc = UnitUpgC.UpgDamagePercent(Unit, Level, Owner);


            var standDamage = _values.StandDamage(Unit, Level);

            float powerDamege = standDamage;

            powerDamege += standDamage * _values.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * DamageUnitValues.UNIQUE_PERCENT_DAMAGE;

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

                var standDamage = _values.StandDamage(Unit, Level);

                powerDamege += standDamage * _values.ProtRelaxPercent(condition);
                powerDamege += standDamage * _values.ProtectionPercent(build);
                foreach (var item in envrs)
                {
                    if (item.Value) powerDamege += standDamage * _values.ProtectionPercent(item.Key);
                }
                return (int)powerDamege;
            }
        }

        internal DamageUnitC(in byte idx, in DamageUnitValues values)
        {
            _idx = idx;
            _values = values;
        }
    }
}