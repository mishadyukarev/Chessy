using Leopotam.Ecs;
using System;
using static Main;

internal sealed class ECSmanager
{
    private EcsWorld _gameWorld;

    private EntitiesGeneralManager _entitiesGeneralManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private SystemsMasterManager _systemsMasterManager;
    private EntitiesMasterManager _entitiesMasterManager;

    private SystemsOtherManager _systemsOtherManager;
    private EntitiesOtherManager _entitiesOtherManager;

    private CellManager _cellManager;
    protected EconomyManager _economyManager;


    internal EntitiesGeneralManager EntitiesGeneralManager => _entitiesGeneralManager;
    internal SystemsGeneralManager SystemsGeneralManager => _systemsGeneralManager;

    internal EntitiesMasterManager EntitiesMasterManager => _entitiesMasterManager;
    internal SystemsMasterManager SystemsMasterManager => _systemsMasterManager;

    internal EntitiesOtherManager EntitiesOtherManager => _entitiesOtherManager;
    internal SystemsOtherManager SystemsOtherManager => _systemsOtherManager;

    internal CellManager CellManager => _cellManager;
    internal EconomyManager EconomyManager => _economyManager;



    internal ECSmanager()
    {
        _gameWorld = new EcsWorld();

        _entitiesGeneralManager = new EntitiesGeneralManager(_gameWorld);
        _systemsGeneralManager = new SystemsGeneralManager(_gameWorld);
        _systemsGeneralManager.CreateSystems();

        _entitiesMasterManager = new EntitiesMasterManager(_gameWorld, this);
        _systemsMasterManager = new SystemsMasterManager(_gameWorld);
        _systemsMasterManager.CreateSystems(this);

        _entitiesOtherManager = new EntitiesOtherManager(_gameWorld);
        _systemsOtherManager = new SystemsOtherManager(_gameWorld);
        _systemsOtherManager.CreateSystems(this);

        _cellManager = new CellManager();
        _economyManager = new EconomyManager();

        _cellManager.InitAfterECS(this);
        _economyManager.InitAfterECS(this);
    }

    internal void InitAndProcessInjectsSystems()
    {
        _systemsGeneralManager.InitAndProcessInjectsSystems();
        _systemsMasterManager.InitAndProcessInjectsSystems();
        _systemsOtherManager.InitAndProcessInjectsSystems();
    }

    internal void SpawnAndFillEntities()
    {
        _entitiesGeneralManager.SpawnAndFillEntities();
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
