using Photon.Realtime;

namespace Assets.Scripts
{
    public sealed class CellBuildingWorker : CellWorker
    {
        public CellBuildingWorker(ECSManager eCSmanager) : base(eCSmanager) { }

        internal void SetPlayerBuilding(bool withEconomy, BuildingTypes buildingType, Player owner, params int[] xy)
        {
            _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
            _eGM.CellBuildEnt_OwnerCom(xy).SetOwner(owner);
            _eGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(true, _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, owner);

            if (withEconomy)
            {
                _eGM.BuildingsEnt_BuildingsCom.AddAmountBuildings
                    (_eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, _eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
            }
        }

        internal void SetBotBuilding(BuildingTypes buildingType, params int[] xy)
        {
            _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
            _eGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot = true;
            _eGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(true, _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType);
        }

        internal void ResetBuilding(bool withEconomy, params int[] xy)
        {
            var buildType = _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType;

            if (withEconomy)
            {
                if (_eGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
                {
                    if (buildType == BuildingTypes.City)
                    {
                        _eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[_eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = false;
                        _eGM.BuildingsEnt_BuildingsCom.XySettedCityDict[_eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = default;
                    }

                    _eGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings(buildType, _eGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
                }
            }

            if (_eGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
            {
                _eGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(false, buildType);
                _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = BuildingTypes.None;
                _eGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            else if (_eGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot)
            {
                _eGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(false, buildType);
                _eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = BuildingTypes.None;
                _eGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }
        }
    }
}