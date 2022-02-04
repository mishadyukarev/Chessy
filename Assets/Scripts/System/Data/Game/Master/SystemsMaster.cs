﻿using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemsMaster
    {
        readonly Dictionary<SystemDataMasterTypes, Action> _systems;
        readonly Dictionary<RpcMasterTypes, Action> _rpcSysts;


        public SystemsMaster(in Entities ents)
        {
            _systems = new Dictionary<SystemDataMasterTypes, Action>();
            _rpcSysts = new Dictionary<RpcMasterTypes, Action>();


            var action =
                (Action)new UpdatorMS(this, ents).Run

                + new UpdExtractWoodcutterMS(ents).Run
                + new UpdExtractFarmMS(ents).Run
                + new UpdExtractMineMS(ents).Run
                + new UpdIceWallGiveWaterMS(ents).Run
                + new UpdIceWallFertilizeAroundMS(ents).Run

                + new UpdateFireMS(ents).Run
                + new CloudUpdMS(ents).Run
                + new UpdateIceWallMS(ents).Run

            #region Unit

                + new UpdateExtractUnitMS(ents).Run
                + new ResumeUnitUpdMS(ents).Run
                + new UpdateHealingUnitMS(ents).Run
                + new UpdateHungryMS(ents).Run
                + new UpdateThirstyMS(ents).Run

                + new UpdTryFireAroundHellMS(ents).Run
                + new UpdAttackFromWaterHellMS(ents).Run

                + new UpdGiveWaterCloudScowyMS(ents).Run

            #endregion

                + new UpdateCamelShiftMS(ents).Run
                + new UpdateSpawnCamelMS(ents).Run;
            _systems.Add(SystemDataMasterTypes.UpdateMove, action);

            action = new TruceMS(ents).Run;
            _systems.Add(SystemDataMasterTypes.Truce, action);
        }

        public void InvokeRun(SystemDataMasterTypes mastDataSys) => _systems[mastDataSys].Invoke();
        public void InvokeRun(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Invoke();
            else throw new System.Exception();
        }
    }
}