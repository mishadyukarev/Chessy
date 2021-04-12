using Leopotam.Ecs;
using Photon.Pun;
using Scripts.ECS.Entities;
using static Main;


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



    public ECSmanager(SupportManager supportManager, PhotonManager photonManager)
    {
        _ecsWorld = new EcsWorld();

        _entitiesGeneralManager = new EntitiesGeneralManager(_ecsWorld);
        _systemsGeneralManager = new SystemsGeneralManager(_ecsWorld);

        _entitiesGeneralManager.CreateEntities(this, supportManager, photonManager);
        _systemsGeneralManager.CreateInitProccessInjectsSystems(this, supportManager, photonManager);

        if (Instance.IsMasterClient)
        {
            _entitiesMasterManager = new EntitiesMasterManager(_ecsWorld);
            _systemsMasterManager = new SystemsMasterManager(_ecsWorld);

            _entitiesMasterManager.CreateEntities(this, supportManager);
            _systemsMasterManager.CreateInitProccessInjectsSystems(this, supportManager, photonManager);
        }
        else
        {
            _entitiesOtherManager = new EntitiesOtherManager(_ecsWorld);
            _systemsOtherManager = new SystemsOtherManager(_ecsWorld);

            _entitiesOtherManager.CreateEntities();
            _systemsOtherManager.CreateInitProccessInjectsSystems(this, supportManager, photonManager);
        }
    }

    public void Run()
    {
        _systemsGeneralManager.RunUpdate();

        if (Instance.IsMasterClient) _systemsMasterManager.RunUpdate();
        else _systemsOtherManager.RunUpdate();
    }

    public void OnDestroy()
    {
        SystemsGeneralManager.Destroy();
        EcsWorld.Destroy();
    }
}
