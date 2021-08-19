using Assets.Scripts;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC;
using Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC.GiveTake;
using Assets.Scripts.ECS.Systems.Game.Master;
using Assets.Scripts.ECS.Systems.Game.Master.PunRPC;
using Leopotam.Ecs;
using System.Collections.Generic;

public sealed class GameMasterSystemManager : SystemAbstManager
{
    private static Dictionary<RpcMasterTypes, EcsSystems> _rpcSystems;

    internal static EcsSystems TruceSystems { get; private set; }

    internal static EcsSystems VisibilityUnitsSystems { get; private set; }
    internal static EcsSystems UpdateMotion { get; private set; }

    internal GameMasterSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld)
    {
        InitOnlySystems
            .Add(new InitGameMasterSystem());


        _rpcSystems = new Dictionary<RpcMasterTypes, EcsSystems>();

        _rpcSystems.Add(RpcMasterTypes.Build, new EcsSystems(gameWorld).Add(new BuilderMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.DestroyBuild, new EcsSystems(gameWorld).Add(new DestroyMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.Shift, new EcsSystems(gameWorld).Add(new ShiftUnitMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.Attack, new EcsSystems(gameWorld).Add(new AttackUnitMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.ConditionUnit, new EcsSystems(gameWorld).Add(new ConditionMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.Ready, new EcsSystems(gameWorld).Add(new ReadyMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreatorUnitMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.GetUnit, new EcsSystems(gameWorld).Add(new GetterUnitMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.SetUnit, new EcsSystems(gameWorld).Add(new SetterUnitMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.Upgrade, new EcsSystems(gameWorld).Add(new UpgradeMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.Fire, new EcsSystems(gameWorld).Add(new FireMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.SeedEnvironment, new EcsSystems(gameWorld).Add(new SeedingMasterSystem()));
        _rpcSystems.Add(RpcMasterTypes.CircularAttackKing, new EcsSystems(gameWorld).Add(new CircularAttackKingSystem()));

        var giveTakeSystems = new EcsSystems(gameWorld)
            .Add(new ArcherGiveTakeToolWeapMastSys())
            .Add(new PawnGiveTakeToolWeapMastSys());
        _rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, giveTakeSystems);


        VisibilityUnitsSystems = new EcsSystems(gameWorld)
            .Add(new VisibilityUnitsMasterSystem());


        UpdateMotion = new EcsSystems(gameWorld)
            .Add(new UpdatorMasterSystem());

        TruceSystems = new EcsSystems(gameWorld)
            .Add(new TruceMasterSystem());


        allGameSystems
            .Add(InitOnlySystems)
            .Add(RunOnlySystems)
            .Add(InitRunSystems)

            .Add(TruceSystems)
            .Add(VisibilityUnitsSystems)
            .Add(UpdateMotion);

        foreach (var system in _rpcSystems.Values) allGameSystems.Add(system);
    }

    internal static void RunRpcSystem(RpcMasterTypes rpcMasterType) => _rpcSystems[rpcMasterType].Run();
}