using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class DataSCreating
    {
        public DataSCreating(EcsSystems gameSysts)
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
                .Add(new FillCellsSetUnitS())
                .Add(new FillCellsForShiftSys())
                .Add(new FillCellsArsonSys());



            runUpdate
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorS())
                .Add(syncAbilities)
                .Add(fillAvailCells);


            new DataC(runUpdate.Run);



            gameSysts
                .Add(runUpdate)
                .Add(rpcGameSys);
        }
    }
}