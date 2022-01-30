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
            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;


                var buildT = build_0.Build;
                var isVisForMe = Es.CellEs.BuildEs.IsVisible(Es.WhoseMove.CurPlayerI, idx_0).IsVisibleC.IsVisible;
                var isVisForNext = Es.CellEs.BuildEs.IsVisible(Es.WhoseMove.NextPlayerFrom(Es.WhoseMove.CurPlayerI), idx_0).IsVisibleC.IsVisible;

                if (Es.CellEs.BuildEs.Build(idx_0).Health.Have)
                {
                    if (isVisForMe)
                    {
                        BuildFront(idx_0).Sprite = ResourceSpriteVEs.Sprite(buildT).SpriteC.Sprite;
                        BuildBack(idx_0).Sprite = ResourceSpriteVEs.SpriteBack(buildT).SpriteC.Sprite;

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
