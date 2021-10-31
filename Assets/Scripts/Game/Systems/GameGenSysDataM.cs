using Leopotam.Ecs;
using Scripts.Common;
using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public sealed class GameGenSysDataM : SystemAbstManager, IDisposable
    {
        public GameGenSysDataM(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
        {
            new PhotonRpcViewC(true);

            var rpcGameSys = new EcsSystems(gameWorld)
                .Add(PhotonRpcViewC.RpcView_GO.AddComponent<RpcSys>());


            var soundSystems = new EcsSystems(gameWorld)
                .Add(new SoundSystem());


            var fillAvailCellsSyss = new EcsSystems(gameWorld)
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


            InitOnlySystems
                .Add(eventExecuters);


            UpdateOnlySystems
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorSystem())

                .Add(soundSystems)

                .Add(fillAvailCellsSyss)

                .Add(rpcGameSys);


            var systems = new Dictionary<GeneralDataSysTypes, EcsSystems>();
            systems.Add(GeneralDataSysTypes.Rpc, rpcGameSys);
            systems.Add(GeneralDataSysTypes.FillAvailCells, fillAvailCellsSyss);
            new GeneralSysContainer(systems);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(PhotonRpcViewC.RpcView_GO);
        }
    }
}