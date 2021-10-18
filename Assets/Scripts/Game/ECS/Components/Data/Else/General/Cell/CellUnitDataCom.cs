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
        internal bool Is(CondUnitTypes condUnitType) => CondUnitType == condUnitType;
        internal bool Is(CondUnitTypes[] condUnitTypes)
        {
            foreach (var conditionUnitType in condUnitTypes)
                if (Is(conditionUnitType)) return true;
            return false;
        }


        internal UpgradeUnitTypes UpgradeUnitType;


        internal ToolWeaponTypes TWExtraPawnType;
        internal bool HaveExtraToolWeaponPawn => TWExtraPawnType != default;

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

            powerProt += (int)(SimplePowerDamage * UnitValues.ProtectionPercentBuild(UnitType, buildingType));

            if (envrs[EnvirTypes.Fertilizer])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, UpgradeUnitType) 
                    * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.Fertilizer));

            if (envrs[EnvirTypes.AdultForest])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, UpgradeUnitType) 
                    * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.AdultForest));

            if (envrs[EnvirTypes.Hill])
                powerProt += (int)(UnitValues.SimplePowerDamage(UnitType, UpgradeUnitType) 
                    * UnitValues.ProtectionPercentEnvir(UnitType, EnvirTypes.Hill));


            if (TWExtraPawnType == ToolWeaponTypes.Shield) powerProt += (int)(SimplePowerDamage * 0.3f);


            return powerProt;
        }

        internal int SimplePowerDamage
        {
            get
            {
                float simplePowerDamege = UnitValues.SimplePowerDamage(UnitType, UpgradeUnitType);

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
            }
        }
    }
}
