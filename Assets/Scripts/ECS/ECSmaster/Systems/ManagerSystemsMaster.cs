using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        SoloSystems
            .Add(new RefreshMasterSystem(eCSmanager), nameof(RefreshMasterSystem))
            .Add(new VisibilityUnitsMasterSystem(eCSmanager), nameof(VisibilityUnitsMasterSystem));

        InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();
    }
}