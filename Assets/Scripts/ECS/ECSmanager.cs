using Leopotam.Ecs;
using System;
using static Main;

internal sealed class ECSManager
{
    private EcsWorld _commonWorld;
    private EcsWorld _gameWorld;

    private EntitiesCommonManager _entitiesCommonManager;

    private EntitiesGeneralManager _entitiesGeneralManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private SystemsMasterManager _systemsMasterManager;
    private EntitiesMasterManager _entitiesMasterManager;

    private SystemsOtherManager _systemsOtherManager;
    private EntitiesOtherManager _entitiesOtherManager;

    private CellManager _cellManager;
    private EconomyManager _economyManager;


    internal EntitiesCommonManager EntitiesCommonManager => _entitiesCommonManager;

    internal EntitiesGeneralManager EntitiesGeneralManager => _entitiesGeneralManager;
    internal SystemsGeneralManager SystemsGeneralManager => _systemsGeneralManager;

    internal EntitiesMasterManager EntitiesMasterManager => _entitiesMasterManager;
    internal SystemsMasterManager SystemsMasterManager => _systemsMasterManager;

    internal EntitiesOtherManager EntitiesOtherManager => _entitiesOtherManager;
    internal SystemsOtherManager SystemsOtherManager => _systemsOtherManager;

    internal CellManager CellManager => _cellManager;
    internal EconomyManager EconomyManager => _economyManager;



    internal ECSManager()
    {
        _commonWorld = new EcsWorld();
        _gameWorld = new EcsWorld();


        _entitiesCommonManager = new EntitiesCommonManager(_commonWorld);


        _entitiesGeneralManager = new EntitiesGeneralManager(_gameWorld, _entitiesCommonManager.ResourcesEnt_ResourcesCommonCom);
        _systemsGeneralManager = new SystemsGeneralManager();

        _entitiesMasterManager = new EntitiesMasterManager(_gameWorld);
        _systemsMasterManager = new SystemsMasterManager();

        _entitiesOtherManager = new EntitiesOtherManager(_gameWorld);
        _systemsOtherManager = new SystemsOtherManager();


        _cellManager = new CellManager(this, _entitiesCommonManager.ResourcesEnt_ResourcesCommonCom);
        _economyManager = new EconomyManager(this);
    }

    internal void OwnUpdate()
    {
        _systemsGeneralManager.Update();

        if (Instance.IsMasterClient) _systemsMasterManager.Update();
        else _systemsOtherManager.Update();
    }
    internal void OwnFixedUpdate()
    {
        //_systemsGeneralManager.FixedUpdate();

        //if (Instance.IsMasterClient) _systemsMasterManager.FixedUpdate();
        //else _systemsOtherManager.FixedUpdate();
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        _entitiesCommonManager.ToggleScene(sceneType);

        switch (sceneType)
        {
            case SceneTypes.Menu:
                _entitiesGeneralManager.Dispose();
                break;

            case SceneTypes.Game:
                _entitiesGeneralManager.FillEntities(_entitiesCommonManager.ResourcesEnt_ResourcesCommonCom);

                _systemsGeneralManager.CreateSystems(_gameWorld);
                _systemsMasterManager.CreateSystems(_gameWorld);
                _systemsOtherManager.CreateSystems(_gameWorld);

                _systemsGeneralManager.ProcessInjects();
                _systemsMasterManager.ProcessInjects();
                _systemsOtherManager.ProcessInjects();

                _systemsGeneralManager.Init();
                _systemsMasterManager.Init();
                _systemsOtherManager.Init();
                break;

            default:
                break;
        }
    }
}
