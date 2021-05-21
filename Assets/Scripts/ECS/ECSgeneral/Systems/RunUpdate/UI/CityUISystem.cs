using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class CityUISystem : SystemGeneralReduction, IEcsRunSystem
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

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;


    internal CityUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;

        _leftImage = Instance.GameObjectPool.LeftImage;

        _inGameLeftUpgradePawnButton = Instance.GameObjectPool.InGameLeftUpgadePawnButton;
        _inGameLeftUpgradePawnButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });

        _inGameLeftUpgradeRookButton = Instance.GameObjectPool.InGameLeftUpgadeRookButton;
        _inGameLeftUpgradeRookButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Rook); });

        _inGameLeftUpgradeBishopButton = Instance.GameObjectPool.InGameLeftUpgadeBishopButton;
        _inGameLeftUpgradeBishopButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Bishop); });


        _buyPawnButton = Instance.GameObjectPool.InGameLeftBuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        _gameLeftBuyRookButton = Instance.GameObjectPool.InGameLeftBuyRookButton;
        _gameLeftBuyRookButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Rook); });

        _gameLeftBuyBishopButton = Instance.GameObjectPool.InGameLeftBuyBishopButton;
        _gameLeftBuyBishopButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        _meltOreButton = Instance.GameObjectPool.LeftMeltButton;
        _meltOreButton.onClick.AddListener(delegate { MeltOre(); });


    }

    public void Run()
    {

        if (_eGM.CellBuildingEnt_BuildingTypeCom(_xySelectedCell).BuildingType == BuildingTypes.City)
        {
            if (_eGM.CellBuildingEnt_OwnerCom(_xySelectedCell).Owner.IsLocal)
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


    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.CreateUnitToMaster(unitType);

    private void UpgradeUnit(UnitTypes unitType) => _photonPunRPC.UpgradeUnitToMaster(unitType);

    private void MeltOre() => _photonPunRPC.MeltOreToMaster();
}
