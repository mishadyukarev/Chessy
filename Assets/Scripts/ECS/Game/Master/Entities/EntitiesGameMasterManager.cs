using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;

public sealed class EntitiesGameMasterManager : EntitiesManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


    private EcsEntity _masterRPCEntity;
    internal ref RPCMasterComponent RPCMasterEnt_RPCMasterCom => ref _masterRPCEntity.Get<RPCMasterComponent>();


    private EcsEntity _readyEnt;
    internal ref ActivatedComponent ReadyEnt_IsActivatedCom => ref _readyEnt.Get<ActivatedComponent>();


    private EcsEntity _fireEnt;
    internal ref FromToXyComponent FireEnt_FromToXyCom => ref _fireEnt.Get<FromToXyComponent>();


    private EcsEntity _upgradeEnt;
    internal ref UpgradeTypeComponent UpgradeEnt_UpgradeTypeCom => ref _upgradeEnt.Get<UpgradeTypeComponent>();
    internal ref XyCellComponent UpgradeEnt_XyCellCom => ref _upgradeEnt.Get<XyCellComponent>();
    internal ref BuildingTypeComponent UpgradeEnt_BuildingTypeCom => ref _upgradeEnt.Get<BuildingTypeComponent>();


    private EcsEntity _protectRelaxEnt;
    internal ref ProtectRelaxComponent ProtectRelaxEnt_ProtectRelaxCom => ref _protectRelaxEnt.Get<ProtectRelaxComponent>();
    internal ref XyCellComponent ProtectRelaxEnt_XyCellCom => ref _protectRelaxEnt.Get<XyCellComponent>();


    private EcsEntity _seedingEnt;
    internal ref EnvironmentTypesComponent SeedingEnt_EnvironmentTypesCom => ref _seedingEnt.Get<EnvironmentTypesComponent>();
    internal ref XyCellComponent SeedingEnt_XyCellCom => ref _seedingEnt.Get<XyCellComponent>();


    public EntitiesGameMasterManager(EcsWorld gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity();
        _masterRPCEntity = gameWorld.NewEntity();

        _readyEnt = gameWorld.NewEntity();
        _fireEnt = gameWorld.NewEntity();
        _upgradeEnt = gameWorld.NewEntity();
        _protectRelaxEnt = gameWorld.NewEntity();
        _seedingEnt = gameWorld.NewEntity();
    }

    internal override void FillEntities()
    {
        base.FillEntities();

        FromInfoEnt_FromInfoCom.StartFill();
        _masterRPCEntity.Replace(new RPCMasterComponent());


        ReadyEnt_IsActivatedCom.StartFill();
        FireEnt_FromToXyCom.StartFill();

        UpgradeEnt_UpgradeTypeCom.StartFill();
        UpgradeEnt_XyCellCom.StartFill();
        UpgradeEnt_BuildingTypeCom.StartFill();

        ProtectRelaxEnt_ProtectRelaxCom.StartFill();
        ProtectRelaxEnt_XyCellCom.StartFill();

        SeedingEnt_EnvironmentTypesCom.StartFill();
        SeedingEnt_XyCellCom.StartFill();
    }
}
