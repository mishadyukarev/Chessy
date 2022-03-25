using UnityEngine;

namespace Chessy.Game
{
    sealed class UpdTryFireAroundHellMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal UpdTryFireAroundHellMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in eMGame.CellEs(cell_0).AroundCellEs)
                    {
                        if (eMGame.AdultForestC(cellE.IdxC.Idx).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                eMGame.HaveFire(cellE.IdxC.Idx) = true;
                            }
                        }
                    }
                }
            }
        }
    }
}