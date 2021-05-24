using Leopotam.Ecs;

internal sealed class SystemsMasterManager : SystemsManager
{
    internal EcsSystems RPCSystems;
    internal SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        RPCSystems = new EcsSystems(ecsWorld);
    }

    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        RunUpdateSystems
            .Add(new EconomyMasterSystem(eCSmanager), nameof(EconomyMasterSystem));

        RPCSystems
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
            .Add(new SetterUnitMasterSystem(eCSmanager), nameof(SetterUnitMasterSystem))
            .Add(new UniquePawnAbilityMasterSystem(eCSmanager), nameof(UniquePawnAbilityMasterSystem))
            .Add(new FireUpdatorMasterSystem(eCSmanager), nameof(FireUpdatorMasterSystem))
            .Add(new UpgradeBuildingMasterSystem(eCSmanager), nameof(UpgradeBuildingMasterSystem))
            .Add(new EconomyUpdatorMasterSystem(eCSmanager), nameof(EconomyUpdatorMasterSystem))
            .Add(new EnvironmentUpdatorMasterSystem(eCSmanager), nameof(EnvironmentUpdatorMasterSystem));


        InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();

        RPCSystems.ProcessInjects();

        RPCSystems.Init();
    }
}