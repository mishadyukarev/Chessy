using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using static Assets.Scripts.Main;
using static Assets.Scripts.PhotonPunRPC;

internal sealed class LeftBuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    private bool IsActivatedDoner => _eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient);

    public override void Init()
    {
        base.Init();

        _eGGUIM.MeltOreEnt_ButtonCom.AddListener(delegate { MeltOre(); });

        _eGGUIM.BuyPawnUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
        _eGGUIM.BuyRookUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Rook); });
        _eGGUIM.BuyBishopUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        _eGGUIM.UpgradeUnitUIEnt_ButtonCom.AddListener(delegate { ToggleUpgradeMod(UpgradeModTypes.Unit); });


        _eGGUIM.UpgradeFarmUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Farm); });
        _eGGUIM.UpgradeWoodcutterUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        _eGGUIM.UpgradeMineUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && CellBuildingsDataWorker.GetBuildingType(XySelectedCell) == BuildingTypes.City)
        {
            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                {
                    _eGGUIM.BuildingZoneEnt_ParentCom.SetActive(true);
                }
                else _eGGUIM.BuildingZoneEnt_ParentCom.SetActive(false);
            }
        }
        else
        {
            _eGGUIM.BuildingZoneEnt_ParentCom.SetActive(false);
        }
    }


    private void BuyUnit(UnitTypes unitType)
    {
        if (!IsActivatedDoner) CreateUnitToMaster(unitType);
    }
    private void ToggleUpgradeMod(UpgradeModTypes upgradeModType)
    {
        if (!IsActivatedDoner)
        {
            if (SelectorWorker.IsUpgradeModType(UpgradeModTypes.None))
            {
                _eGM.SelectorEnt_UpgradeModTypeCom.UpgradeModType = upgradeModType;
            }
            else
            {
                SelectorWorker.ResetUpgradeModType();
            }
        }
    }

    private void UpgradeBuilding(BuildingTypes buildingType)
    {
        if (!IsActivatedDoner) UpgradeBuildingToMaster(buildingType);
    }

    private void MeltOre()
    {
        if (!IsActivatedDoner) MeltOreToMaster();
    }
}
