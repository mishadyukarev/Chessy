﻿using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class DataSCreate
    {
        public DataSCreate(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;


            var runUpd = new EcsSystems(gameWorld);

            new RpcViewC(new GameObject("RpcView"));

            var rpcGameSys = new EcsSystems(gameWorld)
                .Add(RpcViewC.RpcView_GO.AddComponent<RpcSys>());


            var syncAbilities = new EcsSystems(gameWorld)
                .Add(new AbilSyncMasSys());


            var syncSound = new EcsSystems(gameWorld)
                .Add(new SoundS());


            var getAvailCells = new EcsSystems(gameWorld)
                .Add(new ClearAvailCellsS())
                .Add(new GetAttackKingCellsS())
                .Add(new GetAttackPawnCellsS())
                .Add(new GetAttackArcherCellsSs())
                .Add(new GetSetUnitCellsS())
                .Add(new GetShiftCellsS())
                .Add(new GetArsonCellsS());



            runUpd
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorS())
                .Add(syncAbilities)
                .Add(getAvailCells)
                .Add(syncSound);


            new DataSC(runUpd.Run);



            gameSysts
                .Add(runUpd)
                .Add(rpcGameSys);
        }
    }
}