using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    public sealed class GameGenSysDataM : SystemAbstManager, IDisposable
    {
        internal RpcSys RpcGameSys { get; private set; }
        internal EcsSystems FillAvailCellsSyss { get; private set; }

        public GameGenSysDataM(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
        {
            new PhotonRpcViewC(true);

            RpcGameSys = PhotonRpcViewC.RpcView_GO.AddComponent<RpcSys>();


            var soundSystems = new EcsSystems(gameWorld)
                .Add(new SoundSystem());


            FillAvailCellsSyss = new EcsSystems(gameWorld)
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

                .Add(FillAvailCellsSyss)

                .Add(RpcGameSys);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(PhotonRpcViewC.RpcView_GO);
        }
    }
}