using Assets.Scripts;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.ECS.Systems.Game.Master.PunRPC;
using Leopotam.Ecs;

public sealed class SysGameMasterManager : SystemAbstManager
{
    internal static EcsSystems RpcSystems { get; private set; }
    internal static EcsSystems VisibilityUnitsSystems { get; private set; }
    internal static EcsSystems UpdateMotion { get; private set; }

    internal static EcsSystems CircularAttackKingSystems { get; private set; }


    internal SysGameMasterManager(EcsWorld gameWorld) : base(gameWorld)
    {
        RpcSystems = new EcsSystems(gameWorld)
            .Add(new BuilderMasterSystem(), nameof(BuilderMasterSystem))
            .Add(new DestroyMasterSystem(), nameof(DestroyMasterSystem))
            .Add(new ShiftUnitMasterSystem(), nameof(ShiftUnitMasterSystem))
            .Add(new AttackUnitMasterSystem(), nameof(AttackUnitMasterSystem))
            .Add(new ConditionMasterSystem(), nameof(ConditionMasterSystem))
            .Add(new ReadyMasterSystem(), nameof(ReadyMasterSystem))
            .Add(new DonerMasterSystem(), nameof(DonerMasterSystem))
            .Add(new CreatorUnitMasterSystem(), nameof(CreatorUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(), nameof(GetterUnitMasterSystem))
            .Add(new MeltOreMasterSystem(), nameof(MeltOreMasterSystem))
            .Add(new SetterUnitMasterSystem(), nameof(SetterUnitMasterSystem))
            .Add(new TruceMasterSystem(), nameof(TruceMasterSystem))
            .Add(new UpgradeMasterSystem(), nameof(UpgradeMasterSystem))
            .Add(new FireMasterSystem(), nameof(FireMasterSystem))
            .Add(new SeedingMasterSystem(), nameof(SeedingMasterSystem));

        VisibilityUnitsSystems = new EcsSystems(gameWorld)
            .Add(new VisibilityUnitsMasterSystem(), nameof(VisibilityUnitsMasterSystem));

        UpdateMotion = new EcsSystems(gameWorld)
            .Add(new ExtractionUpdatorMasterSystem())
            .Add(new FireUpdatorMasterSystem())
            .Add(new UpdateMotionMasterSystem());


        CircularAttackKingSystems = new EcsSystems(gameWorld)
            .Add(new CircularAttackKingSystem(), nameof(CircularAttackKingSystem));

    }

    internal override void Init()
    {
        base.Init();

        RpcSystems.Init();
        VisibilityUnitsSystems.Init();
        UpdateMotion.Init();
        CircularAttackKingSystems.Init();
    }
}