using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed class CityUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    internal CityUISystem()
    {
        //_leftZoneCityZoneGO = Instance.CanvasGameManager.LeftZoneBuildingZoneGO;

        //_inGameLeftUpgradePawnButton = Instance.CanvasGameManager.LeftZoneCityZoneUpgadePawnButton;
        //_inGameLeftUpgradePawnButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });

        //_inGameLeftUpgradeRookButton = Instance.CanvasGameManager.LeftZoneCityZoneUpgadeRookButton;
        //_inGameLeftUpgradeRookButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Rook); });

        //_inGameLeftUpgradeBishopButton = Instance.CanvasGameManager.LeftZoneCityZoneUpgadeBishopButton;
        //_inGameLeftUpgradeBishopButton.onClick.AddListener(delegate { UpgradeUnit(UnitTypes.Bishop); });


        //_buyPawnButton = Instance.CanvasGameManager.LeftZoneCityZoneBuyPawnButton;
        //_buyPawnButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });

        //_gameLeftBuyRookButton = Instance.CanvasGameManager.LeftZoneCityZoneBuyRookButton;
        //_gameLeftBuyRookButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Rook); });

        //_gameLeftBuyBishopButton = Instance.CanvasGameManager.LeftZoneCityZoneBuyBishopButton;
        //_gameLeftBuyBishopButton.onClick.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        //_meltOreButton = Instance.CanvasGameManager.LeftZoneCityZoneMeltButton;
        //_meltOreButton.onClick.AddListener(delegate { MeltOre(); });


        //_inGameLeftUpgradeFarmButton = Instance.CanvasGameManager.LeftZoneCityZoneUpgradeFarmButton;
        //_inGameLeftUpgradeFarmButton.onClick.AddListener(delegate { UpgradeBuilding(BuildingTypes.Farm); });
        //_inGameLeftUpgradeWoodcutter = Instance.CanvasGameManager.LeftZoneCityZoneUpgradeWoodcutterButton;
        //_inGameLeftUpgradeWoodcutter.onClick.AddListener(delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        //_inGameLeftUpgradeMine = Instance.CanvasGameManager.LeftZoneCityZoneUpgradeMineButton;
        //_inGameLeftUpgradeMine.onClick.AddListener(delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellBuilEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
        {
            if (_eGM.CellBuilEnt_OwnerCom(XySelectedCell).IsMine)
            {
                _eGM.BuildingZoneEnt_ParentCom.SetActive(true);
            }

            else _eGM.BuildingZoneEnt_ParentCom.SetActive(false);
        }
        else
        {
            _eGM.BuildingZoneEnt_ParentCom.SetActive(false);
        }
    }


    private void BuyUnit(UnitTypes unitType) => _photonPunRPC.CreateUnitToMaster(unitType);

    private void UpgradeUnit(UnitTypes unitType) => _photonPunRPC.UpgradeUnitToMaster(unitType);
    private void UpgradeBuilding(BuildingTypes buildingType) => _photonPunRPC.UpgradeBuildingToMaster(buildingType);

    private void MeltOre() => _photonPunRPC.MeltOreToMaster();
}
