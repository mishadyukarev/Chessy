using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class CityUISystem : CellReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;

    private Image _leftImage;

    private Button _improveCityButton;
    private Button _meltOreButton;

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


        _buyPawnButton = InstanceGame.GameObjectPool.GameLeftBuyPawnButton;
        _buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        _gameLeftBuyRookButton = InstanceGame.GameObjectPool.GameLeftBuyRookButton;
        _gameLeftBuyRookButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Rook); });

        _gameLeftBuyBishopButton = InstanceGame.GameObjectPool.GameLeftBuyBishopButton;
        _gameLeftBuyBishopButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });


        _improveCityButton = InstanceGame.GameObjectPool.LeftImproveCityButton;
        _improveCityButton.onClick.AddListener(delegate { ImproveCity(); });

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


    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.BuyUnit(unitType);

    private void ImproveCity() { }

    private void MeltOre() => _photonPunRPC.MeltOre();
}
