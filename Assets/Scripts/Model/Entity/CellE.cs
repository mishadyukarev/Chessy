using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    public sealed class CellE
    {
        public readonly CellC CellC;
        public readonly XyCellC XyCellC;
        public readonly IdxCellC IdxCellC;
        public readonly IsStartedCellC IsStartedCellC;
        public readonly PositionC PositionC;

        internal CellE(in DataFromViewC dataFromViewC, in byte idxCell, in int instanceID, params byte[] xy)
        {
            CellC = new CellC(dataFromViewC.IsBorder(idxCell), instanceID);
            XyCellC = new XyCellC(xy);
            IdxCellC = new IdxCellC(idxCell);
            PositionC = new PositionC() { Position = dataFromViewC.PossitionCell(idxCell) };

            var x = xy[0];
            var y = xy[1];

            var isStartedCell = new bool[(byte)PlayerTypes.End];

            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                isStartedCell[(byte)playerT] = false;

                if (playerT == PlayerTypes.First)
                {
                    if (y < 3 && x > 3 && x < 12)
                    {
                        isStartedCell[(byte)playerT] = true;
                    }
                }
                else
                {
                    if (y > 7 && x > 3 && x < 12)
                    {
                        isStartedCell[(byte)playerT] = true;
                    }
                }
            }

            IsStartedCellC = new IsStartedCellC(isStartedCell);
        }
    }
}