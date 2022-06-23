﻿using Chessy.Model.Model.Component;
using System.Collections.Generic;

namespace Chessy.Model.Entity
{
    public readonly struct CellE
    {
        public readonly bool IsBorder;
        public readonly IdxCellC IdxC;
        public readonly XyCellC XyC;
        public readonly int InstanceID;
        public readonly IsStartedCellC IsStartedCellC;

        internal CellE(in DataFromViewC dataFromViewC, in byte idxCell, in int instanceID, params byte[] xy)
        {
            IsBorder = dataFromViewC.IsBorder(idxCell);

            var x = xy[0];
            var y = xy[1];

            var isStartedCell = new Dictionary<PlayerTypes, bool>();

            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                isStartedCell.Add(playerT, false);

                if (playerT == PlayerTypes.First)
                {
                    if (y < 3 && x > 3 && x < 12)
                    {
                        isStartedCell[playerT] = true;
                    }
                }
                else
                {
                    if (y > 7 && x > 3 && x < 12)
                    {
                        isStartedCell[playerT] = true;
                    }
                }
            }

            IdxC = new IdxCellC(idxCell);
            XyC = new XyCellC(xy);
            InstanceID = instanceID;
            IsStartedCellC = new IsStartedCellC(isStartedCell);
        }
    }
}