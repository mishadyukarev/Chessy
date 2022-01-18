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
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.CityBack);
                                break;

                            case BuildingTypes.Farm:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.Farm);
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.FarmBack);
                                break;

                            case BuildingTypes.Woodcutter:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.Woodcutter);
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.WoodcutterBack);
                                break;

                            case BuildingTypes.Mine:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.Mine);
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.MineBack);
                                break;

                            case BuildingTypes.Camp:
                                BuildFront<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.Camp);
                                BuildBack<SpriteRendererVC>(idx_0).Sprite = SpritesResC.Sprite(SpriteTypes.CampBack);
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
