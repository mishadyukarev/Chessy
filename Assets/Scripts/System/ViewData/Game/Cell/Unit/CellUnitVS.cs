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
            foreach (var idx_0 in CellWorker.Idxs)
            {
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var levelUnit_0 = UnitEs(idx_0).MainE.LevelTC.Level;

                var corner_0 = UnitEs(idx_0).MainE.IsCorned;

                var tw_0 = UnitEs(idx_0).ToolWeaponE.ToolWeaponTC;
                var twLevel_0 = UnitEs(idx_0).ToolWeaponE.LevelTC;

                ref var mainUnit_0 = ref CellVEs(idx_0).UnitVEs.UnitMainSR;
                ref var extraUnit_0 = ref CellVEs(idx_0).UnitVEs.UnitExtraSR;


                mainUnit_0.Disable();
                extraUnit_0.Disable();

                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                {
                    if (UnitEs(idx_0).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        mainUnit_0.Enable();

                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None:
                                throw new Exception();

                            case UnitTypes.King:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0).SpriteC.Sprite;
                                break;

                            case UnitTypes.Pawn:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0).SpriteC.Sprite;

                                if (tw_0.HaveTW)
                                {
                                    extraUnit_0.Enable();
                                    extraUnit_0.Sprite = ResourceSpriteVEs.Sprite(tw_0.ToolWeapon, twLevel_0.Level).SpriteC.Sprite;
                                }
                                break;

                            case UnitTypes.Archer:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(corner_0.Is, levelUnit_0).SpriteC.Sprite;
                                break;

                            case UnitTypes.Scout:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0).SpriteC.Sprite;
                                break;

                            case UnitTypes.Elfemale:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0).SpriteC.Sprite;
                                break;

                            case UnitTypes.Snowy:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0).SpriteC.Sprite;
                                break;

                            case UnitTypes.Camel:
                                mainUnit_0.Sprite = ResourceSpriteVEs.Sprite(unit_0.Unit, levelUnit_0).SpriteC.Sprite; break;
                                throw new Exception();
                        }

                        if (UnitEs(idx_0).VisibleE(Es.WhoseMove.NextPlayerFrom(Es.WhoseMove.CurPlayerI)).IsVisibleC.IsVisible)
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (UnitEs(idx_0).VisibleE(Es.WhoseMove.NextPlayerFrom(Es.WhoseMove.CurPlayerI)).IsVisibleC.IsVisible)
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
