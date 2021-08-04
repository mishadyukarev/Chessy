﻿using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.Master.Systems.PunRPC;
using Assets.Scripts.ECS.Systems.Game.Master.PunRPC;
using Leopotam.Ecs;

public sealed class SysDataGameMasterManager : SystemAbstManager
{
    internal static EcsSystems RpcSystems { get; private set; }
    internal static EcsSystems VisibilityUnitsSystems { get; private set; }
    internal static EcsSystems UpdateMotion { get; private set; }

    internal static EcsSystems CircularAttackKingSystems { get; private set; }



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


    private EcsEntity _circularAttackKingEnt;
    internal ref XyCellComponent CircularAttackKingEnt_XyCellCom => ref _settingUnitEnt.Get<XyCellComponent>();

    internal SysDataGameMasterManager(EcsWorld gameWorld) : base(gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());


        _creatorEnt = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent());

        _getterUnitEnt = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent());

        _settingUnitEnt = gameWorld.NewEntity()
            .Replace(new UnitTypeComponent())
            .Replace(new XyCellComponent(new int[2]));









        _readyEnt = gameWorld.NewEntity()
            .Replace(new ActivatedComponent());

        _donerEnt = gameWorld.NewEntity()
            .Replace(new ActivatedComponent());


        _fireEnt = gameWorld.NewEntity()
            .Replace(new FromToXyComponent(new int[2], new int[2]));


        _upgradeEnt = gameWorld.NewEntity()
            .Replace(new UpgradeModTypeComponent())
            .Replace(new XyCellComponent(new int[2]))
            .Replace(new BuildingTypeComponent());


        _protectRelaxEnt = gameWorld.NewEntity()
            .Replace(new ProtectRelaxComponent())
            .Replace(new XyCellComponent(new int[2]));


        _seedingEnt = gameWorld.NewEntity()
            .Replace(new EnvironmentTypesComponent())
            .Replace(new XyCellComponent(new int[2]));



        _buildEnt = gameWorld.NewEntity()
            .Replace(new XyCellComponent(new int[2]))
            .Replace(new BuildingTypeComponent());


        _destroyEnt = gameWorld.NewEntity()
            .Replace(new XyCellComponent(new int[2]));

        _shiftEnt = gameWorld.NewEntity()
            .Replace(new FromToXyComponent(new int[2], new int[2]));


        _attackEnt = gameWorld.NewEntity()
            .Replace(new FromToXyComponent(new int[2], new int[2]));

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