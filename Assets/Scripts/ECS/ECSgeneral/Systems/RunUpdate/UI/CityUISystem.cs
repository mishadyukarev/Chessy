using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class CityUISystem : CellReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;

    private Image _leftImage;

    private Button _meltOreButton;

    private Button _inGameLeftUpgradePawnButton;
    private Button _inGameLeftUpgradeRookButton;
    private Button _inGameLeftUpgradeBishopButton;

    private Button _buyPawnButton;
    private Button _gameLeftBuyRookButton;
    private Button _gameLeftBuyBishopButton;

    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;


    internal CityUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        _leftImage = InstanceGame.GameObjectPool.LeftImage;

        _inGameLeftUpgradePawnButton = InstanceGame.GameObjectPool.InGameLeftUpgadePawnButton;
        _inGameLeftUpgradePawnButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });

        _inGameLeftUpgradeRookButton = InstanceGame.GameObjectPool.InGameLeftUpgadeRookButton;
        _inGameLeftUpgradeRookButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Rook); });

        _inGameLeftUpgradeBishopButton = InstanceGame.GameObjectPool.InGameLeftUpgadeBishopButton;
        _inGameLeftUpgradeBishopButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Bishop); });


        _buyPawnButton = InstanceGame.GameObjectPool.InGameLeftBuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        _gameLeftBuyRookButton = InstanceGame.GameObjectPool.InGameLeftBuyRookButton;
        _gameLeftBuyRookButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Rook); });

        _gameLeftBuyBishopButton = InstanceGame.GameObjectPool.InGameLeftBuyBishopButton;
        _gameLeftBuyBishopButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        _meltOreButton = InstanceGame.GameObjectPool.LeftMeltButton;
        _meltOreButton.onClick.AddListener(delegate { MeltOre(); });


    }

    public void Run()
    {
        
        if (CellBuildingComponent(_xySelectedCell).BuildingType == BuildingTypes.City)
        {
            if (CellBuildingComponent(_xySelectedCell).IsMine)
            {
                _leftImage.gameObject.SetActive(true);
            }

            else _leftImage.gameObject.SetActive(false);
        }
        else
        {
            _leftImage.gameObject.SetActive(false);
        }
    }


    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.CreateUnit(unitType);

    private void UpgradeUnit(UnitTypes unitType) => _photonPunRPC.UpgradeUnitToMaster(unitType);

    private void MeltOre() => _photonPunRPC.MeltOre();
}
