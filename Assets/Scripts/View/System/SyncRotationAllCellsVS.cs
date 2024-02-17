using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncRotationAllCellsVS : SystemViewAbstract
    {
        readonly GameObjectVC[] _cellGOs;

        internal SyncRotationAllCellsVS(in GameObjectVC[] cellGOs, in EntitiesModel eM) : base(eM)
        {
            _cellGOs = cellGOs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _cellGOs[cellIdxCurrent].Transform.rotation = aboutGameC.CurrentPlayerIType == PlayerTypes.First
                    ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            }
        }
    }
}