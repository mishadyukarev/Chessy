using Chessy.Game.System.Model;
using System;

namespace Chessy.Game
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



            runAfterDoing = default;


            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                runAfterDoing += (Action)
                    new ClearCellsForSetUnitS(playerT, ents).Run
                    + new ClearEffectsKingS(playerT, ents).Run;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                runAfterDoing += (Action)
                    new UnitAttackUnitS(idx_0, ents).Run;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                runAfterDoing += (Action)
                    new DestroyBuildingS(idx_0, ents).Run
                    + new UnitShiftS(idx_0, ents).Run
                    + new AttackShieldS(idx_0, ents).Run;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                runAfterDoing += (Action)
                    new UnitGetEffectsS(idx_0, ents).Run;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                runAfterDoing += (Action) 
                    new WorldClearTrailsS(idx_0, ents).Run
                    + new WoodcutterExtractGetCellsS(idx_0, ents).Run
                    + new FarmExtractGetCellsS(idx_0, ents).Run

                    ///Unit

                    ///Extract
                    + new PawnGetExtractAdultForestS(idx_0, ents).Run
                    + new PawnExtractHillS(idx_0, ents).Run

                    + new GetUnitTypeS(idx_0, ents).Run
                    + new GetDamageUnitsS(idx_0, ents).Run
                    + new AbilitySyncS(idx_0, ents).Run

                    + new VisibleUnitAndBuildingS(idx_0, ents).Run
                    + new GetCellsForShiftUnitS(idx_0, ents).Run

                    + new ClearAttackCellsS(idx_0, ents).Run
                    + new GetAttackMeleeCellsS(idx_0, ents).Run
                    + new GetCellsForAttackArcherS(idx_0, ents).Run

                    + new GetCellForArsonArcherS(idx_0, ents).Run;

                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    runAfterDoing += (Action)
                        new GetCellsForSetUnitS(playerT, idx_0, ents).Run;

                    for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++)
                    {
                        runAfterDoing += (Action)
                            new WorldCountsAmountBuildingInGameS(buildingT, playerT, idx_0, ents).Run;
                    }
                }
            }


            var truce = (Action)new TruceMS(ents).Run;

            var updateMove =
                (Action)new UpdatorMS(ents).Run

                + new FireUpdateMS(ents).Run
                + new RiverFertilizeAroundUpdateMS(ents).Run
                + new WorldDryFertilizerMS(ents).Run
                + new CitiesAddPeopleUpdateMS(ents).Run
                + new WorldMeltIceWallUpdateMS(ents).Run

                + new CloudUpdMS(ents).Run
                + new CloudFertilizeUpdMS(ents).Run


            #region Building

                + new WoodcutterExtractAdultForestUpdateMS(ents).Run
                + new FarmExtractFertilizeUpdateMS(ents).Run
                + new MineExtractUpdateMS(ents).Run
                + new IceWallGiveWaterUnitsUpdMS(ents).Run
                + new IceWallFertilizeAroundUpdateMS(ents).Run
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