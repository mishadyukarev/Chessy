using Leopotam.Ecs;
using Scripts.Common;
using System.Collections.Generic;

namespace Scripts.Game
{
    public sealed class GameMasSysDataM : SystemAbstManager
    {
        private static Dictionary<RpcMasterTypes, EcsSystems> _rpcSystems;

        internal static EcsSystems TruceSystems { get; private set; }

        internal static EcsSystems UpdateMotion { get; private set; }

        public GameMasSysDataM(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
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
            _rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMastSys()));
            _rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreateUnitMastSys()));
            _rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.SetUnit, new EcsSystems(gameWorld).Add(new SetterUnitMastSys()));
            _rpcSystems.Add(RpcMasterTypes.UpgradeBuild, new EcsSystems(gameWorld).Add(new UpgradeMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.Fire, new EcsSystems(gameWorld).Add(new FireMastSys()));
            _rpcSystems.Add(RpcMasterTypes.SeedEnvironment, new EcsSystems(gameWorld).Add(new SeedingMasterSystem()));
            _rpcSystems.Add(RpcMasterTypes.CircularAttackKing, new EcsSystems(gameWorld).Add(new CircularAttackKingMastSys()));
            _rpcSystems.Add(RpcMasterTypes.UpgradeUnit, new EcsSystems(gameWorld).Add(new UpgradeUnitMasSys()));
            _rpcSystems.Add(RpcMasterTypes.OldToNewUnit, new EcsSystems(gameWorld).Add(new OldNewScoutSys()));
            _rpcSystems.Add(RpcMasterTypes.BonusNearUnitKing, new EcsSystems(gameWorld).Add(new BonusNearUnitKingMasSys()));

            var giveTakeSystems = new EcsSystems(gameWorld)
                .Add(new GiveTakeTWMasSys());
            _rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, giveTakeSystems);


            UpdateMotion = new EcsSystems(gameWorld)
                .Add(new UpdatorMastSys())
                .Add(new FireUpdMasSys())
                .Add(new CloudUpdMasSys());

            TruceSystems = new EcsSystems(gameWorld)
                .Add(new TruceMasterSystem());


            allGameSystems
                .Add(TruceSystems)
                .Add(UpdateMotion);

            foreach (var system in _rpcSystems.Values) allGameSystems.Add(system);
        }

        public static void RunRpcSystem(RpcMasterTypes rpcMasterType)
        {
            if (_rpcSystems.ContainsKey(rpcMasterType)) _rpcSystems[rpcMasterType].Run();
            else throw new System.Exception();
        }
    }
}