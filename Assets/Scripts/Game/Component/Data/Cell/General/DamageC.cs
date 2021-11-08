using System.Collections.Generic;

namespace Chessy.Game
{
    public struct DamageC
    {
        public int StandDamage(UnitTypes unitType, LevelUnitTypes levelUnitType) => UnitValues.StandDamage(unitType, levelUnitType);
        public int DamageAttack(UnitTypes unit, LevelUnitTypes levelUnit, ToolWeaponC tWC, UnitEffectsC effectsC, AttackTypes attack, float upgPerc)
        {
            var standDamage = StandDamage(unit, levelUnit);

            float powerDamege = standDamage;

            powerDamege += standDamage * UnitValues.PercentTW(tWC.ToolWeapType);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitValues.UNIQUE_PERCENT_DAMAGE;

            if (effectsC.Have(UnitStatTypes.Damage)) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell(UnitTypes unit, LevelUnitTypes levelUnit, ConditionUnitC condUnitC, ToolWeaponC tWC, UnitEffectsC effectsC, float upgPerc, BuildTypes buildType, Dictionary<EnvTypes, bool> envrs)
        {
            float powerDamege = DamageAttack(unit, levelUnit, tWC, effectsC, AttackTypes.Simple, upgPerc);

            var standDamage = StandDamage(unit, levelUnit);

            powerDamege += standDamage * UnitValues.Percent(condUnitC.Condition);
            powerDamege += standDamage * UnitValues.ProtectionPercent(buildType);
            foreach (var item in envrs)
            {
                if (item.Value) powerDamege += standDamage * UnitValues.ProtectionPercent(item.Key);
            }
            return (int)powerDamege;
        }
    }
}