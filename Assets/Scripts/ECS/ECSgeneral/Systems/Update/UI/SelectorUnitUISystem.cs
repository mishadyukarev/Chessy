using Leopotam.Ecs;
using static MainGame;

internal class SelectorUnitUISystem : IEcsRunSystem
{
    private EcsComponentRef<SelectorUnitComponent> _selectorUnitComponentRef = default;
    private EcsComponentRef<EconomyComponent.UnitComponent> _economyUnitsComponentRef = default;
    private EcsComponentRef<DonerComponent> _doneComponentRef = default;
    private PhotonPunRPC _photonPunRPC = default;

    internal SelectorUnitUISystem(ECSmanager eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;

        _selectorUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;
        _economyUnitsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUnitsComponentRef;
        _doneComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;


        _selectorUnitComponentRef.Unref().Button0 = InstanceGame.StartSpawnGameManager.Button0;
        _selectorUnitComponentRef.Unref().Button0.onClick.AddListener(delegate { GetUnit(UnitTypes.King); });

        _selectorUnitComponentRef.Unref().Button1 = MainGame.InstanceGame.StartSpawnGameManager.Button1;
        _selectorUnitComponentRef.Unref().Button1.onClick.AddListener(delegate { GetUnit(UnitTypes.Pawn); });
    }

    public void Run()
    {
        if (_economyUnitsComponentRef.Unref().IsSettedKing) _selectorUnitComponentRef.Unref().Button0.gameObject.SetActive(false);
    }

    private void GetUnit(UnitTypes unitType)
    {
        if (!_doneComponentRef.Unref().IsDone) _photonPunRPC.GetUnit(unitType);
    }
}
