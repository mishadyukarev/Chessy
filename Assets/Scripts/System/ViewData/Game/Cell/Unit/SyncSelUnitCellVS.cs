using System;

namespace Game.Game
{
    sealed class SyncSelUnitCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal SyncSelUnitCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView) { }

        public void Run()
        {
            if (Es.ClickerObject.CellClickC.Is(CellClickTypes.SetUnit))
            {
                var idx_cur = Es.CurrentIdxE.IdxC.Idx;

                var corner_cur = UnitEs(idx_cur).MainE.IsCorned;

                ref var mainUnit_cur = ref CellVEs(idx_cur).UnitVEs.UnitMainSR;
                ref var mainUnit_pre = ref CellVEs(Es.PreviousVisionIdxE.IdxC.Idx).UnitVEs.UnitMainSR;


                if (UnitEs(idx_cur).MainE.HaveUnit(UnitStatEs(idx_cur)))
                {
                    if (UnitEs(idx_cur).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        mainUnit_pre.Enable();
                    }

                    else
                    {
                        mainUnit_cur.Enable();
                    }
                }

                else
                {
                    mainUnit_cur.Enable();

                }


                var selUnitT = Es.SelectedUnitE.UnitTC.Unit;
                var selLevelUnitT = Es.SelectedUnitE.LevelTC.Level;

                switch (selUnitT)
                {
                    case UnitTypes.None:
                        throw new Exception();

                    case UnitTypes.King:
                        mainUnit_cur.Sprite = ResourceSpriteVEs.Sprite(selUnitT, selLevelUnitT).SpriteC.Sprite;
                        break;

                    case UnitTypes.Pawn:
                        mainUnit_cur.Sprite = ResourceSpriteVEs.Sprite(selUnitT, selLevelUnitT).SpriteC.Sprite;
                        break;

                    case UnitTypes.Archer:
                        mainUnit_cur.Sprite = ResourceSpriteVEs.Sprite(corner_cur.Is, selLevelUnitT).SpriteC.Sprite;
                        break;

                    case UnitTypes.Scout:
                        mainUnit_cur.Sprite = ResourceSpriteVEs.Sprite(selUnitT, selLevelUnitT).SpriteC.Sprite;
                        break;

                    case UnitTypes.Elfemale:
                        mainUnit_cur.Sprite = ResourceSpriteVEs.Sprite(selUnitT, selLevelUnitT).SpriteC.Sprite;
                        break;

                    default:
                        throw new Exception();
                }
            }
        }
    }
}
