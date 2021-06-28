using Assets.Scripts;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Leopotam.Ecs;

public sealed class SystemsGameMasterManager : SystemsManager
{
    internal EcsSystems RPCSystems;

    internal void CreateSystems(EcsWorld ecsWorld)
    {
        RunUpdateSystems = new EcsSystems(ecsWorld)
            .Add(new EconomyMasterSystem(), nameof(EconomyMasterSystem));

        RPCSystems = new EcsSystems(ecsWorld)
            .Add(new UpdateMotionMasterSystem(), nameof(UpdateMotionMasterSystem))
            .Add(new VisibilityUnitsMasterSystem(), nameof(VisibilityUnitsMasterSystem))
            .Add(new BuilderMasterSystem(), nameof(BuilderMasterSystem))
            .Add(new DestroyMasterSystem(), nameof(DestroyMasterSystem))
            .Add(new ShiftUnitMasterSystem(), nameof(ShiftUnitMasterSystem))
            .Add(new AttackUnitMasterSystem(), nameof(AttackUnitMasterSystem))
            .Add(new RelaxMasterSystem(), nameof(RelaxMasterSystem))
            .Add(new ProtectMasterSystem(), nameof(ProtectMasterSystem))
            .Add(new ReadyMasterSystem(), nameof(ReadyMasterSystem))
            .Add(new DonerMasterSystem(), nameof(DonerMasterSystem))
            .Add(new CreatorUnitMasterSystem(), nameof(CreatorUnitMasterSystem))
            .Add(new UpgradeUnitMasterSystem(), nameof(UpgradeUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(), nameof(GetterUnitMasterSystem))
            .Add(new MeltOreMasterSystem(), nameof(MeltOreMasterSystem))
            .Add(new SetterUnitMasterSystem(), nameof(SetterUnitMasterSystem))
            .Add(new UniquePawnAbilityMasterSystem(), nameof(UniquePawnAbilityMasterSystem))
            .Add(new FireUpdatorMasterSystem(), nameof(FireUpdatorMasterSystem))
            .Add(new UpgradeBuildingMasterSystem(), nameof(UpgradeBuildingMasterSystem))
            .Add(new EconomyUpdatorMasterSystem(), nameof(EconomyUpdatorMasterSystem))
            .Add(new FertilizeUpdatorMasterSystem(), nameof(FertilizeUpdatorMasterSystem))
            .Add(new TruceMasterSystem(), nameof(TruceMasterSystem))
            .Add(new UpgradeMasterSystem(), nameof(UpgradeMasterSystem));
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

        RPCSystems.ProcessInjects();
    }

    internal override void Init()
    {
        base.Init();

        RPCSystems.Init();
    }
}