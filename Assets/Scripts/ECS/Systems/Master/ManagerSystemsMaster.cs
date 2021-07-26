using Assets.Scripts;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Leopotam.Ecs;

public sealed class SystemsGameMasterManager : SystemsManager
{
    internal EcsSystems RpcSystems { get; private set; }
    internal EcsSystems VisibilityUnitsSystems { get; private set; }


    internal SystemsGameMasterManager(EcsWorld gameWorld) : base(gameWorld)
    {
        RpcSystems = new EcsSystems(gameWorld)
            .Add(new UpdateMotionMasterSystem(), nameof(UpdateMotionMasterSystem))
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

        VisibilityUnitsSystems = new EcsSystems(gameWorld)
            .Add(new VisibilityUnitsMasterSystem(), nameof(VisibilityUnitsMasterSystem));
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

        RpcSystems.ProcessInjects();
        VisibilityUnitsSystems.ProcessInjects();
    }

    internal override void Init()
    {
        base.Init();

        RpcSystems.Init();
        VisibilityUnitsSystems.Init();
    }
}