using Chessy.Common;
using Chessy.Model.Component;
using Chessy.Model.Model.Component;
using Chessy.Model.Model.Entity;
using Chessy.Model.Model.Entity.Cell.Unit;
using Chessy.Model.Values;
using System.Collections.Generic;

namespace Chessy.Model
{
    public struct UnitE
    {
        public UnitMainC MainC;
        public HealthC HealthC;
        public EnergyC EnergyC;
        public WaterC WaterC;
        public EffectsUnitC EffectsC;
        public MainToolWeaponUnitC MainToolWeaponC;
        public ExtraToolWeaponUnitC ExtraToolWeaponC;
        public WhoLastDiedHereC WhoLastDiedHereC;
        public ExtractionResourcesWithUnitC ExtractionResourcesC;
        public NeedUpdateViewC NeedUpdateViewC;

        public readonly HowManyEnergyNeedForShiftingUnitC HowManyEnergyNeedForShiftingUnitC;
        public readonly WhereUnitCanShiftC WhereCanShiftC;
        public readonly WhereUnitCanAttackToEnemyC WhereCanAttackSimpleAttackToEnemyC;
        public readonly WhereUnitCanAttackToEnemyC WhereCanAttackUniqueAttackToEnemyC;
        public readonly UniqueButtonsC UniqueButtonsC;
        public readonly CooldownAbilitiesC CooldownsC;
        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerOrNotC;
        public readonly CanSetUnitHereC CanSetUnitHereC;
        public readonly WhereUnitCanFireAdultForestC WhereUnitCanFireAdultForestC;
        public readonly EffectsUnitsRightBarsC EffectsUnitsRightBarsC;

        internal UnitE(in bool def)
        {
            MainC = default;
            HealthC = default;
            EnergyC = default;
            WaterC = default;
            EffectsC = default;
            MainToolWeaponC = default;
            ExtraToolWeaponC = default;
            WhoLastDiedHereC = default;
            ExtractionResourcesC = default;
            NeedUpdateViewC = default;

            VisibleToOtherPlayerOrNotC = new VisibleToOtherPlayerOrNotC(default);
            CanSetUnitHereC = new CanSetUnitHereC(new bool[(byte)PlayerTypes.End]);
            HowManyEnergyNeedForShiftingUnitC = new HowManyEnergyNeedForShiftingUnitC(new float[StartValues.CELLS]);
            WhereCanShiftC = new WhereUnitCanShiftC(new bool[StartValues.CELLS]);
            WhereCanAttackSimpleAttackToEnemyC = new WhereUnitCanAttackToEnemyC(new bool[StartValues.CELLS]);
            WhereCanAttackUniqueAttackToEnemyC = new WhereUnitCanAttackToEnemyC(new bool[StartValues.CELLS]);
            UniqueButtonsC = new UniqueButtonsC(new AbilityTypes[(byte)ButtonTypes.End]);
            CooldownsC = new CooldownAbilitiesC(default);
            WhereUnitCanFireAdultForestC = new WhereUnitCanFireAdultForestC(new bool[StartValues.CELLS]);

            var dict = new Dictionary<ButtonTypes, EffectTypes>();
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++) dict.Add(buttonT, default);
            EffectsUnitsRightBarsC = new EffectsUnitsRightBarsC(new Dictionary<ButtonTypes, EffectTypes>());
        }
    }
}