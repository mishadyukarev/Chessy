using Chessy.Game.Extensions;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class SyncBuildingVS
    {
        readonly byte _curCell;

        readonly bool[] _needActive = new bool[(byte)BuildingTypes.End];

        internal SyncBuildingVS(in byte curCell)
        {
            _curCell = curCell;
        }

        public void Sync(in EntitiesViewGame vEs, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            var curPlayerI = e.CurPlayerIT;

            var isVisForMe = e.BuildingVisibleC(_curCell).IsVisible(curPlayerI);
            var isVisForNext = e.BuildingVisibleC(_curCell).IsVisible(curPlayerI.NextPlayer());

            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                _needActive[(byte)build] = false;
            }

            if (e.BuildingTC(_curCell).HaveBuilding)
            {
                if (isVisForMe)
                {
                    _needActive[(byte)e.BuildingT(_curCell)] = true;
                }
            }


            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                vEs.BuildingE(_curCell, buildT).GO.SetActive(_needActive[(byte)buildT]);
            }
                
        }
    }

}
