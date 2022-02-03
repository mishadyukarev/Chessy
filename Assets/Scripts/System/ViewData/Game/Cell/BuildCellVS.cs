using System;
using UnityEngine;
using static Game.Game.CellBuildingVEs;

namespace Game.Game
{
    sealed class BuildCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public BuildCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var build_0 = BuildEs(idx_0).BuildingE.BuildTC;
                var ownBuild_0 = BuildEs(idx_0).BuildingE.OwnerC;


                var buildT = build_0.Build;
                var isVisForMe = BuildEs(idx_0).BuildingVisE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible;
                var isVisForNext = BuildEs(idx_0).BuildingVisE(Es.WhoseMove.NextPlayerFrom(Es.WhoseMove.CurPlayerI)).IsVisibleC.IsVisible;

                if (BuildEs(idx_0).BuildingE.HaveBuilding)
                {
                    if (isVisForMe)
                    {
                        BuildFront(idx_0).Sprite = VEs.ResourceSpriteEs.Sprite(buildT).SpriteC.Sprite;
                        BuildBack(idx_0).Sprite = VEs.ResourceSpriteEs.SpriteBack(buildT).SpriteC.Sprite;

                        BuildFront(idx_0).Enable();
                        BuildBack(idx_0).Enable();

                        var color = BuildFront(idx_0).Color;
                        color.a = isVisForNext ? 1 : 0.7f;
                        BuildFront(idx_0).Color = color;
                        BuildBack(idx_0).Color = color;

                        switch (ownBuild_0.Player)
                        {
                            case PlayerTypes.None: throw new Exception();
                            case PlayerTypes.First: BuildBack(idx_0).Color = Color.blue; break;
                            case PlayerTypes.Second: BuildBack(idx_0).Color = Color.red; break;
                            default: throw new Exception();
                        }
                    }
                    else
                    {
                        BuildFront(idx_0).Disable();
                        BuildBack(idx_0).Disable();
                    }
                }
                else
                {
                    BuildFront(idx_0).Disable();
                    BuildBack(idx_0).Disable();
                }
            }
        }
    }

}
