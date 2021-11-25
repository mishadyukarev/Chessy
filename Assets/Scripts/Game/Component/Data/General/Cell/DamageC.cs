using System.Collections.Generic;

namespace Game.Game
{
    public struct DamageC : IUnitCell
    {
        public int StandDamage(UnitTypes unit, LevelTypes level) => UnitValues.StandDamage(unit, level);
        public int DamageAttack(UnitTypes unit, LevelTypes level, ToolWeaponC tWC, UnitEffectsC effectsC, AttackTypes attack, float upgPerc)
        {
            var standDamage = StandDamage(unit, level);

            float powerDamege = standDamage;

            powerDamege += standDamage * UnitValues.PercentTW(tWC.TW);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * UnitValues.UNIQUE_PERCENT_DAMAGE;

            if (effectsC.Have(UnitStatTypes.Damage)) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage * upgPerc;

            return (int)powerDamege;
        }
        public int DamageOnCell(UnitTypes unit, LevelTypes level, ConditionUnitC condUnitC, ToolWeaponC tWC, UnitEffectsC effectsC, float upgPerc, BuildTypes build, Dictionary<EnvTypes, bool> envrs)
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