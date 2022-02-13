using UnityEditor;
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
                if (Es.MountainE(idx_0).HaveEnvironment)
                {
                    foreach (var idx_1 in Es.CellSpaceWorker.GetIdxsAround(idx_0))
                    {
                        if (Random.Range(0f, 1f) <= 0.05f)
                        {
                            if (!Es.MountainE(idx_1).HaveEnvironment && !Es.BuildingE(idx_1).HaveBuilding)
                            {
                                Es.HillE(idx_1).AddFromMountain();
                            }
                        }
                    }
                }
            }
        }
    }
}