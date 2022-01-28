using System;
using UnityEngine;
using static Game.Game.CellBuildEs;
using static Game.Game.CellBuildingVEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct BuildCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                ref var build_cur = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_cur = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;


                var buildT = build_cur.Build;
                var isVisForMe = Entities.CellEs.BuildEs.IsVisible(Entities.WhoseMove.CurPlayerI, idx_0).IsVisibleC.IsVisible;
                var isVisForNext = Entities.CellEs.BuildEs.IsVisible(Entities.WhoseMove.NextPlayerFrom(Entities.WhoseMove.CurPlayerI), idx_0).IsVisibleC.IsVisible;

                if (buildT != BuildingTypes.None)
                {
                    if (isVisForMe)
                    {
                        BuildFront<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVEs.Sprite(buildT).SpriteC.Sprite;
                        BuildBack<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVEs.SpriteBack(buildT).SpriteC.Sprite;

                        BuildFront<SpriteRendererVC>(idx_0).Enable();
                        BuildBack<SpriteRendererVC>(idx_0).Enable();

                        var color = BuildFront<SpriteRendererVC>(idx_0).Color;
                        color.a = isVisForNext ? 1 : 0.7f;
                        BuildFront<SpriteRendererVC>(idx_0).Color = color;
                        BuildBack<SpriteRendererVC>(idx_0).Color = color;

                        switch (ownBuild_cur.Player)
                        {
                            case PlayerTypes.None: throw new Exception();
                            case PlayerTypes.First: BuildBack<SpriteRendererVC>(idx_0).Color = Color.blue; break;
                            case PlayerTypes.Second: BuildBack<SpriteRendererVC>(idx_0).Color = Color.red; break;
                            default: throw new Exception();
                        }
                    }
                    else
                    {
                        BuildFront<SpriteRendererVC>(idx_0).Disable();
                        BuildBack<SpriteRendererVC>(idx_0).Disable();
                    }
                }
                else
                {
                    BuildFront<SpriteRendererVC>(idx_0).Disable();
                    BuildBack<SpriteRendererVC>(idx_0).Disable();
                }
            }
        }
    }

}
