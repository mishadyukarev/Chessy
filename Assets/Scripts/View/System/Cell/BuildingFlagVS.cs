using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class BuildingFlagVS : SystemViewAbstract
    {
        bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _flagSRCs;

        internal BuildingFlagVS(in SpriteRendererVC[] flagSRCs, in EntitiesModel eM) : base(eM)
        {
            _flagSRCs = flagSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (buildingCs[cellIdxCurrent].HaveBuilding)
                {
                    _needActive[cellIdxCurrent] = true;
                    _flagSRCs[cellIdxCurrent].Color = buildingCs[cellIdxCurrent].PlayerType == PlayerTypes.First ? Color.blue : Color.red;
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _flagSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }


        }
    }
}