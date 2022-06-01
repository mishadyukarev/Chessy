using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class SyncBuildingVS : SystemViewCellGameAbs
    {
        readonly bool[] _needActive = new bool[(byte)BuildingTypes.End];
        readonly CellBuildingVEs _buildingVEs;


        internal SyncBuildingVS(in CellBuildingVEs buildingVEs, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _buildingVEs = buildingVEs;
        }

        internal override void Sync()
        {
            var curPlayerI = _e.CurPlayerIT;

            var isVisForMe = _e.BuildingVisibleC(_currentCell).IsVisible(curPlayerI);
            var isVisForNext = _e.BuildingVisibleC(_currentCell).IsVisible(curPlayerI.NextPlayer());

            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                _needActive[(byte)build] = false;
            }

            if (_e.BuildingTC(_currentCell).HaveBuilding)
            {
                if (isVisForMe)
                {
                    _needActive[(byte)_e.BuildingT(_currentCell)] = true;
                }
            }


            for (var buildT = (BuildingTypes)1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingVEs.Main(buildT).GO.SetActive(_needActive[(byte)buildT]);
            }
                
        }
    }

}
