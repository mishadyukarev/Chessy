﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;

internal sealed class LeftBuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected);
    private bool IsActivatedDoner => _eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient);

    public override void Init()
    {
        base.Init();

        _eGM.MeltOreEnt_ButtonCom.AddListener(delegate { MeltOre(); });

        _eGM.BuyPawnUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
        _eGM.BuyRookUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Rook); });
        _eGM.BuyBishopUIEnt_ButtonCom.AddListener(delegate { BuyUnit(UnitTypes.Bishop); });

        _eGM.UpgradeUnitUIEnt_ButtonCom.AddListener(delegate { ToggleUpgradeMod(UpgradeModTypes.Unit); });


        _eGM.UpgradeFarmUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Farm); });
        _eGM.UpgradeWoodcutterUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        _eGM.UpgradeMineUIEnt_ButtonCom.AddListener(delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
        {
            if (_eGM.CellBuildEnt_OwnerCom(XySelectedCell).HaveOwner)
            {
                if (_eGM.CellBuildEnt_OwnerCom(XySelectedCell).IsMine)
                {
                    _eGM.BuildingZoneEnt_ParentCom.SetActive(true);
                }
                else _eGM.BuildingZoneEnt_ParentCom.SetActive(false);
            }
        }
        else
        {
            _eGM.BuildingZoneEnt_ParentCom.SetActive(false);
        }
    }


    private void BuyUnit(UnitTypes unitType)
    {
        if (!IsActivatedDoner) _photonPunRPC.CreateUnitToMaster(unitType);
    }
    private void ToggleUpgradeMod(UpgradeModTypes upgradeModType)
    {
        if (!IsActivatedDoner)
        {
            if (_eGM.SelectorEnt_UpgradeModTypeCom.IsUpgradeModType(UpgradeModTypes.None))
            {
                _eGM.SelectorEnt_UpgradeModTypeCom.SetUpgradeModType(upgradeModType);
            }
            else
            {
                _eGM.SelectorEnt_UpgradeModTypeCom.ResetUpgradeModType();
            }
        }
    }

    private void UpgradeBuilding(BuildingTypes buildingType)
    {
        if (!IsActivatedDoner) _photonPunRPC.UpgradeBuildingToMaster(buildingType);
    }

    private void MeltOre()
    {
        if (!IsActivatedDoner) _photonPunRPC.MeltOreToMaster();
    }
}