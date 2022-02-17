using UnityEngine;

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
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.AdultForestC(idx_1).HaveAny)
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= 0.005f)
                            {
                                Es.HaveFire(idx_1) = true;
                            }
                        }
                    }
                }
            }
        }
    }
}