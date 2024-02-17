using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Model.Entity
{
    public sealed class EntitiesModel
    {
        readonly PlayerInfoE[] _forPlayerEs = new PlayerInfoE[(byte)PlayerTypes.End];
        readonly CellEs[] _cellEs = new CellEs[IndexCellsValues.CELLS];

        public readonly CommonGameE CommonGameE;
        public readonly WeatherE WeatherE = new();

        public CellEs CellEs(in byte idx) => _cellEs[idx];
        public PlayerInfoE PlayerInfoE(in byte idx) => _forPlayerEs[idx];


        public EntitiesModel(in DataFromViewC dataFromViewC, in string nameRpcMethod, in List<object> actions, in TestModeTypes testModeT)
        {
            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs[(byte)playerT] = new PlayerInfoE();
            }

            var idxs = new byte[IndexCellsValues.X_AMOUNT, IndexCellsValues.Y_AMOUNT];

            var xys = new List<byte[]>();

            byte idxCell = 0;
            for (byte x = 0; x < IndexCellsValues.X_AMOUNT; x++)
                for (byte y = 0; y < IndexCellsValues.Y_AMOUNT; y++)
                {
                    idxs[x, y] = idxCell;
                    xys.Add(new byte[] { x, y });
                    idxCell++;
                }


            CommonGameE = new CommonGameE(dataFromViewC, testModeT, DateTime.Now, actions, nameRpcMethod, idxs);


            for (idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
            {
                _cellEs[idxCell] = new CellEs(dataFromViewC, dataFromViewC.IdCell(idxCell), idxCell, this, xys[idxCell]);
            }




            for (byte startCellIdx_0 = 0; startCellIdx_0 < IndexCellsValues.CELLS; startCellIdx_0++)
            {
                var cellEs_0 = CellEs(startCellIdx_0);

                if (cellEs_0.CellE.CellC.IsBorder) continue;

                var aroudIdxs = new byte[(byte)DirectTypes.End - 1];
                var aroundByDirectIdxs = new byte[(byte)DirectTypes.End];

                for (byte currentCellIdx_1 = 0; currentCellIdx_1 < IndexCellsValues.CELLS; currentCellIdx_1++)
                {
                    if (CellEs(currentCellIdx_1).CellE.CellC.IsBorder) continue;

                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                    {
                        if (cellEs_0.AroundCellE(currentCellIdx_1).CellAroundC.DirectT == directT)
                        {
                            if (cellEs_0.AroundCellE(currentCellIdx_1).CellAroundC.LevelFromCellT == DistanceFromCellTypes.First)
                            {
                                aroudIdxs[(byte)directT - 1] = currentCellIdx_1;
                                aroundByDirectIdxs[(byte)directT] = currentCellIdx_1;
                            }
                        }
                    }
                }

                aroundByDirectIdxs[(byte)DirectTypes.None] = startCellIdx_0;

                cellEs_0.CellE.IdxsAroundCellC = new IdxsAroundCellC(aroudIdxs);
                cellEs_0.CellE.CellsByDirectAroundC = new CellsByDirectAroundC(aroundByDirectIdxs);
            }
        }

        internal void Dispose()
        {
            CommonGameE.Dispose();
            WeatherE.Dispose();

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                CellEs(cellIdxCurrent).Dispose();
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs[(byte)playerT].Dispose();
            }
        }
    }
}