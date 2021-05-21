using Leopotam.Ecs;
using static MainGame;


public sealed class ECSmanager
{
    private EcsWorld _ecsWorld;

    private EntitiesGeneralManager _entitiesGeneralManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private SystemsMasterManager _systemsMasterManager;
    private EntitiesMasterManager _entitiesMasterManager;

    private SystemsOtherManager _systemsOtherManager;
    private EntitiesOtherManager _entitiesOtherManager;


    internal EntitiesGeneralManager EntitiesGeneralManager => _entitiesGeneralManager;
    public SystemsGeneralManager SystemsGeneralManager => _systemsGeneralManager;

    internal EntitiesMasterManager EntitiesMasterManager => _entitiesMasterManager;
    internal SystemsMasterManager SystemsMasterManager => _systemsMasterManager;

    internal EntitiesOtherManager EntitiesOtherManager => _entitiesOtherManager;
    public SystemsOtherManager SystemsOtherManager => _systemsOtherManager;



    internal ECSmanager()
    {
        _ecsWorld = new EcsWorld();

        _entitiesGeneralManager = new EntitiesGeneralManager(_ecsWorld);
        _systemsGeneralManager = new SystemsGeneralManager(_ecsWorld);

        _entitiesGeneralManager.CreateEntities();
        _systemsGeneralManager.CreateInitSystems(this);


        if (Instance.IsMasterClient)
        {
            _entitiesMasterManager = new EntitiesMasterManager(_ecsWorld);
            _systemsMasterManager = new SystemsMasterManager(_ecsWorld);

            _entitiesMasterManager.CreateEntities(this);
            _systemsMasterManager.CreateInitSystems(this);
        }
        else
        {
            _entitiesOtherManager = new EntitiesOtherManager(_ecsWorld);
            _systemsOtherManager = new SystemsOtherManager(_ecsWorld);

            _entitiesOtherManager.CreateEntities();
            _systemsOtherManager.CreateInitSystems(this);
        }
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

    public void OnDestroy()
    {
        _systemsGeneralManager.Destroy();

        if (Instance.IsMasterClient) _systemsMasterManager.Destroy();
        else _systemsOtherManager.Destroy();

        _ecsWorld.Destroy();
    }
}
