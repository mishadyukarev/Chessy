using System;
using UnityEngine;

namespace Game.Game
{
    sealed class CellUnitVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var unit_0 = UnitEs(idx_0).TypeE.UnitTC;
                var levelUnit_0 = UnitEs(idx_0).LevelE.LevelTC.Level;

                var isCorned = UnitEs(idx_0).CornedE.IsCornered;

                var tw_0 = UnitEs(idx_0).ToolWeaponE.ToolWeaponTC;
                var twLevel_0 = UnitEs(idx_0).ToolWeaponE.LevelTC;


                for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                {
                    VEs.UnitE(idx_0, unitT).Disable();
                }

                //ref var mainUnit_0 = ref CellVEs(idx_0).UnitVEs.UnitMainSR;
                //ref var extraUnit_0 = ref CellVEs(idx_0).UnitVEs.UnitExtraSR;


                //mainUnit_0.Disable();
                //extraUnit_0.Disable();

                if (Es.UnitTypeE(idx_0).HaveUnit)
                {
                    if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        var unitT = Es.UnitTypeE(idx_0).UnitT;

                        VEs.UnitE(idx_0, unitT).Enable();

                        if (unit_0.Is(UnitTypes.Archer))
                        {
                           
                        }
                        else
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                if (tw_0.HaveTW)
                                {
                                    //extraUnit_0.Sprite = VEs.ResourceSpriteEs.Sprite(tw_0.ToolWeapon, twLevel_0.Level).SpriteC.Sprite;
                                }
                            }
                        }

                        if (UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(Es.WhoseMoveE.CurPlayerI)).IsVisibleC.IsVisible)
                        {
                            //mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            //mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(Es.WhoseMoveE.CurPlayerI)).IsVisibleC.IsVisible)
                        {
                            //extraUnit_0.Color = new Color(extraUnit_0.Color.r, extraUnit_0.Color.g, extraUnit_0.Color.b, 1);
                        }
                        else
                        {
                            //extraUnit_0.Color = new Color(extraUnit_0.Color.r, extraUnit_0.Color.g, extraUnit_0.Color.b, 0.6f);
                        }
                    }
                }
            }
        }
    }
}
