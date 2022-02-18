﻿using UnityEngine;

namespace Game.Game
{
    sealed class UpdTryFireAroundHellMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdTryFireAroundHellMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitTC(idx_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in Es.CellEs(idx_0).AroundCellEs)
                    {
                        if (Es.AdultForestC(cellE.IdxC.Idx).HaveAny)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                Es.HaveFire(cellE.IdxC.Idx) = true;
                            }
                        }
                    }
                }
            }
        }
    }
}