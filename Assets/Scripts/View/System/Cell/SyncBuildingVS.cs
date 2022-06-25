using Chessy.Model.Extensions;
using Chessy.Model;

namespace Chessy.Model
{
    sealed class SyncBuildingVS : SystemViewCellGameAbs
    {
        readonly bool[] _needActive = new bool[(byte)BuildingTypes.End];
        readonly CellBuildingVE _buildingVEs;


        internal SyncBuildingVS(in CellBuildingVE buildingVEs, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _buildingVEs = buildingVEs;
        }

        internal override void Sync()
        {
            var isVisForMe = _e.BuildingVisibleC(_currentCell).IsVisible(_e.CurPlayerIT);
            var isVisForNext = _e.BuildingVisibleC(_currentCell).IsVisible(_e.CurPlayerIT.NextPlayer());

            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                _needActive[(byte)build] = false;
            }

            if (_e.BuildingOnCellT(_currentCell).HaveBuilding())
            {
                if (isVisForMe)
                {
                    _needActive[(byte)_e.BuildingOnCellT(_currentCell)] = true;
                }
            }


            for (var buildT = (BuildingTypes)1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingVEs.Main(buildT).GO.SetActive(_needActive[(byte)buildT]);
            }

        }
    }

}
