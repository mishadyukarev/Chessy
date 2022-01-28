using System;
using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct CellUnitVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var levelUnit_0 = ref CellUnitEs.Else(idx_0).LevelC;

                ref var corner_0 = ref CellUnitEs.Else(idx_0).CornedC;

                ref var tw_0 = ref CellUnitEs.ToolWeapon(idx_0).ToolWeaponC;
                ref var twLevel_0 = ref CellUnitEs.ToolWeapon(idx_0).LevelC;

                ref var mainUnit_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extraUnit_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);


                mainUnit_0.Disable();
                extraUnit_0.Disable();

                if (unit_0.Have)
                {
                    if (CellUnitEs.VisibleE(Entities.WhoseMove.CurPlayerI, idx_0).VisibleC.IsVisible)
                    {
                        mainUnit_0.Enable();

                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None:
                                throw new Exception();

                            case UnitTypes.King:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Pawn:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;

                                if (tw_0.HaveTW)
                                {
                                    extraUnit_0.Enable();
                                    extraUnit_0.Sprite = ResourceSpriteVEs.Sprite(tw_0.ToolWeapon, twLevel_0.Level).SpriteC.Sprite;
                                }
                                break;

                            case UnitTypes.Archer:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(corner_0.IsCornered, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Scout:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Elfemale:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Snowy:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite;
                                break;

                            case UnitTypes.Camel:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0.Level).SpriteC.Sprite; break;
                                throw new Exception();
                        }

                        if (CellUnitEs.VisibleE(Entities.WhoseMove.NextPlayerFrom(Entities.WhoseMove.CurPlayerI), idx_0).VisibleC.IsVisible)
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (CellUnitEs.VisibleE(Entities.WhoseMove.NextPlayerFrom(Entities.WhoseMove.CurPlayerI), idx_0).VisibleC.IsVisible)
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
