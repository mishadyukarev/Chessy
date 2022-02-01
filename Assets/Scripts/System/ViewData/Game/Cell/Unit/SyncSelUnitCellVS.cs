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

                var corner_cur = UnitEs.Main(idx_cur).IsCorned;

                ref var mainUnit_cur = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_cur);
                ref var mainUnit_pre = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(Es.PreviousVisionIdxE.IdxC.Idx);


                if (UnitEs.Main(idx_cur).HaveUnit(UnitStatEs))
                {
                    if (UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_cur).IsVisibleC.IsVisible)
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
