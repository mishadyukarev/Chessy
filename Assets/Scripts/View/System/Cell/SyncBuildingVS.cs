using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using System.Collections.Generic;

namespace Chessy.View.System
{
    sealed class SyncBuildingVS : SystemViewAbstract
    {
        readonly Dictionary<BuildingTypes, SpriteRendererVC[]> _buildingSRCs = new Dictionary<BuildingTypes, SpriteRendererVC[]>();
        readonly Dictionary<BuildingTypes, bool[]> _needActive = new Dictionary<BuildingTypes, bool[]>();

        internal SyncBuildingVS(in Dictionary<BuildingTypes, SpriteRendererVC[]> buildingSRCs, in EntitiesModel eM) : base(eM)
        {
            _buildingSRCs = buildingSRCs;

            for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
            {
                _needActive.Add(buildingT, new bool[IndexCellsValues.CELLS]);
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                {
                    _needActive[buildingT][cellIdxCurrent] = false;
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var isVisForMe = _e.BuildingVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT);
                var isVisForNext = _e.BuildingVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT.NextPlayer());

                if (_e.BuildingOnCellT(cellIdxCurrent).HaveBuilding())
                {
                    if (isVisForMe)
                    {
                        _needActive[_e.BuildingOnCellT(cellIdxCurrent)][cellIdxCurrent] = true;
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                {
                    _buildingSRCs[buildingT][cellIdxCurrent].TrySetActiveGO(_needActive[buildingT][cellIdxCurrent]);
                }
            }



        }
    }

}
