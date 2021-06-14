using Photon.Realtime;

internal sealed class CellBuildingWorker : CellWorker
{
    internal CellBuildingWorker(ECSManager eCSmanager) : base(eCSmanager) { }

    internal void SetBuilding(bool withEconomy, BuildingTypes buildingType, Player owner, params int[] xy)
    {
        _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
        _eGM.CellBuildEnt_OwnerCom(xy).SetOwner(owner);
        _eGM.CellBuildEnt_CellBuilCom(xy).SetEnabledSR(true, _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, owner);

        if (withEconomy)
        {
            _eGM.BuildingsEnt_BuildingsCom.AddAmountBuildings
                (_eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, _eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
        }
    }
    internal void ResetBuilding(bool withEconomy, params int[] xy)
    {
        if (withEconomy)
        {
            if (_eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType == BuildingTypes.City)
            {
                _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[_eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = false;
                _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[_eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = default;
            }

            _eGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings
                (_eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, _eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
        }


        _eGM.CellBuildEnt_CellBuilCom(xy).SetEnabledSR(false, _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType);
        _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = BuildingTypes.None;
        _eGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
    }
}