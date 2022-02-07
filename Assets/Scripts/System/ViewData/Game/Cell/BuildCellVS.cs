using System;
using UnityEngine;
using static Game.Game.CellBuildingVEs;

namespace Game.Game
{
    sealed class BuildCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal BuildCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var curPlayerI = Es.WhoseMoveE.CurPlayerI;

                var build_0 = BuildEs(idx_0).BuildingE.BuildTC;
                var ownBuild_0 = BuildEs(idx_0).BuildingE.OwnerC;


                var buildT = build_0.Build;
                var isVisForMe = BuildEs(idx_0).BuildingVisE(curPlayerI).IsVisibleC.IsVisible;
                var isVisForNext = BuildEs(idx_0).BuildingVisE(Es.WhoseMoveE.NextPlayerFrom(curPlayerI)).IsVisibleC.IsVisible;

                for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
                {
                    VEs.BuildingE(idx_0, build).SR.Disable();
                }

                if (Es.BuildE(idx_0).HaveBuilding)
                {
                    if (isVisForMe)
                    {
                        VEs.BuildingE(idx_0, buildT).SR.Enable();
                    }
                }
            }
        }
    }

}
