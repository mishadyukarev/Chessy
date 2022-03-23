using Chessy.Game.Entity.Model;
using UnityEngine;

namespace Chessy.Game
{
    sealed class MountainThrowHillsUpdMS
    {
        internal MountainThrowHillsUpdMS(in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (e.CellEs(idx_0).IsActiveParentSelf)
            {
                if (e.MountainC(idx_0).HaveAnyResources)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                        if (Random.Range(0f, 1f) <= 0.5)
                        {
                            if (!e.MountainC(e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx).HaveAnyResources && !e.BuildingTC(idx_1).HaveBuilding)
                            {
                                e.HillC(idx_1).Resources = Random.Range(0f, 1f);
                            }
                        }
                    }

                    foreach (var cellE in e.CellEs(idx_0).AroundCellEs)
                    {

                    }
                }
            }
        }
    }
}