using System;

namespace Game.Game
{
    public readonly struct SystemsModel
    {
        public SystemsModel(ref ActionC update, in EntitiesModel ents, in Action updateView, in Action updateUI, out Action runAfterDoing)
        {
            update.Action +=
                (Action)
                new InputS(ents).Run
                + new RayS(ents).Run
                + new SelectorS(ents, updateView, updateUI).Run;


            runAfterDoing =
                (Action)
                new CenterUpgradeUnitMS(ents).Run
                + new GetHeroMS(ents).Run
                + new SetUnitMS(ents).Run
                + new AttackMS(ents).Run
                + new UnitShiftMS(ents).Run

                + new WorldClearTrailsS(ents).Run
                + new WorldCountsAmountBuildingInGameS(ents).Run

                + new PawnExtractAdultForestGetCellsS(ents).Run
                + new PawnExtractHillS(ents).Run

                + new DestroyBuildingS(ents).Run
                + new CityBuildingGetCellsS(ents).Run
                + new WoodcutterExtractGetCellsS(ents).Run
                + new FarmExtractGetCellsS(ents).Run

                + new UnitAttackUnitS(ents).Run
                + new UnitShiftS(ents).Run
                + new AttackShieldS(ents).Run   
                + new GetUnitTypeS(ents).Run
                + new GetCellsForSetUnitS(ents).Run
                + new AbilitySyncS(ents).Run
                + new GetDamageUnitsS(ents).Run
                + new VisibleUnitAndBuildingS(ents).Run
                + new GetCellsForShiftUnitS(ents).Run
                + new GetAttackMeleeCellsS(ents).Run
                + new GetCellsForAttackArcherS(ents).Run
                + new GetCellsForArsonArcherS(ents).Run;




            var truce = (Action)new TruceMS(ents).Run;

            var updateMove =
                (Action)new UpdatorMS(ents).Run

                + new FireUpdateMS(ents).Run
                + new CloudUpdMS(ents).Run
                + new UpdateIceWallMS(ents).Run
                + new RiverFertilizeAroundUpdateMS(ents).Run
                + new CloudFertilizeUpdMS(ents).Run
                + new WorldDryFertilizerMS(ents).Run
                + new GetPawnUnitUpdMS(ents).Run
                + new TryGetPeopleUpdateMS(ents).Run
                + new WorldMeltIceWallUpdateMS(ents).Run

            #region Building

                + new WoodcutterExtractAdultForestUpdateMS(ents).Run
                + new FarmExtractFertilizeUpdateMS(ents).Run
                + new MineExtractUpdateMS(ents).Run
                + new IceWallGiveWaterUnitsUpdMS(ents).Run
                + new IceWallFertilizeAroundUpdMS(ents).Run
                + new CitySetWoodcuttersAroundUpdateMS(ents).Run
                + new CityExtractHillMS(ents).Run
                + new SmelterSmeltUpdateMS(ents).Run

            #endregion


            #region Environment

                + new MountainThrowHillsUpdMS(ents).Run

            #endregion


            #region Unit

                + new PawnExtractAdultForestMS(ents).Run
                + new ResumeUnitUpdMS(ents).Run
                + new UpdateHealingUnitMS(ents).Run
                + new UpdateHungryMS(ents).Run
                + new ThirstyUnitsUpdateMS(ents).Run
                + new PawnExtractHillUpdateMS(ents).Run

                + new UpdTryFireAroundHellMS(ents).Run
                + new UpdAttackFromWaterHellMS(ents).Run

                + new UpdGiveWaterCloudScowyMS(ents).Run

                + new CamelShiftUpdateMS(ents).Run
                + new CamelSpawnUpdateMS(ents).Run

            #endregion

                + new TryInvokeTruceUpdateMS(truce, ents).Run;


            ents.RpcPoolEs.Doner = new DonerMS(updateMove, ents).Run;
        }
    }
}