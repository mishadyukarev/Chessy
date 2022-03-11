using Chessy.Game.System.Model;
using System;

namespace Chessy.Game
{
    public readonly struct SystemsModel
    {
        public SystemsModel(in EntitiesModel ents)
        {
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

            #endregion


            #region Environment

                + new MountainThrowHillsUpdMS(ents).Run

            #endregion


            #region Unit

                + new PawnExtractAdultForestMS(ents).Run
                + new ResumeUnitUpdMS(ents).Run
                + new UpdateHealingUnitMS(ents).Run
                + new UnitEatFoodUpdateS_M(ents).Run
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