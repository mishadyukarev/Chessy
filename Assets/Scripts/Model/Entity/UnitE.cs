using Chessy.Model.Cell.Unit;
using Chessy.Model.Component;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;
namespace Chessy.Model
{
    public struct UnitE
    {
        public UnitOnCellC MainC;
        public PositionC PositionC;
        public HealthC HealthC;
        public EnergyC EnergyC;
        public WaterAmountC WaterC;
        public EffectsUnitC EffectsC;

        public MainToolWeaponUnitC MainToolWeaponC;
        public ExtraToolWeaponUnitC ExtraToolWeaponC;
        public WhoLastDiedOnCellC WhoLastDiedHereC;
        public ExtractionResourcesWithUnitC ExtractionResourcesC;
        public NeedUpdateViewC NeedUpdateViewC;
        public ShiftingObjectC ShiftingInfoForUnitC;
        public WhereViewIdxCellC WhereViewDataUnitC;

        public readonly HowManyDistanceNeedForShiftingUnitC HowManyDistanceNeedForShiftingUnitC;
        public readonly WhereUnitCanShiftC WhereCanShiftC;
        public WhereUnitCanAttackToEnemyC WhereCanAttackSimpleAttackToEnemyC;
        public WhereUnitCanAttackToEnemyC WhereCanAttackUniqueAttackToEnemyC;
        public readonly ButtonsAbilitiesUnitC UniqueButtonsC;
        public readonly CooldownAbilitiesInSecondsC CooldownsC;
        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerOrNotC;
        public readonly CanSetUnitHereC CanSetUnitHereC;
        public readonly WhereUnitCanFireAdultForestC WhereUnitCanFireAdultForestC;
        public readonly EffectsUnitsRightBarsC EffectsUnitsRightBarsC;
        public readonly HasUnitKingEffectHereC HasKingEffectHereC;

        internal UnitE(in bool def)
        {
            MainC = default;
            PositionC = default;
            HealthC = default;
            EnergyC = default;
            WaterC = default;
            EffectsC = default;
            MainToolWeaponC = default;
            ExtraToolWeaponC = default;
            WhoLastDiedHereC = default;
            ExtractionResourcesC = default;
            NeedUpdateViewC = default;
            ShiftingInfoForUnitC = default;
            WhereViewDataUnitC = default;

            VisibleToOtherPlayerOrNotC = new VisibleToOtherPlayerOrNotC(default);
            CanSetUnitHereC = new CanSetUnitHereC(new bool[(byte)PlayerTypes.End]);
            HowManyDistanceNeedForShiftingUnitC = new HowManyDistanceNeedForShiftingUnitC(new float[IndexCellsValues.CELLS]);
            WhereCanShiftC = new WhereUnitCanShiftC(new bool[IndexCellsValues.CELLS]);
            WhereCanAttackSimpleAttackToEnemyC = new WhereUnitCanAttackToEnemyC(new bool[IndexCellsValues.CELLS]);
            WhereCanAttackUniqueAttackToEnemyC = new WhereUnitCanAttackToEnemyC(new bool[IndexCellsValues.CELLS]);
            UniqueButtonsC = new ButtonsAbilitiesUnitC(new AbilityTypes[(byte)ButtonTypes.End]);
            CooldownsC = new CooldownAbilitiesInSecondsC(default);
            WhereUnitCanFireAdultForestC = new WhereUnitCanFireAdultForestC(new bool[IndexCellsValues.CELLS]);
            HasKingEffectHereC = new HasUnitKingEffectHereC(new bool[(byte)PlayerTypes.End]);

            var dict = new Dictionary<ButtonTypes, EffectTypes>();
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++) dict.Add(buttonT, default);
            EffectsUnitsRightBarsC = new EffectsUnitsRightBarsC(default);
        }

        internal void Dispose()
        {
            MainC.UnitType = UnitTypes.None;

            WhereViewDataUnitC = default;
        }

        internal UnitE Clone()
        {
            var unitE = new UnitE(default);

            unitE.MainC = MainC;
            unitE.HealthC = HealthC;
            unitE.WaterC = WaterC;
            unitE.EffectsC = EffectsC;

            unitE.MainToolWeaponC = MainToolWeaponC;
            unitE.ExtraToolWeaponC = ExtraToolWeaponC;
            unitE.WhoLastDiedHereC = WhoLastDiedHereC;
            unitE.ExtractionResourcesC = ExtractionResourcesC;
            unitE.NeedUpdateViewC = NeedUpdateViewC;

            unitE.WhereViewDataUnitC = WhereViewDataUnitC;


            unitE.UniqueButtonsC.Sync(UniqueButtonsC.AbilityTypesClone);
            unitE.CooldownsC.Sync(CooldownsC.CooldonwsCopy);

            return unitE;
        }
    }
}