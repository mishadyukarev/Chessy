﻿using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class DataS
    {
        public DataS(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;


            var runUpdate = new EcsSystems(gameWorld);

            new RpcViewC(new GameObject("RpcView"));

            var rpcGameSys = new EcsSystems(gameWorld)
                .Add(RpcViewC.RpcView_GO.AddComponent<RpcSys>());


            var syncAbilities = new EcsSystems(gameWorld)
                .Add(new AbilSyncMasSys());


            var fillAvailCells = new EcsSystems(gameWorld)
                 .Add(new SoundSystem())
                .Add(new ClearAvailCellsSys())
                .Add(new FillCellsForAttackKingSys())
                .Add(new FillCellsForAttackPawnSys())
                .Add(new FillCellsForAttackRookSys())
                .Add(new FillCellsForAttackBishopSys())
                .Add(new FillCellsForSetUnitSys())
                .Add(new FillCellsForShiftSys())
                .Add(new FillCellsArsonSys());


            var eventExecuters = new EcsSystems(gameWorld)
                .Add(new CenterEventUISys())
                .Add(new LeftCityEventUISys())
                .Add(new LeftEnvEventUISys())
                .Add(new DownEventUISys())
                .Add(new RightUnitEventUISys());


            runUpdate
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorS())
                .Add(syncAbilities)
                .Add(fillAvailCells)
                .Add(eventExecuters);


            new DataC(runUpdate.Run);
            


            gameSysts
                .Add(runUpdate)
                .Add(rpcGameSys);
        }
    }
}