using UnityEngine;

namespace Chessy.Game
{
    sealed class UpdTryFireAroundHellMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdTryFireAroundHellMS(in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.Hell))
                {
                    foreach (var cellE in E.CellEs(idx_0).AroundCellEs)
                    {
                        if (E.AdultForestC(cellE.IdxC.Idx).HaveAnyResources)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                E.HaveFire(cellE.IdxC.Idx) = true;
                            }
                        }
                    }
                }
            }
        }
    }
}