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
                if (CellC(cell_0).IsBorder) continue;
                if (!CloudC(cell_0).IsCenter) continue;


                var cell_1 = CloudShiftC(cell_0).WhereNeedShiftIdxCell;
                var directXy_1 = XyCellC(cell_1).Xy;


                var isInSquareNextCell = directXy_1[0] >= 4 && directXy_1[0] <= 11 && directXy_1[1] >= 3 && directXy_1[1] <= 7;

                if (isInSquareNextCell)
                {
                    var adding = Time.deltaTime * windC.Speed * 0.25f;

                    CloudShiftC(cell_0).Distance += adding;

                    if (CloudShiftC(cell_0).Distance >= 1)
                    {
                        var savedAroundComponents = new List<WhereViewIdxCellC>();

                        foreach (var aroundCell_0_0 in IdxsAroundCellC(cell_0).IdxCellsAroundArray)
                        {
                            savedAroundComponents.Add(CloudViewDataC(aroundCell_0_0).Clone());

                            //savedAroundComponents.Add(CloudViewDataC(aroundCell_0_0));
                            //CloudViewDataC(CloudViewDataC(aroundCell_0_0).ViewIdxCell).Dispose();

                            CloudViewDataC(aroundCell_0_0).Dispose();
                            CloudC(aroundCell_0_0).Dispose();
                        }

                        var viewIdx_0_1 = CloudViewDataC(cell_0).ViewIdxCell;


                        CloudC(cell_1).Copy(CloudC(cell_0));
                        CloudViewDataC(cell_1).Copy(CloudViewDataC(cell_0));
                        CloudShiftC(cell_1).Copy(CloudShiftC(cell_0));

                        CloudC(cell_0).Dispose();
                        CloudViewDataC(cell_0).Dispose();
                        CloudShiftC(cell_0).Dispose();

                        CloudViewDataC(viewIdx_0_1).DataIdxCell = cell_1;
                        CloudShiftC(cell_1).Dispose();
                        CloudShiftC(cell_1).WhereNeedShiftIdxCell = CellsByDirectAroundC(cell_1).Get(windC.DirectT);



                        var idxArray = 0;
                        foreach (var aroundCell_1_0 in IdxsAroundCellC(cell_1).IdxCellsAroundArray)
                        {
                            var savedViewIdx = savedAroundComponents[idxArray].ViewIdxCell;

                            CloudViewDataC(aroundCell_1_0).ViewIdxCell = savedViewIdx;
                            CloudViewDataC(savedViewIdx).DataIdxCell = aroundCell_1_0;

                            idxArray++;
                        }
                    }
                }
                else
                {
                    windC.DirectT = (DirectTypes)Random.Range(1, (byte)DirectTypes.End);


                    CloudShiftC(cell_0).WhereNeedShiftIdxCell = CellsByDirectAroundC(cell_0).Get(windC.DirectT);
                }
            }
        }
    }
}