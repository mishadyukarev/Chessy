﻿using System.Linq;

namespace Game.Game
{
    sealed class UpdGiveWaterCloudScowyMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdGiveWaterCloudScowyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitTC(idx_0).Is(UnitTypes.Snowy))
                {
                    if (Es.CellEs(Es.CenterCloudIdxC.Idx).AroundCellEs.Any(e => e.IdxC.Idx == idx_0))
                    {
                        //Es.UnitE(idx_0).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                    }
                    
                }
            }
        }
    }
}