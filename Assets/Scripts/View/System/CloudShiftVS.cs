using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using Photon.Pun;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class CloudShiftVS : SystemViewAbstract
    {
        readonly Transform[] _cloudTrans = new Transform[IndexCellsValues.CELLS];

        internal CloudShiftVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _cloudTrans[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).CloudSRC.Transform;
            }
        }

        internal override void Sync()
        {
            var t = Time.deltaTime * 7f;
            if (!PhotonNetwork.IsMasterClient) t /= 1.5f;
            if (t > 1) t = 1;

            for (byte cellIdxCurrent_0 = 0; cellIdxCurrent_0 < IndexCellsValues.CELLS; cellIdxCurrent_0++)
            {
                if (_cellCs[cellIdxCurrent_0].IsBorder) continue;

                if (_cloudCs[cellIdxCurrent_0].IsCenterP)
                {
                    var whereSkinIdxCell = _cloudWhereViewDataCs[cellIdxCurrent_0].ViewIdxCellP;

                    var curCloudTrans = _cloudTrans[whereSkinIdxCell];
                    var curPos = curCloudTrans.position;
                    var nextPos = _e.CloudPossitionC(whereSkinIdxCell).PositionP;

                    curCloudTrans.position = Vector3.Lerp(curPos, nextPos, t);


                    foreach (var item in _e.IdxsCellsAround(cellIdxCurrent_0))
                    {
                        whereSkinIdxCell = _cloudWhereViewDataCs[item].ViewIdxCellP;

                        curCloudTrans.position = Vector3.Lerp(curPos, nextPos, t);
                    }
                }
            }
        }
    }
}