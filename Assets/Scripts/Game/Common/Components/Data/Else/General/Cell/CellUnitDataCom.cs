using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct CellUnitDataCom
    {
        public UnitTypes UnitType;
        public bool HaveUnit => UnitType != UnitTypes.None;
        public void DefUnitType() => UnitType = default;
        public bool Is(UnitTypes unitType) => UnitType.Is(unitType);
        public bool Is(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
        public bool IsMelee
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

        public CondUnitTypes CondUnitType { get; set; }
        public void DefCondType() => CondUnitType = default;
        public bool Is(CondUnitTypes condUnitType) => CondUnitType == condUnitType;
        public bool Is(CondUnitTypes[] condUnitTypes)
        {
            foreach (var conditionUnitType in condUnitTypes)
                if (Is(conditionUnitType)) return true;
            return false;
        }


        public LevelUnitTypes LevelUnitType;


        public ToolWeaponTypes TWExtraType;
        public bool IsTWExtraType(ToolWeaponTypes toolWeaponType) => TWExtraType == toolWeaponType;
        public bool HaveExtraTW => TWExtraType != default;
        public bool HaveShield => TWExtraType == ToolWeaponTypes.Shield;

        public LevelTWTypes LevelTWType;
        public bool IsLevelTWType(LevelTWTypes levelTWType) => LevelTWType == levelTWType;

        public int ShieldProtection;
        public void AddShieldProtect(LevelTWTypes levelTWType)
        {
            switch (levelTWType)
            {
                case LevelTWTypes.None: throw new Exception();
                case LevelTWTypes.Wood: ShieldProtection = 1; return;
                case LevelTWTypes.Iron: ShieldProtection = 3; return;
                default: throw new Exception();
            }
        }
        public void TakeShieldProtect(int taking = 1)
        {
            ShieldProtection -= taking;
            if(ShieldProtection <= 0)
            {
                TWExtraType = ToolWeaponTypes.None;
            }
        }

        public int AmountSteps { get; set; }
        public void AddAmountSteps(int adding = 1) => AmountSteps += adding;
        public void TakeAmountSteps(int taking = 1) => AmountSteps -= taking;
        public int MaxAmountSteps => UnitValues.StandartAmountSteps(UnitType);
        public bool HaveMaxAmountSteps => AmountSteps == MaxAmountSteps;
        public bool HaveMinAmountSteps => AmountSteps >= 1;
        public void DefAmountSteps() => AmountSteps = default;
        public void SetMaxAmountSteps() => AmountSteps = MaxAmountSteps;


        private Dictionary<StatTypes, bool> _effects;
        public void Set(StatTypes statType, bool isActive)
        {
            if (_effects.ContainsKey(statType)) _effects[statType] = isActive;
            else throw new Exception();
        }
        public bool Have(StatTypes statType)
        {
            if (_effects.ContainsKey(statType)) return _effects[statType];
            else throw new Exception();
        }


        public int AmountHealth { get; set; }
        public void AddAmountHealth(int adding = 1) => AmountHealth += adding;
        public void TakeAmountHealth(int taking = 1) => AmountHealth -= taking;
        public void TakeAmountHealth(int max, float taking) => AmountHealth -= (int)(max * taking);

        public int MaxAmountHealth => UnitValues.StandAmountHealth(UnitType);
        public bool HaveMaxAmountHealth => AmountHealth >= MaxAmountHealth;
        public bool HaveAmountHealth => AmountHealth > 0;
        public void AddStandartHeal() => AddAmountHealth((int)(UnitValues.StandAmountHealth(UnitType) * UnitValues.ForAddingHealth(UnitType)));
        public void SetMaxAmountHealth() => AmountHealth = MaxAmountHealth;

        public int StandPowerDamage => UnitValues.StandPowerDamage(UnitType, LevelUnitType);
        public int PowerDamageAttack(AttackTypes attackType)
        {
            float powerDamege = StandPowerDamage;

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
        public int PowerDamageOnCell(BuildingTypes buildType, Dictionary<EnvirTypes, bool> envrs)
        {
            float powerDamege = PowerDamageAttack(AttackTypes.Simple);

            if (Is(CondUnitTypes.Protected)) powerDamege += StandPowerDamage * UnitValues.PercentForProtection;
            else if (Is(CondUnitTypes.Relaxed)) powerDamege += StandPowerDamage * UnitValues.PercentForRelax;


            powerDamege += StandPowerDamage * UnitValues.ProtectionPercentBuild(buildType);


            if (envrs[EnvirTypes.Fertilizer])
                powerDamege += StandPowerDamage * UnitValues.ProtectionPercentEnvir(EnvirTypes.Fertilizer);

            if (envrs[EnvirTypes.AdultForest])
                powerDamege += StandPowerDamage * UnitValues.ProtectionPercentEnvir(EnvirTypes.AdultForest);

            if (envrs[EnvirTypes.Hill])
                powerDamege += StandPowerDamage * UnitValues.ProtectionPercentEnvir(EnvirTypes.Hill);


            return (int)powerDamege;
        }

        public void ReplaceUnit(CellUnitDataCom newUnit)
        {
            UnitType = newUnit.UnitType;
            LevelUnitType = newUnit.LevelUnitType;
            TWExtraType = newUnit.TWExtraType;
            LevelTWType = newUnit.LevelTWType;
            ShieldProtection = newUnit.ShieldProtection;
            AmountHealth = newUnit.AmountHealth;
            AmountSteps = newUnit.AmountSteps;
            CondUnitType = newUnit.CondUnitType;
        }

        public CellUnitDataCom(bool needNew) : this()
        {
            if (needNew)
            {
                _effects = new Dictionary<StatTypes, bool>();
                _effects.Add(StatTypes.Health, default);
                _effects.Add(StatTypes.Damage, default);
                _effects.Add(StatTypes.Steps, default);
            }
        }
    }
}
