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
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.MountainC(idx_0).HaveAny)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (Random.Range(0f, 1f) <= 0.05f)
                        {
                            if (!E.MountainC(E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx).HaveAny && !E.BuildTC(idx_1).HaveBuilding)
                            {
                                E.HillC(idx_1).Resources += CellEnvironment_Values.ADDING_FROM_MOUNTAIN;
                            }
                        }
                    }

                    foreach (var cellE in E.CellEs(idx_0).AroundCellEs)
                    {
                       
                    }
                }
            }
        }
    }
}