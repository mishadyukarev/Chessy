using System;
using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    struct CellUnitVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var levelUnit_0 = ref CellUnitEntities.Else(idx_0).LevelC;

                ref var corner_0 = ref CellUnitEntities.Else(idx_0).CornedC;

                ref var tw_0 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref CellUnitTWE.UnitTW<LevelTC>(idx_0);

                ref var mainUnit_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extraUnit_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);


                mainUnit_0.Disable();
                extraUnit_0.Disable();

                if (unit_0.Have)
                {
                    if (CellUnitVisibleEs.Visible(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                    {
                        mainUnit_0.Enable();

                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None:
                                throw new Exception();

                            case UnitTypes.King:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Pawn:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;

                                if (tw_0.HaveTW)
                                {
                                    extraUnit_0.Enable();
                                    extraUnit_0.Sprite = ResourceSpriteVPool.Sprite(tw_0.ToolWeapon, twLevel_0.Level).SpriteC.Sprite;
                                }
                                break;

                            case UnitTypes.Archer:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(corner_0.IsCornered, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Scout:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Elfemale:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Snowy:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Camel:
                                mainUnit_0.Sprite = ResourceSpriteVPool.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite; break;
                                throw new Exception();
                        }

                        if (CellUnitVisibleEs.Visible(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible)
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (CellUnitVisibleEs.Visible(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible)
                        {
                            extraUnit_0.Color = new Color(extraUnit_0.Color.r, extraUnit_0.Color.g, extraUnit_0.Color.b, 1);
                        }
                        else
                        {
                            extraUnit_0.Color = new Color(extraUnit_0.Color.r, extraUnit_0.Color.g, extraUnit_0.Color.b, 0.6f);
                        }
                    }
                }
            }
        }
    }
}
