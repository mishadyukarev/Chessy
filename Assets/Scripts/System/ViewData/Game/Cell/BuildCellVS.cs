using System;
using UnityEngine;
using static Game.Game.CellBuildE;
using static Game.Game.CellBuildingVEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct BuildCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var build_cur = ref Build<BuildingTC>(idx_0);
                ref var ownBuild_cur = ref Build<PlayerTC>(idx_0);


                var buildT = build_cur.Build;
                var isVisForMe = IsVisible<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_0).IsVisible;
                var isVisForNext = IsVisible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible;

                if (buildT != BuildingTypes.None)
                {
                    if (isVisForMe)
                    {
                        switch (buildT)
                        {
                            case BuildingTypes.None:
                                throw new Exception();

                            case BuildingTypes.City:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.Sprite(BuildingTypes.City).SpriteC.Sprite;
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.SpriteBack(BuildingTypes.City).SpriteC.Sprite;
                                break;

                            case BuildingTypes.Farm:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.Sprite(BuildingTypes.Farm).SpriteC.Sprite;
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.SpriteBack(BuildingTypes.Farm).SpriteC.Sprite;
                                break;

                            case BuildingTypes.Woodcutter:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.Sprite(BuildingTypes.Woodcutter).SpriteC.Sprite;
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.SpriteBack(BuildingTypes.Woodcutter).SpriteC.Sprite;
                                break;

                            case BuildingTypes.Mine:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.Sprite(BuildingTypes.Mine).SpriteC.Sprite;
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.SpriteBack(BuildingTypes.Mine).SpriteC.Sprite;
                                break;

                            case BuildingTypes.Camp:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.Sprite(BuildingTypes.Camp).SpriteC.Sprite;
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = ResourceSpriteVPool.SpriteBack(BuildingTypes.Camp).SpriteC.Sprite;
                                break;

                            default:
                                throw new Exception();
                        }
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
