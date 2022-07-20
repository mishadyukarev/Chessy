using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncBuildingVS : SystemViewAbstract
    {
        readonly SpriteRenderer[][] _buildingSRs = new SpriteRenderer[IndexCellsValues.CELLS][];
        readonly GameObject[][] _buildingGOs = new GameObject[IndexCellsValues.CELLS][];
        readonly bool[][] _wasActivated = new bool[IndexCellsValues.CELLS][];
        readonly bool[][] _needActive = new bool[IndexCellsValues.CELLS][];

        internal SyncBuildingVS(in SpriteRenderer[][] buildingSRCs, in EntitiesModel eM) : base(eM)
        {
            _buildingSRs = buildingSRCs;


            for (var cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                _needActive[cellIdx] = new bool[(byte)BuildingTypes.End];
                _wasActivated[cellIdx] = new bool[(byte)BuildingTypes.End];
                _buildingGOs[cellIdx] = new GameObject[(byte)BuildingTypes.End]; 

                for (var buildingTByte = (byte)1; buildingTByte < (byte)BuildingTypes.End; buildingTByte++)
                {
                    _buildingGOs[cellIdx][buildingTByte] = _buildingSRs[cellIdx][buildingTByte].gameObject;
                }   
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var buildingTByte = (byte)1; buildingTByte < (byte)BuildingTypes.End; buildingTByte++)
                {
                    _needActive[cellIdxCurrent][buildingTByte] = false;
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var isVisForMe = _e.BuildingVisibleC(cellIdxCurrent).IsVisible(_aboutGameC.CurrentPlayerIType);
                var isVisForNext = _e.BuildingVisibleC(cellIdxCurrent).IsVisible(_aboutGameC.CurrentPlayerIType.NextPlayer());

                if (_e.BuildingOnCellT(cellIdxCurrent).HaveBuilding())
                {
                    if (isVisForMe)
                    {
                        _needActive[cellIdxCurrent][(byte)_e.BuildingOnCellT(cellIdxCurrent)] = true;
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var buildingTByte = (byte)1; buildingTByte < (byte)BuildingTypes.End; buildingTByte++)
                {
                    var needActive = _needActive[cellIdxCurrent][buildingTByte];
                    ref var wasActivated = ref _wasActivated[cellIdxCurrent][buildingTByte];

                    if (wasActivated != needActive) _buildingGOs[cellIdxCurrent][buildingTByte].SetActive(needActive);

                    wasActivated = needActive;
                }
            }
        }
    }

}
