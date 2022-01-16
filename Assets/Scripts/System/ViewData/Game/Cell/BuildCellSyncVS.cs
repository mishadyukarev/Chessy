using static Game.Game.CellEs;
using static Game.Game.CellBuildE;
using System;
using static Game.Game.CellBuildingVEs;
using UnityEngine;

namespace Game.Game
{
    struct BuildCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx in Idxs)
            {
                ref var build_0 = ref Build<BuildingC>(idx);
                ref var ownBuild_0 = ref Build<PlayerTC>(idx);


                var buildT = build_0.Build;
                var isVisForMe = Build<IsVisibledC>(WhoseMoveE.CurPlayerI, idx).IsVisibled;
                var isVisForNext = Build<IsVisibledC>(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx).IsVisibled;

                if (buildT != BuildTypes.None)
                {
                    if (isVisForMe)
                    {
                        switch (buildT)
                        {
                            case BuildTypes.None:
                                throw new Exception();

                            case BuildTypes.City:
                                BuildFront<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.City);
                                break;

                            case BuildTypes.Farm:
                                BuildFront<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.Farm);
                                break;

                            case BuildTypes.Woodcutter:
                                BuildFront<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.Woodcutter);
                                break;

                            case BuildTypes.Mine:
                                BuildFront<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.Mine);
                                break;

                            case BuildTypes.Camp:
                                BuildFront<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.Camp);
                                break;

                            default:
                                throw new Exception();
                        }
                        BuildFront<SpriteRendererVC>(idx).Enable();


                        var color = BuildFront<SpriteRendererVC>(idx).Color;
                        color.a = isVisForNext ? 1 : 0.7f;

                        BuildFront<SpriteRendererVC>(idx).Color = color;
                    }
                    else
                    {
                        BuildFront<SpriteRendererVC>(idx).Disable();
                    }
                }
                else
                {
                    BuildFront<SpriteRendererVC>(idx).Disable();
                }


                if (buildT != BuildTypes.None)
                {
                    if (isVisForMe)
                    {
                        BuildBack<SpriteRendererVC>(idx).Enable();

                        switch (buildT)
                        {
                            case BuildTypes.None:
                                throw new Exception();

                            case BuildTypes.City:
                                BuildBack<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.CityBack);
                                break;

                            case BuildTypes.Farm:
                                BuildBack<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.FarmBack);
                                break;

                            case BuildTypes.Woodcutter:
                                BuildBack<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.WoodcutterBack);
                                break;

                            case BuildTypes.Mine:
                                BuildBack<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.MineBack);
                                break;

                            case BuildTypes.Camp:
                                BuildBack<SpriteRendererVC>(idx).Sprite = SpritesResC.Sprite(SpriteTypes.CampBack);
                                break;

                            default:
                                throw new Exception();
                        }


                        var color = BuildBack<SpriteRendererVC>(idx).Color;
                        color.a = isVisForNext ? 1 : 0.7f;

                        BuildBack<SpriteRendererVC>(idx).Color = color;

                        switch (ownBuild_0.Player)
                        {
                            case PlayerTypes.None: throw new Exception();
                            case PlayerTypes.First: BuildBack<SpriteRendererVC>(idx).Color = Color.blue; return;
                            case PlayerTypes.Second: BuildBack<SpriteRendererVC>(idx).Color = Color.red; return;
                            default: throw new Exception();
                        }
                    }
                    else
                    {
                        BuildBack<SpriteRendererVC>(idx).Disable();
                    }
                }
                else
                {
                    BuildBack<SpriteRendererVC>(idx).Disable();
                }
            }
        }
    }

}
