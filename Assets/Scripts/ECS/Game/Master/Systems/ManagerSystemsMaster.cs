using Assets.Scripts;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Leopotam.Ecs;

public sealed class SystemsGameMasterManager : SystemsManager
{
    private EcsSystems _rpcSystems;

    internal EcsSystems RpcSystems => _rpcSystems;

    internal override void CreateSystems(EcsWorld gameWorld)
    {
        base.CreateSystems(gameWorld);

        RunUpdateSystems = new EcsSystems(gameWorld)
            .Add(new EconomyMasterSystem(), nameof(EconomyMasterSystem));

        _rpcSystems = new EcsSystems(gameWorld)
            .Add(new UpdateMotionMasterSystem(), nameof(UpdateMotionMasterSystem))
            .Add(new VisibilityUnitsMasterSystem(), nameof(VisibilityUnitsMasterSystem))
            .Add(new BuilderMasterSystem(), nameof(BuilderMasterSystem))
            .Add(new DestroyMasterSystem(), nameof(DestroyMasterSystem))
            .Add(new ShiftUnitMasterSystem(), nameof(ShiftUnitMasterSystem))
            .Add(new AttackUnitMasterSystem(), nameof(AttackUnitMasterSystem))
            .Add(new ProtectRelaxMasterSystem(), nameof(ProtectRelaxMasterSystem))
            .Add(new ReadyMasterSystem(), nameof(ReadyMasterSystem))
            .Add(new DonerMasterSystem(), nameof(DonerMasterSystem))
            .Add(new CreatorUnitMasterSystem(), nameof(CreatorUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(), nameof(GetterUnitMasterSystem))
            .Add(new MeltOreMasterSystem(), nameof(MeltOreMasterSystem))
            .Add(new SetterUnitMasterSystem(), nameof(SetterUnitMasterSystem))
            .Add(new FireUpdatorMasterSystem(), nameof(FireUpdatorMasterSystem))
            .Add(new ExtractionUpdatorMasterSystem(), nameof(ExtractionUpdatorMasterSystem))
            .Add(new TruceMasterSystem(), nameof(TruceMasterSystem))
            .Add(new UpgradeMasterSystem(), nameof(UpgradeMasterSystem))
            .Add(new FireMasterSystem(), nameof(FireMasterSystem))
            .Add(new SeedingMasterSystem(), nameof(SeedingMasterSystem));
    }

    internal override void DestroySystems()
    {
        base.DestroySystems();

        if (!_isStartedFilling)
        {
            RpcSystems.Destroy();
        }
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

        RpcSystems.ProcessInjects();
    }

    internal override void Init()
    {
        base.Init();

        RpcSystems.Init();
    }
}