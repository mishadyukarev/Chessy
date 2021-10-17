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
                    case UnitTypes.None:
                        throw new Exception();

                    case UnitTypes.King:
                        return true;

                    case UnitTypes.Pawn:
                        return true;

                    case UnitTypes.Rook:
                        return false;

                    case UnitTypes.Bishop:
                        return false;

                    default:
                        throw new Exception();
                }
            }
        }


        private Dictionary<PlayerTypes, bool> _isVisibleDict;
        internal bool IsVisibleUnit(PlayerTypes key) => _isVisibleDict[key];
        internal void SetIsVisibleUnit(PlayerTypes key, bool value) => _isVisibleDict[key] = value;


        internal CondUnitTypes CondUnitType { get; set; }
        internal void ResetCondType() => CondUnitType = default;
        internal bool Is(CondUnitTypes conditionUnitType) => CondUnitType == conditionUnitType;
        internal bool Is(CondUnitTypes[] conditionUnitTypes)
        {
            foreach (var conditionUnitType in conditionUnitTypes)
                if (Is(conditionUnitType)) return true;
            return false;
        }


        internal UpgradeUnitTypes UpgradeUnitType;


        internal ToolWeaponTypes TWExtraPawnType;
        internal bool HaveExtraToolWeaponPawn => TWExtraPawnType != default;


        private Dictionary<CondUnitTypes, int> _amountStepsInCondition;
        internal int AmountStepsInProtectRelax(CondUnitTypes conditionUnitType) => _amountStepsInCondition[conditionUnitType];
        internal void SetAmountStepsInProtectRelax(CondUnitTypes conditionUnitType, int value) => _amountStepsInCondition[conditionUnitType] = value;
        internal void AddAmountStepsInProtectRelax(CondUnitTypes conditionUnitType, int adding = 1) => _amountStepsInCondition[conditionUnitType] += adding;
        internal void TakeAmountStepsInProtectRelax(CondUnitTypes conditionUnitType, int taking = 1) => _amountStepsInCondition[conditionUnitType] += taking;

        internal void ResetAmountStepsInProtectRelax(CondUnitTypes conditionUnitType) => _amountStepsInCondition[conditionUnitType] = default;


        internal int AmountSteps { get; set; }
        internal void AddAmountSteps(int adding = 1) => AmountSteps += adding;
        internal void TakeAmountSteps(int taking = 1) => AmountSteps -= taking;


        internal bool HaveMaxAmountSteps => AmountSteps == UnitValues.StandartAmountSteps(UnitType);
        internal bool HaveMinAmountSteps => AmountSteps >= 1;
        internal void ResetAmountSteps() => AmountSteps = default;
        internal void RefreshAmountSteps() => AmountSteps = UnitValues.StandartAmountSteps(UnitType);


        internal int AmountHealth { get; set; }
        internal void AddAmountHealth(int adding = 1) => AmountHealth += adding;
        internal void TakeAmountHealth(int taking = 1) => AmountHealth -= taking;


        internal int MaxAmountHealth => UnitValues.StandartAmountHealth(UnitType);
        internal bool HaveMaxAmountHealth => AmountHealth >= MaxAmountHealth;
        internal bool HaveAmountHealth => AmountHealth > 0;
        internal void AddStandartHeal() => AddAmountHealth((int)(UnitValues.StandartAmountHealth(UnitType) * UnitValues.ForAdding(UnitType)));

        internal int PowerProtection(BuildingTypes buildingType, Dictionary<EnvirTypes, bool> envrs)
        {
            int powerProt = 0;

            if (Is(CondUnitTypes.Protected)) powerProt += (int)(SimplePowerDamage * UnitValues.PercentForProtection(UnitType));
            else if (Is(CondUnitTypes.Relaxed)) powerProt += (int)(SimplePowerDamage * UnitValues.PercentForRelax(UnitType));

            switch (buildingType)
            {
                case BuildingTypes.City:
                    switch (UnitType)
                    {
                        case UnitTypes.None: throw new Exception();
                        case UnitTypes.King: powerProt += (int)(SimplePowerDamage * 0.5f); break;
                        case UnitTypes.Pawn: powerProt += (int)(SimplePowerDamage * 0.5); break;
                        case UnitTypes.Rook: powerProt += (int)(SimplePowerDamage * 0.5); break;
                        case UnitTypes.Bishop: powerProt += (int)(SimplePowerDamage * 0.5); break;
                        default: throw new Exception();
                    }
                    break;

                case BuildingTypes.Farm:
                    //switch (UnitType)
                    //{
                    //    case UnitTypes.None: throw new Exception();
                    //    case UnitTypes.King: powerProtection += (int)(SimplePowerDamage * 0.3f); break;
                    //    case UnitTypes.Pawn: powerProtection += (int)(SimplePowerDamage * 0.5f); break;
                    //    case UnitTypes.Rook: powerProtection += (int)(SimplePowerDamage * 0.5f); break;
                    //    case UnitTypes.Bishop: powerProtection += (int)(SimplePowerDamage * 0.5f); break;
                    //    default: throw new Exception();
                    //}
                    break;

                case BuildingTypes.Woodcutter:
                    //switch (UnitType)
                    //{
                    //    case UnitTypes.None: throw new Exception();
                    //    case UnitTypes.King: powerProtection += (int)(SimplePowerDamage * 0.1f); break;
                    //    case UnitTypes.Pawn: powerProtection += (int)(SimplePowerDamage * 0.3f); break;
                    //    case UnitTypes.Rook: powerProtection += (int)(SimplePowerDamage * 0.3f); break;
                    //    case UnitTypes.Bishop: powerProtection += (int)(SimplePowerDamage * 0.3f); break;
                    //    default: throw new Exception();
                    //}
                    break;

                case BuildingTypes.Mine:
                    //switch (UnitType)
                    //{
                    //    case UnitTypes.None: throw new Exception();
                    //    case UnitTypes.King: powerProtection -= (int)(SimplePowerDamage * 0.3f); break;
                    //    case UnitTypes.Pawn: powerProtection -= (int)(SimplePowerDamage * 0.3f); break;
                    //    case UnitTypes.Rook: powerProtection -= (int)(SimplePowerDamage * 0.3f); break;
                    //    case UnitTypes.Bishop: powerProtection -= (int)(SimplePowerDamage * 0.3f); break;
                    //    default: throw new Exception();
                    //}
                    break;
            }


            if (envrs[EnvirTypes.Fertilizer])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType) 
                    * UnitValues.ProtectionRatioEnvir(UnitType, EnvirTypes.Fertilizer));

            if (envrs[EnvirTypes.AdultForest])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType) 
                    * UnitValues.ProtectionRatioEnvir(UnitType, EnvirTypes.AdultForest));

            if (envrs[EnvirTypes.Hill])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType) 
                    * UnitValues.ProtectionRatioEnvir(UnitType, EnvirTypes.Hill));


            if (TWExtraPawnType == ToolWeaponTypes.Shield) powerProt += (int)(SimplePowerDamage * 0.3f);


            return powerProt;
        }

        internal int SimplePowerDamage
        {
            get
            {
                float simplePowerDamege = UnitValues.SimplePowerDamage(UnitType);

                if (UnitType.Is(UnitTypes.Pawn))
                {
                    switch (TWExtraPawnType)
                    {
                        case ToolWeaponTypes.None:
                            break;

                        case ToolWeaponTypes.Hoe:
                            throw new Exception();

                        case ToolWeaponTypes.Pick:
                            simplePowerDamege -= simplePowerDamege * 0.5f;
                            break;

                        case ToolWeaponTypes.Sword:
                            simplePowerDamege += simplePowerDamege * 0.5f;
                            break;

                        case ToolWeaponTypes.Shield:
                            break;

                        default:
                            throw new Exception();
                    }
                }

                else if (UnitType.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                {
                    //switch (Arc)
                    //{
                    //    default:
                    //        break;
                    //}

                    //switch (ArcherWeapType)
                    //{
                    //    case ToolWeaponTypes.None:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Hoe:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Pick:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Sword:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Bow:
                    //        simplePowerDamege += 0;
                    //        break;

                    //    case ToolWeaponTypes.Crossbow:
                    //        simplePowerDamege += simplePowerDamege / 3;
                    //        break;

                    //    default:
                    //        throw new Exception();
                    //}

                    //switch (ExtraTWPawnType)
                    //{
                    //    case ToolWeaponTypes.None:
                    //        break;

                    //    case ToolWeaponTypes.Hoe:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Axe:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Pick:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Sword:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Bow:
                    //        throw new Exception();

                    //    case ToolWeaponTypes.Crossbow:
                    //        throw new Exception();

                    //    default:
                    //        throw new Exception();
                    //}
                }

                return (int)simplePowerDamege;
            }
        }
        internal int UniquePowerDamage => (int)(SimplePowerDamage * UnitValues.UniqueRatioPowerDamage(UnitType));

        internal void ReplaceUnit(CellUnitDataCom newCellUnitDataCom)
        {
            UnitType = newCellUnitDataCom.UnitType;
            UpgradeUnitType = newCellUnitDataCom.UpgradeUnitType;
            TWExtraPawnType = newCellUnitDataCom.TWExtraPawnType;
            AmountHealth = newCellUnitDataCom.AmountHealth;
            AmountSteps = newCellUnitDataCom.AmountSteps;
            CondUnitType = newCellUnitDataCom.CondUnitType;
        }

        internal CellUnitDataCom(bool needNew) : this()
        {
            if (needNew)
            {
                _isVisibleDict = new Dictionary<PlayerTypes, bool>();

                _isVisibleDict.Add(PlayerTypes.First, default);
                _isVisibleDict.Add(PlayerTypes.Second, default);


                _amountStepsInCondition = new Dictionary<CondUnitTypes, int>();
                _amountStepsInCondition.Add(CondUnitTypes.None, default);
                _amountStepsInCondition.Add(CondUnitTypes.Protected, default);
                _amountStepsInCondition.Add(CondUnitTypes.Relaxed, default);
            }
        }
    }
}
