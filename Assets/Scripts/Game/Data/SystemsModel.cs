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



            runAfterDoing = (Action)
                //ClearingCells
                new ClearCellsForSetUnitS(ents).Run
                + new ClearEffectsKingS(ents).Run

                + new DestroyBuildingS(ents).Run
                + new AttackShieldS(ents).Run
                + new UnitGetEffectsS(ents).Run
                + new WorldClearTrailsS(ents).Run
                + new WoodcutterExtractGetCellsS(ents).Run
                + new FarmExtractGetCellsS(ents).Run


            #region World

                 + new WorldCountsAmountBuildingInGameS(ents).Run

            #endregion


            #region Unit

                //Moving
                + new UnitAttackUnitS(ents).Run
                + new UnitShiftS(ents).Run

                //Extract
                + new PawnGetExtractAdultForestS(ents).Run
                + new PawnExtractHillS(ents).Run
                + new GetDamageUnitsS(ents).Run
                + new UnitAbilityS(ents).Run



                //Attack
                + new ClearAttackCellsS(ents).Run
                + new GetAttackMeleeCellsS(ents).Run
                + new GetCellsForAttackArcherS(ents).Run

                + new VisibleUnitAndBuildingS(ents).Run
                + new GetCellsForShiftUnitS(ents).Run
                + new GetCellForArsonArcherS(ents).Run;

            #endregion

               


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