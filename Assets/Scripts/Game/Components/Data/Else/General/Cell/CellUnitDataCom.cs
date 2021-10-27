using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct CellUnitDataCom
    {
        internal UnitTypes UnitType;
        internal bool HaveUnit => UnitType != UnitTypes.None;
        internal void DefUnitType() => UnitType = default;
        internal bool Is(UnitTypes unitType) => UnitType.Is(unitType);
        internal bool Is(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
        internal bool IsMelee
        {
            get
            {
                switch (UnitType)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return true;
                    case UnitTypes.Pawn: return true;
                    case UnitTypes.Rook: return false;
                    case UnitTypes.Bishop: return false;
                    case UnitTypes.Scout: return true;
                    default: throw new Exception();
                }
            }
        }

        internal CondUnitTypes CondUnitType { get; set; }
        internal void DefCondType() => CondUnitType = default;
        internal bool Is(CondUnitTypes condUnitType) => CondUnitType == condUnitType;
        internal bool Is(CondUnitTypes[] condUnitTypes)
        {
            foreach (var conditionUnitType in condUnitTypes)
                if (Is(conditionUnitType)) return true;
            return false;
        }


        internal LevelUnitTypes LevelUnitType;


        internal ToolWeaponTypes TWExtraType;
        internal bool IsTWExtraType(ToolWeaponTypes toolWeaponType) => TWExtraType == toolWeaponType;
        internal bool HaveExtraTW => TWExtraType != default;

        internal LevelTWTypes LevelTWType;
        internal bool IsLevelTWType(LevelTWTypes levelTWType) => LevelTWType == levelTWType;

        internal int ShieldProtection;
        internal void AddShieldProtect(LevelTWTypes levelTWType)
        {
            switch (levelTWType)
            {
                case LevelTWTypes.None: throw new Exception();
                case LevelTWTypes.Wood: ShieldProtection = 1; return;
                case LevelTWTypes.Iron: ShieldProtection = 3; return;
                default: throw new Exception();
            }
        }
        internal void TakeShieldProtect(int taking = 1) => ShieldProtection -= taking;


        internal int AmountSteps { get; set; }
        internal void AddAmountSteps(int adding = 1) => AmountSteps += adding;
        internal void TakeAmountSteps(int taking = 1) => AmountSteps -= taking;

        internal bool HaveMaxAmountSteps => AmountSteps == UnitValues.StandartAmountSteps(UnitType);
        internal bool HaveMinAmountSteps => AmountSteps >= 1;
        internal void DefAmountSteps() => AmountSteps = default;
        internal void SetMaxAmountSteps() => AmountSteps = UnitValues.StandartAmountSteps(UnitType);


        internal int AmountHealth { get; set; }
        internal void AddAmountHealth(int adding = 1) => AmountHealth += adding;
        internal void TakeAmountHealth(int taking = 1) => AmountHealth -= taking;

        internal int MaxAmountHealth => UnitValues.StandAmountHealthAll;
        internal bool HaveMaxAmountHealth => AmountHealth >= MaxAmountHealth;
        internal bool HaveAmountHealth => AmountHealth > 0;
        internal void AddStandartHeal() => AddAmountHealth((int)(UnitValues.StandAmountHealthAll * UnitValues.ForAddingHealth(UnitType)));
        internal void SetMaxAmountHealth() => AmountHealth = MaxAmountHealth;


        internal int PowerDamageAttack(AttackTypes attackType)
        {
            float powerDamege = UnitValues.StandPowerDamage(UnitType, LevelUnitType);

            if (UnitType.Is(UnitTypes.Pawn))
            {
                switch (TWExtraType)
                {
                    case ToolWeaponTypes.None:
                        break;

                    case ToolWeaponTypes.Hoe:
                        throw new Exception();

                    case ToolWeaponTypes.Pick:
                        //simplePowerDamege -= simplePowerDamege * 0.5f;
                        break;

                    case ToolWeaponTypes.Sword:
                        powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * 0.5f;
                        break;

                    case ToolWeaponTypes.Shield:
                        break;

                    default:
                        throw new Exception();
                }
            }

            if(attackType == AttackTypes.Unique) powerDamege += powerDamege * UnitValues.UniqueRatioPowerDamage;

            return (int)powerDamege;
        }

        internal int PowerDamageOnCell(BuildingTypes buildType, Dictionary<EnvirTypes, bool> envrs)
        {
            float powerDamege = PowerDamageAttack(AttackTypes.Simple);

            if (Is(CondUnitTypes.Protected)) powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * UnitValues.PercentForProtection;
            else if (Is(CondUnitTypes.Relaxed)) powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * UnitValues.PercentForRelax;


            powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * UnitValues.ProtectionPercentBuild(UnitType, buildType);


            if (envrs[EnvirTypes.Fertilizer])
                powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.Fertilizer);

            if (envrs[EnvirTypes.AdultForest])
                powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.AdultForest);

            if (envrs[EnvirTypes.Hill])
                powerDamege += UnitValues.StandPowerDamage(UnitType, LevelUnitType) * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.Hill);


            return (int)powerDamege;
        }

        internal void ReplaceUnit(CellUnitDataCom newUnit)
        {
            UnitType = newUnit.UnitType;
            LevelUnitType = newUnit.LevelUnitType;
            TWExtraType = newUnit.TWExtraType;
            LevelTWType = newUnit.LevelTWType;
            AmountHealth = newUnit.AmountHealth;
            AmountSteps = newUnit.AmountSteps;
            CondUnitType = newUnit.CondUnitType;
        }
    }
}
