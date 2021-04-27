using Leopotam.Ecs;
using Scripts.ECS.Entities;
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


    public EcsWorld EcsWorld => _ecsWorld;

    public EntitiesGeneralManager EntitiesGeneralManager => _entitiesGeneralManager;
    public SystemsGeneralManager SystemsGeneralManager => _systemsGeneralManager;

    public EntitiesMasterManager EntitiesMasterManager => _entitiesMasterManager;
    public SystemsMasterManager SystemsMasterManager => _systemsMasterManager;

    public EntitiesOtherManager EntitiesOtherManager => _entitiesOtherManager;
    public SystemsOtherManager SystemsOtherManager => _systemsOtherManager;



    internal ECSmanager(PhotonGameManager photonManager)
    {
        _ecsWorld = new EcsWorld();

        _entitiesGeneralManager = new EntitiesGeneralManager(_ecsWorld);
        _systemsGeneralManager = new SystemsGeneralManager(_ecsWorld);
        _entitiesGeneralManager.CreateEntities(this);
        _systemsGeneralManager.CreateInitSystems(this, photonManager);


        if (InstanceGame.IsMasterClient)
        {
            _entitiesMasterManager = new EntitiesMasterManager(_ecsWorld);
            _systemsMasterManager = new SystemsMasterManager(_ecsWorld);

            _entitiesMasterManager.CreateEntities(this);
            _systemsMasterManager.CreateInitSystems(this, photonManager);
        }
        else
        {
            _entitiesOtherManager = new EntitiesOtherManager(_ecsWorld);
            _systemsOtherManager = new SystemsOtherManager(_ecsWorld);

            _entitiesOtherManager.CreateEntities();
            _systemsOtherManager.CreateInitSystems(this, photonManager);
        }     
    }

    public void Run()
    {
        _systemsGeneralManager.RunUpdate();

        if (InstanceGame.IsMasterClient) _systemsMasterManager.RunUpdate();
        else _systemsOtherManager.RunUpdate();
    }

    public void OnDestroy()
    {
        _systemsGeneralManager.Destroy();

        if (InstanceGame.IsMasterClient) _systemsMasterManager.Destroy();
        else _systemsOtherManager.Destroy();

        _ecsWorld.Destroy();
    }
}
