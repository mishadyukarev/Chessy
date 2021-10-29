using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public struct DamageComponent
    {
        public int StandPowerDamage(CellUnitDataCom cellUnitDatC) => UnitValues.StandPowerDamage(cellUnitDatC.UnitType, cellUnitDatC.LevelUnitType);
        public int PowerDamageAttack(CellUnitDataCom cellUnitDatC, ToolWeaponC tWC ,AttackTypes attackType)
        {
            float powerDamege = StandPowerDamage(cellUnitDatC);

            if (cellUnitDatC.UnitType.Is(UnitTypes.Pawn))
            {
                switch (tWC.TWExtraType)
                {
                    case ToolWeaponTypes.None:
                        break;

                    case ToolWeaponTypes.Hoe:
                        throw new Exception();

                    case ToolWeaponTypes.Pick:
                        //simplePowerDamege -= simplePowerDamege * 0.5f;
                        break;

                    case ToolWeaponTypes.Sword:
                        powerDamege += UnitValues.StandPowerDamage(cellUnitDatC.UnitType, cellUnitDatC.LevelUnitType) * 0.5f;
                        break;

                    case ToolWeaponTypes.Shield:
                        break;

                    default:
                        throw new Exception();
                }
            }

            if (attackType == AttackTypes.Unique) powerDamege += powerDamege * UnitValues.UniqueRatioPowerDamage;

            return (int)powerDamege;
        }
        public int PowerDamageOnCell(CellUnitDataCom cellUnitDatC,ToolWeaponC tWC, BuildingTypes buildType, Dictionary<EnvirTypes, bool> envrs)
        {
            float powerDamege = PowerDamageAttack(cellUnitDatC, tWC, AttackTypes.Simple);

            var standPowerDamage = StandPowerDamage(cellUnitDatC);

            if (cellUnitDatC.CondUnitType != default)
                powerDamege += standPowerDamage * UnitValues.Percent(cellUnitDatC.CondUnitType);


            powerDamege += standPowerDamage * UnitValues.ProtectionPercent(buildType);


            if (envrs[EnvirTypes.Fertilizer])
                powerDamege += standPowerDamage * UnitValues.ProtectionPercent(EnvirTypes.Fertilizer);

            if (envrs[EnvirTypes.AdultForest])
                powerDamege += standPowerDamage * UnitValues.ProtectionPercent(EnvirTypes.AdultForest);

            if (envrs[EnvirTypes.Hill])
                powerDamege += standPowerDamage * UnitValues.ProtectionPercent(EnvirTypes.Hill);


            return (int)powerDamege;
        }
    }
}