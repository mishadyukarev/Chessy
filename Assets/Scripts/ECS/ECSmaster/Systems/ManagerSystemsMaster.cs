using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        RunUpdateSystems
            .Add(new EconomyMasterSystem(eCSmanager), nameof(EconomyMasterSystem));

        SoloSystems
            .Add(new UpdateMotionMasterSystem(eCSmanager), nameof(UpdateMotionMasterSystem))
            .Add(new VisibilityUnitsMasterSystem(eCSmanager), nameof(VisibilityUnitsMasterSystem))
            .Add(new BuilderMasterSystem(eCSmanager), nameof(BuilderMasterSystem))
            .Add(new DestroyMasterSystem(eCSmanager), nameof(DestroyMasterSystem))
            .Add(new ShiftUnitMasterSystem(eCSmanager), nameof(ShiftUnitMasterSystem))
            .Add(new AttackUnitMasterSystem(eCSmanager), nameof(AttackUnitMasterSystem))
            .Add(new RelaxMasterSystem(eCSmanager), nameof(RelaxMasterSystem))
            .Add(new ProtectMasterSystem(eCSmanager), nameof(ProtectMasterSystem))
            .Add(new ReadyMasterSystem(eCSmanager), nameof(ReadyMasterSystem))
            .Add(new DonerMasterSystem(eCSmanager), nameof(DonerMasterSystem))
            .Add(new CreatorUnitMasterSystem(eCSmanager), nameof(CreatorUnitMasterSystem))
            .Add(new UpgradeUnitMasterSystem(eCSmanager), nameof(UpgradeUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(eCSmanager), nameof(GetterUnitMasterSystem))
            .Add(new MeltOreMasterSystem(eCSmanager), nameof(MeltOreMasterSystem))
            .Add(new SetterUnitMasterSystem(eCSmanager), nameof(SetterUnitMasterSystem));


        InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();
    }
}