using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;

public sealed class EntitiesGameMasterManager : EntitiesManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


    private EcsEntity _readyEnt;
    internal ref ActivatedComponent ReadyEnt_IsActivatedCom => ref _readyEnt.Get<ActivatedComponent>();


    private EcsEntity _donerEnt;
    internal ref ActivatedComponent DonerEnt_IsActivatedCom => ref _donerEnt.Get<ActivatedComponent>();


    private EcsEntity _fireEnt;
    internal ref FromToXyComponent FireEnt_FromToXyCom => ref _fireEnt.Get<FromToXyComponent>();


    private EcsEntity _upgradeEnt;
    internal ref UpgradeModTypeComponent UpgradeEnt_UpgradeTypeCom => ref _upgradeEnt.Get<UpgradeModTypeComponent>();
    internal ref XyCellComponent UpgradeEnt_XyCellCom => ref _upgradeEnt.Get<XyCellComponent>();
    internal ref BuildingTypeComponent UpgradeEnt_BuildingTypeCom => ref _upgradeEnt.Get<BuildingTypeComponent>();


    private EcsEntity _protectRelaxEnt;
    internal ref ProtectRelaxComponent ProtectRelaxEnt_ProtectRelaxCom => ref _protectRelaxEnt.Get<ProtectRelaxComponent>();
    internal ref XyCellComponent ProtectRelaxEnt_XyCellCom => ref _protectRelaxEnt.Get<XyCellComponent>();


    private EcsEntity _seedingEnt;
    internal ref EnvironmentTypesComponent SeedingEnt_EnvironmentTypesCom => ref _seedingEnt.Get<EnvironmentTypesComponent>();
    internal ref XyCellComponent SeedingEnt_XyCellCom => ref _seedingEnt.Get<XyCellComponent>();


    private EcsEntity _buildEnt;
    internal ref XyCellComponent BuildEnt_XyCellCom => ref _buildEnt.Get<XyCellComponent>();
    internal ref BuildingTypeComponent BuildEnt_BuildingTypeCom => ref _buildEnt.Get<BuildingTypeComponent>();


    private EcsEntity _destroyEnt;
    internal ref XyCellComponent DestroyEnt_XyCellCom => ref _destroyEnt.Get<XyCellComponent>();


    private EcsEntity _shiftEnt;
    internal ref FromToXyComponent ShiftEnt_FromToXyCom => ref _shiftEnt.Get<FromToXyComponent>();


    private EcsEntity _attackEnt;
    internal ref FromToXyComponent AttackEnt_FromToXyCom => ref _attackEnt.Get<FromToXyComponent>();


    private EcsEntity _creatorEnt;
    internal ref UnitTypeComponent CreatorEnt_UnitTypeCom => ref _creatorEnt.Get<UnitTypeComponent>();


    private EcsEntity _getterUnitEnt;
    internal ref UnitTypeComponent GetterUnitEnt_UnitTypeCom => ref _getterUnitEnt.Get<UnitTypeComponent>();


    private EcsEntity _settingUnitEnt;
    internal ref XyCellComponent SettingUnitEnt_XyCellCom => ref _settingUnitEnt.Get<XyCellComponent>();
    internal ref UnitTypeComponent SettingUnitEnt_UnitTypeCom => ref _settingUnitEnt.Get<UnitTypeComponent>();


    public EntitiesGameMasterManager(EcsWorld gameWorld)
    {

    }

    internal override void FillEntities(EcsWorld gameWorld)
    {
        base.FillEntities(gameWorld);





        _fromInfoEnt = gameWorld.NewEntity();

        _readyEnt = gameWorld.NewEntity();
        _donerEnt = gameWorld.NewEntity();

        _fireEnt = gameWorld.NewEntity();
        _upgradeEnt = gameWorld.NewEntity();
        _protectRelaxEnt = gameWorld.NewEntity();
        _seedingEnt = gameWorld.NewEntity();
        _buildEnt = gameWorld.NewEntity();
        _destroyEnt = gameWorld.NewEntity();
        _shiftEnt = gameWorld.NewEntity();
        _attackEnt = gameWorld.NewEntity();
        _creatorEnt = gameWorld.NewEntity();
        _getterUnitEnt = gameWorld.NewEntity();
        _settingUnitEnt = gameWorld.NewEntity();











        FromInfoEnt_FromInfoCom.StartFill();

        ReadyEnt_IsActivatedCom.StartFill();

        DonerEnt_IsActivatedCom.StartFill();


        FireEnt_FromToXyCom.StartFill();

        UpgradeEnt_UpgradeTypeCom.StartFill();
        UpgradeEnt_XyCellCom.StartFill();
        UpgradeEnt_BuildingTypeCom.StartFill();

        ProtectRelaxEnt_ProtectRelaxCom.StartFill();
        ProtectRelaxEnt_XyCellCom.StartFill();

        SeedingEnt_EnvironmentTypesCom.StartFill();
        SeedingEnt_XyCellCom.StartFill();

        BuildEnt_XyCellCom.StartFill();
        BuildEnt_BuildingTypeCom.StartFill();

        DestroyEnt_XyCellCom.StartFill();

        ShiftEnt_FromToXyCom.StartFill();

        AttackEnt_FromToXyCom.StartFill();

        CreatorEnt_UnitTypeCom.StartFill();

        GetterUnitEnt_UnitTypeCom.StartFill();

        SettingUnitEnt_XyCellCom.StartFill();
        SettingUnitEnt_UnitTypeCom.StartFill();
    }
}
