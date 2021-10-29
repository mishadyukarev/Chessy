using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct DamageComponent
    {
        public int StandDamage(CellUnitDataCom cellUnitDatC) => UnitValues.StandDamage(cellUnitDatC.UnitType, cellUnitDatC.LevelUnitType);
        public int DamageAttack(CellUnitDataCom cellUnitDatC, ToolWeaponC tWC, UnitEffectsC  effectsC, AttackTypes attackType)
        {
            float powerDamege = StandDamage(cellUnitDatC);

            powerDamege += StandDamage(cellUnitDatC) * UnitValues.PercentTW(tWC.ToolWeapType);
            if (attackType == AttackTypes.Unique) powerDamege += powerDamege * UnitValues.UNIQUE_PERCENT_DAMAGE;

            if (effectsC.Have(StatTypes.Damage)) powerDamege += StandDamage(cellUnitDatC) * 0.2f;

            return (int)powerDamege;
        }
        public int DamageOnCell(CellUnitDataCom cellUnitDatC, ConditionUnitC condUnitC, ToolWeaponC tWC, UnitEffectsC effectsC, BuildingTypes buildType, Dictionary<EnvirTypes, bool> envrs)
        {
            float powerDamege = DamageAttack(cellUnitDatC, tWC, effectsC, AttackTypes.Simple);

            var standDamage = StandDamage(cellUnitDatC);

            powerDamege += standDamage * UnitValues.Percent(condUnitC.CondUnitType);
            powerDamege += standDamage * UnitValues.ProtectionPercent(buildType);
            foreach (var envType in envrs.Keys) powerDamege += standDamage * UnitValues.ProtectionPercent(envType);

            return (int)powerDamege;
        }
    }
}