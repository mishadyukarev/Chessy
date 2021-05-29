using Photon.Realtime;
using UnityEngine;

internal class CellWorker
{
    private EntitiesGeneralManager _eGM;

    internal void InitAfterECS(EntitiesGeneralManager eGM)
    {
        _eGM = eGM;
    }

    internal void SetBuilding(BuildingTypes buildingType, Player owner, params int[] xy)
    {
        _eGM.CellBuildingEnt_BuildingTypeCom(xy).BuildingType = buildingType;
        _eGM.CellBuildingEnt_OwnerCom(xy).SetOwner(owner);

        switch (_eGM.CellBuildingEnt_BuildingTypeCom(xy).BuildingType)
        {
            case BuildingTypes.City:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(true, BuildingTypes.City);
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetColorBuilding(BuildingTypes.City, owner);
                break;

            case BuildingTypes.Farm:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(true, BuildingTypes.Farm);
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetColorBuilding(BuildingTypes.Farm, owner);
                _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] += 1;
                break;

            case BuildingTypes.Woodcutter:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(true, BuildingTypes.Woodcutter);
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetColorBuilding(BuildingTypes.Woodcutter, owner);
                _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] += 1;
                break;

            case BuildingTypes.Mine:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(true, BuildingTypes.Mine);
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetColorBuilding(BuildingTypes.Mine, owner);
                _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] += 1;
                break;
        }
    }

    internal void ResetBuilding(params int[] xy)
    {
        switch (_eGM.CellBuildingEnt_BuildingTypeCom(xy).BuildingType)
        {
            case BuildingTypes.None:
                break;

            case BuildingTypes.City:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(false, BuildingTypes.City);
                _eGM.InfoEnt_BuildingsInfoCom.IsSettedCityDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] = false;
                _eGM.InfoEnt_BuildingsInfoCom.XySettedCityDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] = default;
                break;

            case BuildingTypes.Farm:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(false, BuildingTypes.Farm);
                _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] -= 1;
                break;

            case BuildingTypes.Woodcutter:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(false, BuildingTypes.Woodcutter);
                _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] -= 1;
                break;

            case BuildingTypes.Mine:
                _eGM.CellBuildingEnt_CellBuildingCom(xy).SetEnabledSR(false, BuildingTypes.Mine);
                _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[_eGM.CellBuildingEnt_OwnerCom(xy).IsMasterClient] -= 1;
                break;

            default:
                break;
        }

        _eGM.CellBuildingEnt_BuildingTypeCom(xy).BuildingType = BuildingTypes.None;
        _eGM.CellBuildingEnt_OwnerCom(xy).SetOwner(default);
    }
}
