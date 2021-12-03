using System.Collections.Generic;

namespace Game.Game
{
    public struct DamageC : IUnitStatCell
    {
        public int StandDamage(UnitTypes unit, LevelTypes level) => UnitValues.StandDamage(unit, level);
        public int DamageAttack(UnitTypes unit, LevelTypes level, ToolWeaponC tWC, EffectsC effectsC, AttackTypes attack, float upgPerc)
        {
            var standDamage = StandDamage(unit, level);

            float powerDamege = standDamage;

            powerDamege += standDamage * UnitValues.PercentTW(tWC.ToolWeapon);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitValues.UNIQUE_PERCENT_DAMAGE;

            if (effectsC.Have(UnitStatTypes.Damage)) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell(UnitTypes unit, LevelTypes level, ConditionC condUnitC, ToolWeaponC tWC, EffectsC effectsC, float upgPerc, BuildTypes build, Dictionary<EnvTypes, bool> envrs)
        {
            float powerDamege = DamageAttack(unit, level, tWC, effectsC, AttackTypes.Simple, upgPerc);

            var standDamage = StandDamage(unit, level);

            powerDamege += standDamage * UnitValues.Percent(condUnitC.Condition);
            powerDamege += standDamage * UnitValues.ProtectionPercent(build);
            foreach (var item in envrs)
            {
                if (item.Value) powerDamege += standDamage * UnitValues.ProtectionPercent(item.Key);
            }
            return (int)powerDamege;
        }
    }
}