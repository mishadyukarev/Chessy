using Assets.Scripts;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.ECS.Systems.Game.Master;
using Assets.Scripts.ECS.Systems.Game.Master.PunRPC;
using Leopotam.Ecs;

public sealed class GameMasterSystemManager : SystemAbstManager
{
    internal static EcsSystems BuilderSystems { get; private set; }
    internal static EcsSystems DestroySystems { get; private set; }
    internal static EcsSystems ShiftUnitSystems { get; private set; }
    internal static EcsSystems AttackUnitSystems { get; private set; }
    internal static EcsSystems ConditionUnitSystems { get; private set; }
    internal static EcsSystems ReadySystems { get; private set; }
    internal static EcsSystems DonerSystems { get; private set; }
    internal static EcsSystems CreatorUnitSystems { get; private set; }
    internal static EcsSystems GetterUnitSystems { get; private set; }
    internal static EcsSystems MeltOreSystems { get; private set; }
    internal static EcsSystems SetterUnitSystems { get; private set; }
    internal static EcsSystems TruceSystems { get; private set; }
    internal static EcsSystems UpgradeSystems { get; private set; }
    internal static EcsSystems FireSystems { get; private set; }
    internal static EcsSystems SeedingSystems { get; private set; }


    internal static EcsSystems VisibilityUnitsSystems { get; private set; }
    internal static EcsSystems UpdateMotion { get; private set; }

    internal static EcsSystems CircularAttackKingSystems { get; private set; }


    internal GameMasterSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld)
    {
        InitSystems
            .Add(new MainMasterSystem());


        BuilderSystems = new EcsSystems(gameWorld)
            .Add(new BuilderMasterSystem());


        DestroySystems = new EcsSystems(gameWorld)
            .Add(new DestroyMasterSystem());


        ShiftUnitSystems = new EcsSystems(gameWorld)
            .Add(new ShiftUnitMasterSystem());


        AttackUnitSystems = new EcsSystems(gameWorld)
            .Add(new AttackUnitMasterSystem());


        ConditionUnitSystems = new EcsSystems(gameWorld)
            .Add(new ConditionMasterSystem());


        ReadySystems = new EcsSystems(gameWorld)
            .Add(new ReadyMasterSystem());


        DonerSystems = new EcsSystems(gameWorld)
            .Add(new DonerMasterSystem());


        CreatorUnitSystems = new EcsSystems(gameWorld)
            .Add(new CreatorUnitMasterSystem());


        GetterUnitSystems = new EcsSystems(gameWorld)
            .Add(new GetterUnitMasterSystem());


        MeltOreSystems = new EcsSystems(gameWorld)
            .Add(new MeltOreMasterSystem());


        SetterUnitSystems = new EcsSystems(gameWorld)
            .Add(new SetterUnitMasterSystem());


        TruceSystems = new EcsSystems(gameWorld)
            .Add(new TruceMasterSystem());


        UpgradeSystems = new EcsSystems(gameWorld)
            .Add(new UpgradeMasterSystem());


        FireSystems = new EcsSystems(gameWorld)
            .Add(new FireMasterSystem());


        SeedingSystems = new EcsSystems(gameWorld)
            .Add(new SeedingMasterSystem());



        VisibilityUnitsSystems = new EcsSystems(gameWorld)
            .Add(new VisibilityUnitsMasterSystem());


        UpdateMotion = new EcsSystems(gameWorld)
            .Add(new UpdatorMasterSystem());


        CircularAttackKingSystems = new EcsSystems(gameWorld)
            .Add(new CircularAttackKingSystem());


        allGameSystems
            .Add(InitSystems)
            .Add(RunSystems)

            .Add(BuilderSystems)
            .Add(DestroySystems)
            .Add(ShiftUnitSystems)
            .Add(AttackUnitSystems)
            .Add(ConditionUnitSystems)
            .Add(ReadySystems)
            .Add(DonerSystems)
            .Add(CreatorUnitSystems)
            .Add(GetterUnitSystems)
            .Add(MeltOreSystems)
            .Add(SetterUnitSystems)
            .Add(TruceSystems)
            .Add(UpgradeSystems)
            .Add(FireSystems)
            .Add(SeedingSystems)
            .Add(VisibilityUnitsSystems)
            .Add(UpdateMotion)
            .Add(CircularAttackKingSystems);
    }
}