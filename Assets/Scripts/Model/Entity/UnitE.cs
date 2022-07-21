using Chessy.Model.Component;
using Chessy.Model.Values;

namespace Chessy.Model.Entity
{
    public sealed class UnitE
    {
        public readonly UnitOnCellC MainC = new();
        public readonly HealthC HealthC = new();
        public readonly WaterAmountC WaterC = new();
        public readonly EffectsUnitC EffectsC = new();

        public readonly MainToolWeaponUnitC MainToolWeaponC = new();
        public readonly ExtraToolWeaponUnitC ExtraToolWeaponC = new();
        public readonly WhoLastDiedOnCellC WhoLastDiedHereC = new();
        public readonly ExtractionResourcesWithUnitC ExtractionResourcesC = new();
        public readonly NeedUpdateViewC NeedUpdateViewC = new();
        public readonly ShiftingObjectC ShiftingInfoForUnitC = new();
        public readonly WhereViewIdxCellC WhereViewDataUnitC = new();

        public readonly HowManyDistanceNeedForShiftingUnitC HowManyDistanceNeedForShiftingUnitC = new(new float[IndexCellsValues.CELLS]);
        public readonly WhereUnitCanShiftC WhereCanShiftC = new(new bool[IndexCellsValues.CELLS]);
        public readonly WhereUnitCanAttackToEnemyC WhereCanAttackSimpleAttackToEnemyC = new(new bool[IndexCellsValues.CELLS]);
        public readonly WhereUnitCanAttackToEnemyC WhereCanAttackUniqueAttackToEnemyC = new(new bool[IndexCellsValues.CELLS]);
        public readonly ButtonsAbilitiesUnitC UniqueButtonsC = new(new AbilityTypes[(byte)ButtonTypes.End]);
        public readonly CooldownAbilitiesInSecondsC CooldownsC = new(default);
        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerOrNotC = new(default);
        public readonly CanSetUnitHereC CanSetUnitHereC = new(new bool[(byte)PlayerTypes.End]);
        public readonly WhereUnitCanFireAdultForestC WhereUnitCanFireAdultForestC = new(new bool[IndexCellsValues.CELLS]);
        public readonly EffectsUnitsRightBarsC EffectsUnitsRightBarsC = new(default);
        public readonly HasUnitKingEffectHereC HasKingEffectHereC = new(new bool[(byte)PlayerTypes.End]);

        internal void Dispose()
        {
            MainC.UnitT = UnitTypes.None;

            WhereViewDataUnitC.Dispose();
        }

        internal void Clone(in UnitE newUnitE)
        {
            MainC.Clone(newUnitE.MainC);
            HealthC.Health = newUnitE.HealthC.Health;
            WaterC.Water = newUnitE.WaterC.Water;
            EffectsC.Clone(newUnitE.EffectsC);

            MainToolWeaponC.Clone(newUnitE.MainToolWeaponC);
            ExtraToolWeaponC.Clone(newUnitE.ExtraToolWeaponC);
            WhoLastDiedHereC.Clone(newUnitE.WhoLastDiedHereC);
            ExtractionResourcesC.Clone(newUnitE.ExtractionResourcesC);

            WhereViewDataUnitC.Clone(newUnitE.WhereViewDataUnitC);


            UniqueButtonsC.Sync(newUnitE.UniqueButtonsC.AbilityTypesClone);
            CooldownsC.Sync(newUnitE.CooldownsC.CooldonwsCopy);
        }
    }
}