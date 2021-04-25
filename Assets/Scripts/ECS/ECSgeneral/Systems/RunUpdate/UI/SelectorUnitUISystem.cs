using Leopotam.Ecs;

internal class SelectorUnitUISystem : IEcsRunSystem
{
    private EcsComponentRef<SelectorUnitComponent> _selectorUnitComponentRef = default;
    private EcsComponentRef<EconomyComponent.UnitsComponent> _economyUnitsComponentRef = default;
    private PhotonPunRPC _photonPunRPC = default;

    internal SelectorUnitUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager)
    {
        _photonPunRPC = photonManager.PhotonPunRPC;

        _selectorUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;
        _economyUnitsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUnitsComponentRef;


        _selectorUnitComponentRef.Unref().Button0 = startSpawnGameManager.Button0;
        _selectorUnitComponentRef.Unref().Button0.onClick.AddListener(delegate { GetUnit(UnitTypes.King); });

        _selectorUnitComponentRef.Unref().Button1 = startSpawnGameManager.Button1;
        _selectorUnitComponentRef.Unref().Button1.onClick.AddListener(delegate { GetUnit(UnitTypes.Pawn); });
    }

    public void Run()
    {
        if (_economyUnitsComponentRef.Unref().IsSettedKing) _selectorUnitComponentRef.Unref().Button0.gameObject.SetActive(false);
    }

    private void GetUnit(UnitTypes unitType) => _photonPunRPC.GetUnit(unitType);
}
