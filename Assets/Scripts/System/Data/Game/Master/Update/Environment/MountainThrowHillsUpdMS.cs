using UnityEngine;

namespace Game.Game
{
    sealed class MountainThrowHillsUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal MountainThrowHillsUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.MountainC(idx_0).HaveAny)
                {
                    foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (Random.Range(0f, 1f) <= 0.05f)
                        {
                            if (!Es.MountainC(idx_1).HaveAny && !Es.BuildTC(idx_1).HaveBuilding)
                            {
                                Es.HillC(idx_1).Resources += CellEnvironment_Values.ADDING_FROM_MOUNTAIN;
                            }
                        }
                    }
                }
            }
        }
    }
}