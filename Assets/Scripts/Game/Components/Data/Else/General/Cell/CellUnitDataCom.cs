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


        private Dictionary<PlayerTypes, bool> _isVisibleDict;
        internal bool IsVisibleUnit(PlayerTypes key) => _isVisibleDict[key];
        internal void SetIsVisibleUnit(PlayerTypes key, bool value) => _isVisibleDict[key] = value;


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

        internal int MaxAmountHealth => UnitValues.StandartAmountHealth(UnitType, LevelUnitType);
        internal bool HaveMaxAmountHealth => AmountHealth >= MaxAmountHealth;
        internal bool HaveAmountHealth => AmountHealth > 0;
        internal void AddStandartHeal() => AddAmountHealth((int)(UnitValues.StandartAmountHealth(UnitType, LevelUnitType) * UnitValues.ForAddingHealth(UnitType)));
        internal void SetMaxAmountHealth() => AmountHealth = MaxAmountHealth;

        internal int PowerProtection(BuildingTypes buildingType, Dictionary<EnvirTypes, bool> envrs)
        {
            int powerProt = 0;

            if (Is(CondUnitTypes.Protected)) powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, LevelUnitType) * UnitValues.PercentForProtection(UnitType));
            else if (Is(CondUnitTypes.Relaxed)) powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, LevelUnitType) * UnitValues.PercentForRelax(UnitType));

            powerProt += (int)(PowerDamage * UnitValues.ProtectionPercentBuild(UnitType, buildingType));

            if (envrs[EnvirTypes.Fertilizer])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, LevelUnitType) 
                    * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.Fertilizer));

            if (envrs[EnvirTypes.AdultForest])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, LevelUnitType) 
                    * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.AdultForest));

            if (envrs[EnvirTypes.Hill])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, LevelUnitType) 
                    * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.Hill));


            //if (TWExtraType == ToolWeaponTypes.Shield) powerProt += (int)(SimplePowerDamage * 0.3f);


            return powerProt;
        }

        internal int PowerDamage
        {
            get
            {
                float powerDamege = UnitValues.SimplePowerDamage(UnitType, LevelUnitType);

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
                            powerDamege += powerDamege * 0.5f;
                            break;

                        case ToolWeaponTypes.Shield:
                            break;

                        default:
                            throw new Exception();
                    }
                }

                return (int)powerDamege;
            }
        }
        internal int UniquePowerDamage => (int)(PowerDamage * UnitValues.UniqueRatioPowerDamage(UnitType));

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

        internal CellUnitDataCom(bool needNew) : this()
        {
            if (needNew)
            {
                _isVisibleDict = new Dictionary<PlayerTypes, bool>();

                _isVisibleDict.Add(PlayerTypes.First, default);
                _isVisibleDict.Add(PlayerTypes.Second, default);
            }
        }
    }
}
