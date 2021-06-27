using Assets.Scripts;

internal sealed class LeftBuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Init()
    {
        base.Init();

        _eGM.MeltOreEnt_ButtonCom.AddListener(delegate { MeltOre(); });

        _eGM.BuyPawnUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
        _eGM.BuyRookUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Rook); });
        _eGM.BuyBishopUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        _eGM.UpgradePawnUIEnt_ButtonCom.AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
        _eGM.UpgradeRookUIEnt_ButtonCom.AddListener(delegate { UpgradeUnit(UnitTypes.Rook); });
        _eGM.UpgradeBishopUIEnt_ButtonCom.AddListener(delegate { UpgradeUnit(UnitTypes.Bishop); });


        _eGM.UpgradeFarmUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Farm); });
        _eGM.UpgradeWoodcutterUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        _eGM.UpgradeMineUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
        {
            if (_eGM.CellBuildEnt_OwnerCom(XySelectedCell).IsMine)
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
