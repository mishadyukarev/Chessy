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
            .Add(new AttackUnitMasterSystem(eCSmanager), nameof(AttackUnitMasterSystem));
            

        InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();
    }
}