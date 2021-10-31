using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct DamageComponent
    {
        public int StandDamage(UnitTypes unitType, LevelUnitTypes levelUnitType) => UnitValues.StandDamage(unitType, levelUnitType);
        public int DamageAttack(UnitTypes unit, LevelUnitTypes levelUnit, ToolWeaponC tWC, UnitEffectsC  effectsC, AttackTypes attack)
        {
            float powerDamege = StandDamage(unit, levelUnit);

            powerDamege += StandDamage(unit, levelUnit) * UnitValues.PercentTW(tWC.ToolWeapType);
            if (attack == AttackTypes.Unique) powerDamege += powerDamege * UnitValues.UNIQUE_PERCENT_DAMAGE;

            if (effectsC.Have(StatTypes.Damage)) powerDamege += StandDamage(unit, levelUnit) * 0.2f;

            return (int)powerDamege;
        }
        public int DamageOnCell(UnitTypes unit, LevelUnitTypes levelUnit, ConditionUnitC condUnitC, ToolWeaponC tWC, UnitEffectsC effectsC, BuildTypes buildType, Dictionary<EnvTypes, bool> envrs)
        {
            float powerDamege = DamageAttack(unit, levelUnit, tWC, effectsC, AttackTypes.Simple);

            var standDamage = StandDamage(unit, levelUnit);

            powerDamege += standDamage * UnitValues.Percent(condUnitC.CondUnitType);
            powerDamege += standDamage * UnitValues.ProtectionPercent(buildType);
            foreach (var envType in envrs.Keys) powerDamege += standDamage * UnitValues.ProtectionPercent(envType);

            return (int)powerDamege;
        }
    }
}