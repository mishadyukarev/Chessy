using UnityEngine;

namespace Chessy.Game
{
    sealed class MountainThrowHillsUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal MountainThrowHillsUpdMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.MountainC(idx_0).HaveAnyResources)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (Random.Range(0f, 1f) <= 0.05f)
                        {
                            if (!E.MountainC(E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx).HaveAnyResources && !E.BuildingTC(idx_1).HaveBuilding)
                            {
                                E.HillC(idx_1).Resources += Environment_Values.ADDING_FROM_MOUNTAIN;
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