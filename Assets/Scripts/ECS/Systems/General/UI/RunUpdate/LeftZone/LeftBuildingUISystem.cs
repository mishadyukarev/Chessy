using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using static Assets.Scripts.Main;
using static Assets.Scripts.PhotonPunRPC;

internal sealed class LeftBuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    public override void Init()
    {
        base.Init();

        _eGGUIM.MeltOreEnt_ButtonCom.Button.onClick.AddListener(delegate { MeltOre(); });

        _eGGUIM.BuyPawnUIEnt_ButtonCom.Button.onClick.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
        _eGGUIM.BuyRookUIEnt_ButtonCom.Button.onClick.AddListener(delegate { BuyUnit(UnitTypes.Rook); });
        _eGGUIM.BuyBishopUIEnt_ButtonCom.Button.onClick.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        _eGGUIM.UpgradeUnitUIEnt_ButtonCom.Button.onClick.AddListener(delegate { ToggleUpgradeMod(UpgradeModTypes.Unit); });


        _eGGUIM.UpgradeFarmUIEnt_ButtonCom.Button.onClick.AddListener(delegate { UpgradeBuilding(BuildingTypes.Farm); });
        _eGGUIM.UpgradeWoodcutterUIEnt_ButtonCom.Button.onClick.AddListener(delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        _eGGUIM.UpgradeMineUIEnt_ButtonCom.Button.onClick.AddListener(delegate { UpgradeBuilding(BuildingTypes.Mine); });
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
                    _eGGUIM.BuildingZoneEnt_ParentCom.ParentGO.SetActive(true);
                }
                else _eGGUIM.BuildingZoneEnt_ParentCom.ParentGO.SetActive(false);
            }
        }
        else
        {
            _eGGUIM.BuildingZoneEnt_ParentCom.ParentGO.SetActive(false);
        }
    }


    private void BuyUnit(UnitTypes unitType)
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) CreateUnitToMaster(unitType);
    }
    private void ToggleUpgradeMod(UpgradeModTypes upgradeModType)
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient))
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
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) UpgradeBuildingToMaster(buildingType);
    }

    private void MeltOre()
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) MeltOreToMaster();
    }
}
