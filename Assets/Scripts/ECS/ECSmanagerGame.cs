using Leopotam.Ecs;
using System;
using static Main;

internal sealed class ECSmanagerGame
{
    private EcsWorld _gameWorld;

    private EntitiesGeneralManager _entitiesGeneralManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private SystemsMasterManager _systemsMasterManager;
    private EntitiesMasterManager _entitiesMasterManager;

    private SystemsOtherManager _systemsOtherManager;
    private EntitiesOtherManager _entitiesOtherManager;


    internal EntitiesGeneralManager EntitiesGeneralManager => _entitiesGeneralManager;
    internal SystemsGeneralManager SystemsGeneralManager => _systemsGeneralManager;

    internal EntitiesMasterManager EntitiesMasterManager => _entitiesMasterManager;
    internal SystemsMasterManager SystemsMasterManager => _systemsMasterManager;

    internal EntitiesOtherManager EntitiesOtherManager => _entitiesOtherManager;
    internal SystemsOtherManager SystemsOtherManager => _systemsOtherManager;



    internal ECSmanagerGame(CanvasGameManager canvasGameManager, StartValuesGameConfig startValuesGameConfig, CellManager cellManager, Names names)
    {
        _gameWorld = new EcsWorld();

        _entitiesGeneralManager = new EntitiesGeneralManager(_gameWorld, canvasGameManager, startValuesGameConfig);
        _systemsGeneralManager = new SystemsGeneralManager(_gameWorld);
        _systemsGeneralManager.CreateSystems(this, cellManager, names);

        _entitiesMasterManager = new EntitiesMasterManager(_gameWorld, this);
        _systemsMasterManager = new SystemsMasterManager(_gameWorld);
        _systemsMasterManager.CreateSystems(this);

        _entitiesOtherManager = new EntitiesOtherManager(_gameWorld);
        _systemsOtherManager = new SystemsOtherManager(_gameWorld);
        _systemsOtherManager.CreateSystems(this);
    }

    internal void InitAndProcessInjectsSystems()
    {
        _systemsGeneralManager.InitAndProcessInjectsSystems();
        _systemsMasterManager.InitAndProcessInjectsSystems();
        _systemsOtherManager.InitAndProcessInjectsSystems();
    }

    internal void FillEntities(ObjectPoolGame objectPoolGame, CanvasGameManager canvasGameManager, StartValuesGameConfig startValuesGameConfig)
    {
        _entitiesGeneralManager.FillEntities(objectPoolGame, canvasGameManager, startValuesGameConfig);
    }

    public void OwnUpdate()
    {
        _systemsGeneralManager.Update();

        if (Instance.IsMasterClient) _systemsMasterManager.Update();
        else _systemsOtherManager.Update();
    }
    internal void FixedUpdate()
    {
        _systemsGeneralManager.FixedUpdate();

        if (Instance.IsMasterClient) _systemsMasterManager.FixedUpdate();
        else _systemsOtherManager.FixedUpdate();
    }
}
