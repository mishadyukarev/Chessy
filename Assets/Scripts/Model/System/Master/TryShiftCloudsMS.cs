using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    sealed class TryShiftCloudsMS : SystemModelAbstract
    {
        internal TryShiftCloudsMS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {

        }

        internal void TryShift()
        {
            for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            {
                if (cellCs[cell_0].IsBorder) continue;
                if (!cloudCs[cell_0].IsCenter) continue;


                var cell_1 = shiftCloudCs[cell_0].WhereNeedShiftIdxCell;
                var directXy_1 = xyCellsCs[cell_1].Xy;


                var isInSquareNextCell = directXy_1[0] >= 4 && directXy_1[0] <= 11 && directXy_1[1] >= 3 && directXy_1[1] <= 7;

                if (isInSquareNextCell)
                {
                    var adding = Time.deltaTime * windC.Speed * 0.25f;

                    shiftCloudCs[cell_0].Distance += adding;

                    if (shiftCloudCs[cell_0].Distance >= 1)
                    {
                        var savedAroundComponents = new List<WhereViewIdxCellC>();

                        foreach (var aroundCell_0_0 in idxsAroundCellCs[cell_0].IdxCellsAroundArray)
                        {
                            savedAroundComponents.Add(cloudWhereViewDataCs[aroundCell_0_0].Clone());

                            //savedAroundComponents.Add(CloudViewDataC(aroundCell_0_0));
                            //CloudViewDataC(CloudViewDataC(aroundCell_0_0).ViewIdxCell).Dispose();

                            cloudWhereViewDataCs[aroundCell_0_0].Dispose();
                            cloudCs[aroundCell_0_0].Dispose();
                        }

                        var viewIdx_0_1 = cloudWhereViewDataCs[cell_0].ViewIdxCell;


                        cloudCs[cell_1].Copy(cloudCs[cell_0]);
                        cloudWhereViewDataCs[cell_1].Copy(cloudWhereViewDataCs[cell_0]);
                        shiftCloudCs[cell_1].Copy(shiftCloudCs[cell_0]);

                        cloudCs[cell_0].Dispose();
                        cloudWhereViewDataCs[cell_0].Dispose();
                        shiftCloudCs[cell_0].Dispose();

                        cloudWhereViewDataCs[viewIdx_0_1].DataIdxCell = cell_1;
                        shiftCloudCs[cell_1].Dispose();
                        shiftCloudCs[cell_1].WhereNeedShiftIdxCell = cellsByDirectAroundC[cell_1].Get(windC.DirectT);



                        var idxArray = 0;
                        foreach (var aroundCell_1_0 in idxsAroundCellCs[cell_1].IdxCellsAroundArray)
                        {
                            var savedViewIdx = savedAroundComponents[idxArray].ViewIdxCell;

                            cloudWhereViewDataCs[aroundCell_1_0].ViewIdxCell = savedViewIdx;
                            cloudWhereViewDataCs[savedViewIdx].DataIdxCell = aroundCell_1_0;

                            idxArray++;
                        }
                    }
                }
                else
                {
                    windC.DirectT = (DirectTypes)Random.Range(1, (byte)DirectTypes.End);


                    shiftCloudCs[cell_0].WhereNeedShiftIdxCell = cellsByDirectAroundC[cell_0].Get(windC.DirectT);
                }
            }
        }
    }
}