using Leopotam.Ecs;
using Scripts.Common;
using System.Collections.Generic;

namespace Scripts.Game
{
    public sealed class GameMasterSystemManager : SystemAbstManager
    {
        private static Dictionary<RpcMasterTypes, EcsSystems> _rpcSystems;

        internal static EcsSystems TruceSystems { get; private set; }

        internal static EcsSystems UpdateMotion { get; private set; }

        public GameMasterSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
        {
            _rpcSystems = new Dictionary<RpcMasterTypes, EcsSystems>();

            _rpcSystems.Add(RpcMasterTypes.Build, new EcsSystems(gameWorld)
                .Add(new BuildMineMastSys())
                .Add(new BuildFarmMastSys())
                .Add(new BuildCityMastSys()));
            _rpcSystems.Add(RpcMasterTypes.DestroyBuild, new EcsSystems(gameWorld).Add(new DestroyMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.Shift, new EcsSystems(gameWorld).Add(new ShiftUnitMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.Attack, new EcsSystems(gameWorld).Add(new AttackMastSys()));
            _rpcSystems.Add(RpcMasterTypes.ConditionUnit, new EcsSystems(gameWorld).Add(new ConditionMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.Ready, new EcsSystems(gameWorld).Add(new ReadyMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreateUnitMastSys()));
            _rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.SetUnit, new EcsSystems(gameWorld).Add(new SetterUnitMastSys()));
            _rpcSystems.Add(RpcMasterTypes.UpgradeBuild, new EcsSystems(gameWorld).Add(new UpgradeMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.Fire, new EcsSystems(gameWorld).Add(new FireMastSys()));
            _rpcSystems.Add(RpcMasterTypes.SeedEnvironment, new EcsSystems(gameWorld).Add(new SeedingMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.CircularAttackKing, new EcsSystems(gameWorld).Add(new CircularAttackKingMastSys()));

            var giveTakeSystems = new EcsSystems(gameWorld)
                .Add(new ArcherGiveTakeToolWeapMastSys())
                .Add(new PawnGiveTakeToolWeapMastSys());
            _rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, giveTakeSystems);


            UpdateMotion = new EcsSystems(gameWorld)
                .Add(new UpdatorMastSys());

            TruceSystems = new EcsSystems(gameWorld)
                .Add(new TruceMasterSystem());


            allGameSystems
                .Add(TruceSystems)
                .Add(UpdateMotion);

            foreach (var system in _rpcSystems.Values) allGameSystems.Add(system);
        }

        internal static void RunRpcSystem(RpcMasterTypes rpcMasterType) => _rpcSystems[rpcMasterType].Run();
    }
}