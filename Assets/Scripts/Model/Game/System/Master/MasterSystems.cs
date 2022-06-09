using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System.Master;

namespace Chessy.Game.Model.System
{
    sealed class MasterSystems
    {
        internal readonly TryBuyFromMarketBuildingS_M TryBuyFromMarketBuildingS;
        internal readonly TryMeltInMelterBuildingS_M TryMeltInMelterBuildingS;
        internal readonly GetHeroInCenterS_M GetHeroInCenterS;
        internal readonly TryExecuteReadyForOnlineS_M TryExecuteReadyForOnlineS;
        internal readonly TryExecuteDoneS_M TryExecuteDoneS_M;
        internal readonly TryAttackUnitOnCell_M TryAttackUnit_M;
        internal readonly TrySetUnitOnCellS_M TrySetUnitS_M;
        internal readonly TryShiftUnitS_M TryShiftUnitS_M;
        internal readonly TrySeedYoungForestOnCellWithPawnS_M TrySeedYoungForestOnCellWithPawnUnitS;
        internal readonly TryBuildFarmOnCellWithUnitS_M TryBuildFarmOnCellWithUnitS;
        internal readonly TrySetConditionUnitOnCellS_M TrySetConditionUnitS;
        internal readonly TryGiveTakeToolOrWeaponToUnitOnCellS_M TryGiveTakeToolWeaponUnitS;
        internal readonly TryGiveWaterToUnitsAroundRainyS_M RainyGiveWaterToUnitsAroundS_M;

        internal readonly TryTakeAdultForestResourcesS_M TryTakeAdultForestResourcesS;
        internal readonly TryDestroyAdultForestS_M TryDestroyAdultForestS;
        internal readonly ClearAllEnvironmentS_M ClearAllEnvironmentS;
        internal readonly TryDestroyAllTrailsOnCellS_M TryClearAllTrailsOnCellS;
        internal readonly TrySeedNewYoungForestS_M TrySeedNewYoungForestS;

        internal readonly GetDataCellsAfterAnyDoingS_M GetDataCellsS;
        internal readonly ExecuteUpdateEverythingS_M ExecuteUpdateEverythingS;

        internal readonly UnitSystems UnitSs;
        internal readonly BuildingSystems BuildingSs;

        internal readonly ResetAllS_M ResetAllS;

        internal readonly StartGameS_M StartGameS;

        internal MasterSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            TryBuyFromMarketBuildingS = new TryBuyFromMarketBuildingS_M(sMG, eMG);
            TryMeltInMelterBuildingS = new TryMeltInMelterBuildingS_M(sMG, eMG);
            GetHeroInCenterS = new GetHeroInCenterS_M(sMG, eMG);
            TryExecuteReadyForOnlineS = new TryExecuteReadyForOnlineS_M(sMG, eMG);
            ExecuteUpdateEverythingS = new ExecuteUpdateEverythingS_M(sMG, eMG);
            TryExecuteDoneS_M = new TryExecuteDoneS_M(sMG, eMG);      
            TryAttackUnit_M = new TryAttackUnitOnCell_M(sMG, eMG);
            TrySetUnitS_M = new TrySetUnitOnCellS_M(sMG, eMG);
            TryShiftUnitS_M = new TryShiftUnitS_M(sMG, eMG);
            TrySeedYoungForestOnCellWithPawnUnitS = new TrySeedYoungForestOnCellWithPawnS_M(sMG, eMG);
            TryBuildFarmOnCellWithUnitS = new TryBuildFarmOnCellWithUnitS_M(sMG, eMG);
            TrySetConditionUnitS = new TrySetConditionUnitOnCellS_M(sMG, eMG);
            TryGiveTakeToolWeaponUnitS = new TryGiveTakeToolOrWeaponToUnitOnCellS_M(sMG, eMG);

            GetDataCellsS = new GetDataCellsAfterAnyDoingS_M(sMG, eMG);

            TryTakeAdultForestResourcesS = new TryTakeAdultForestResourcesS_M(sMG, eMG);
            ClearAllEnvironmentS = new ClearAllEnvironmentS_M(sMG, eMG);
            TryDestroyAdultForestS = new TryDestroyAdultForestS_M(sMG, eMG);
            TryClearAllTrailsOnCellS = new TryDestroyAllTrailsOnCellS_M(sMG, eMG);
            TrySeedNewYoungForestS = new TrySeedNewYoungForestS_M(sMG, eMG);

            RainyGiveWaterToUnitsAroundS_M = new TryGiveWaterToUnitsAroundRainyS_M(sMG, eMG);
            ResetAllS = new ResetAllS_M(sMG, eMG);

            UnitSs = new UnitSystems(sMG, eMG);
            BuildingSs = new BuildingSystems(sMG, eMG);

            StartGameS = new StartGameS_M(sMG, eMG);
        }
    }
}