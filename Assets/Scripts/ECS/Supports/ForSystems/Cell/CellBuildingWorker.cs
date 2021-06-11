using Photon.Realtime;

internal sealed class CellBuildingWorker : CellWorker
{
    internal CellBuildingWorker(ECSManager eCSmanager) : base(eCSmanager) { }

    internal void SetBuilding(BuildingTypes buildingType, Player owner, params int[] xy)
    {
        _eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType = buildingType;
        _eGM.CellBuilEnt_OwnerCom(xy).SetOwner(owner);


        _eGM.CellBuildEnt_CellBuilCom(xy).SetEnabledSR(true, _eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType, owner);
        _eGM.BuildingsEnt_BuildingsCom.AddAmountBuildings
            (_eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType, _eGM.CellBuilEnt_OwnerCom(xy).IsMasterClient);
    }
    internal void ResetBuilding(params int[] xy)
    {
        if (_eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType == BuildingTypes.City)
        {
            _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[_eGM.CellBuilEnt_OwnerCom(xy).IsMasterClient] = false;
            _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[_eGM.CellBuilEnt_OwnerCom(xy).IsMasterClient] = default;
        }

        _eGM.CellBuildEnt_CellBuilCom(xy).SetEnabledSR(false, _eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType);
        _eGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings
            (_eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType, _eGM.CellBuilEnt_OwnerCom(xy).IsMasterClient);

        _eGM.CellBuilEnt_BuilTypeCom(xy).BuildingType = BuildingTypes.None;
        _eGM.CellBuilEnt_OwnerCom(xy).SetOwner(default);
    }
}