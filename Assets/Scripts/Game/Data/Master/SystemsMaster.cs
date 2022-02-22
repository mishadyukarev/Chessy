using System;

namespace Game.Game
{
    public readonly struct SystemsMaster
    {
        public SystemsMaster(in EntitiesModel ents)
        {
            ents.RpcPoolEs.AttackAction = new AttackMS(ents).Attack;


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