using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemsMaster
    {
        readonly Dictionary<SystemDataMasterTypes, Action> _systems;


        public SystemsMaster(in Entities ents)
        {
            _systems = new Dictionary<SystemDataMasterTypes, Action>();


            var action =
                (Action)new UpdatorMS(ents).Run

                + new UpdateFireMS(ents).Run
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
                + new UpdateThirstyMS(ents).Run
                + new PawnExtractHillUpdateMS(ents).Run

                + new UpdTryFireAroundHellMS(ents).Run
                + new UpdAttackFromWaterHellMS(ents).Run

                + new UpdGiveWaterCloudScowyMS(ents).Run

                + new CamelShiftUpdateMS(ents).Run
                + new CamelSpawnUpdateMS(ents).Run

            #endregion

                + new UpdTryInvokeTruceMS(this, ents).Run;


            _systems.Add(SystemDataMasterTypes.UpdateMove, action);

            action = new TruceMS(ents).Run;
            _systems.Add(SystemDataMasterTypes.Truce, action);
        }

        public void InvokeRun(SystemDataMasterTypes mastDataSys) => _systems[mastDataSys].Invoke();
    }
}