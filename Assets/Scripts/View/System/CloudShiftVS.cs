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
            for (byte cellIdxCurrent_0 = 0; cellIdxCurrent_0 < IndexCellsValues.CELLS; cellIdxCurrent_0++)
            {
                if (CellC(cellIdxCurrent_0).IsBorder) continue;


                var whereDataViewC_0 = CloudViewDataC(cellIdxCurrent_0);

                if (!whereDataViewC_0.HaveDataReference) continue;


                var whereDataIdx_1 = CloudViewDataC(cellIdxCurrent_0).DataIdxCellP;
                if (!CloudC(whereDataIdx_1).IsCenterP) continue;
                var whereShiftC_1 = CloudShiftC(whereDataIdx_1);

                var whereShiftIdx_2 = whereShiftC_1.WhereNeedShiftIdxCellP;

                var pos_1 = positionCellCs[whereDataIdx_1].PositionP;
                var pos_2 = positionCellCs[whereShiftIdx_2].PositionP;

                var t = whereShiftC_1.DistanceP;

                SmoothShiftCloud(_cloudTrans[cellIdxCurrent_0], Vector3.Lerp(pos_1, pos_2, t));

                foreach (var aroundCell_1_0 in IdxsAroundCellC(whereDataIdx_1).IdxCellsAroundArray)
                {
                    var pos_1_0 = positionCellCs[aroundCell_1_0].PositionP;
                    var pos_1_1 = positionCellCs[CellsByDirectAroundC(aroundCell_1_0).Get(windC.DirectType)].PositionP;

                    SmoothShiftCloud(_cloudTrans[CloudViewDataC(aroundCell_1_0).ViewIdxCellP], Vector3.Lerp(pos_1_0, pos_1_1, t));
                }
            }


            void SmoothShiftCloud(in Transform cloundT, in Vector3 newPossition)
            {
                var t = Time.deltaTime * 7f;
                if (!PhotonNetwork.IsMasterClient) t /= 1.5f;
                if (t > 1) t = 1;

                cloundT.position = Vector3.Lerp(cloundT.position, newPossition, t);
            }
        }
    }
}