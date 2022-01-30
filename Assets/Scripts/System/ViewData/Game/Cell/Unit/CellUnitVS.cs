using System;
using UnityEngine;

namespace Game.Game
{
    sealed class CellUnitVS : SystemViewAbstract, IEcsRunSystem
    {
        public CellUnitVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var levelUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;

                ref var corner_0 = ref Es.CellEs.UnitEs.Main(idx_0).IsCorned;

                ref var tw_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeapon;
                ref var twLevel_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).LevelTW;

                ref var mainUnit_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extraUnit_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);


                mainUnit_0.Disable();
                extraUnit_0.Disable();

                if (unit_0.Have)
                {
                    if (Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_0).VisibleC.IsVisible)
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
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(corner_0.Is, levelUnit_0.Level).SpriteC.Sprite;
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

                        if (Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.NextPlayerFrom(Es.WhoseMove.CurPlayerI), idx_0).VisibleC.IsVisible)
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.NextPlayerFrom(Es.WhoseMove.CurPlayerI), idx_0).VisibleC.IsVisible)
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
