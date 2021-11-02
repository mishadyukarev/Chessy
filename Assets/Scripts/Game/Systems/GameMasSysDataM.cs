using Leopotam.Ecs;
using Scripts.Common;
using System.Collections.Generic;

namespace Scripts.Game
{
    public sealed class GameMasSysDataM : SystemAbstManager
    {
        public GameMasSysDataM(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
        {
            var rpcSystems = new Dictionary<RpcMasterTypes, EcsSystems>();

            rpcSystems.Add(RpcMasterTypes.Build, new EcsSystems(gameWorld)
                .Add(new BuildMineMastSys())
                .Add(new BuildFarmMastSys())
                .Add(new BuildCityMastSys()));
            rpcSystems.Add(RpcMasterTypes.DestroyBuild, new EcsSystems(gameWorld).Add(new DestroyMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Shift, new EcsSystems(gameWorld).Add(new ShiftUnitMasSys()));
            rpcSystems.Add(RpcMasterTypes.Attack, new EcsSystems(gameWorld).Add(new AttackMastSys()));
            rpcSystems.Add(RpcMasterTypes.ConditionUnit, new EcsSystems(gameWorld).Add(new ConditionMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Ready, new EcsSystems(gameWorld).Add(new ReadyMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMastSys()));
            rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreateUnitMastSys()));
            rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.SetUnit, new EcsSystems(gameWorld).Add(new SetterUnitMastSys()));
            rpcSystems.Add(RpcMasterTypes.UpgradeBuild, new EcsSystems(gameWorld).Add(new UpgradeMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Fire, new EcsSystems(gameWorld).Add(new FireMastSys()));
            rpcSystems.Add(RpcMasterTypes.SeedEnvironment, new EcsSystems(gameWorld).Add(new SeedingMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.CircularAttackKing, new EcsSystems(gameWorld).Add(new CircularAttackKingMastSys()));
            rpcSystems.Add(RpcMasterTypes.UpgradeUnit, new EcsSystems(gameWorld).Add(new UpgradeUnitMasSys()));
            rpcSystems.Add(RpcMasterTypes.OldToNewUnit, new EcsSystems(gameWorld).Add(new ScoutOldNewSys()));
            rpcSystems.Add(RpcMasterTypes.BonusNearUnitKing, new EcsSystems(gameWorld).Add(new BonusNearUnitKingMasSys()));
            rpcSystems.Add(RpcMasterTypes.PickUpgrade, new EcsSystems(gameWorld).Add(new PickUpgMasSys()));

            var giveTakeSystems = new EcsSystems(gameWorld)
                .Add(new GiveTakeTWMasSys());
            rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, giveTakeSystems);


            var updateMotion = new EcsSystems(gameWorld)
                .Add(new UpdatorMastSys())
                .Add(new ExtractBuildUpdMasSys())
                .Add(new FireUpdMasSys())
                .Add(new CloudUpdMasSys())
                .Add(new ThirstyUpdMasSys())
                .Add(new RelaxUpdMasSys())
                .Add(new HungryUpdMasSys());

            var truceSystems = new EcsSystems(gameWorld)
                .Add(new TruceMasterSystem());


            allGameSystems
                .Add(truceSystems)
                .Add(updateMotion);

            foreach (var system in rpcSystems.Values) allGameSystems.Add(system);


            var systs = new Dictionary<MastDataSysTypes, EcsSystems>();
            systs.Add(MastDataSysTypes.Update, updateMotion);
            systs.Add(MastDataSysTypes.Truce, truceSystems);

            new MastDataSysContainer(systs, rpcSystems);
        }
    }
}