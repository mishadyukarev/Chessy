using System;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SyncSelUnitCellVS : IEcsRunSystem
    {
        public void Run()
        {
            if (ClickerObject<CellClickC>().Is(CellClickTypes.SetUnit))
            {
                var idx_cur = Entities.CurrentIdxE.IdxC.Idx;

                ref var unitC_cur = ref CellUnitEs.Else(idx_cur).UnitC;
                ref var levUnitC_cur = ref CellUnitEs.Else(idx_cur).LevelC;

                ref var corner_cur = ref CellUnitEs.Else(idx_cur).CornedC;

                ref var mainUnit_cur = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_cur);
                ref var mainUnit_pre = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(PreVisIdx<IdxC>().Idx);


                if (unitC_cur.Have)
                {
                    if (CellUnitEs.VisibleE(Entities.WhoseMoveE.CurPlayerI, idx_cur).VisibleC.IsVisible)
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


                var selUnitT = SelectedUnitE.SelUnit<UnitTC>().Unit;
                var selLevelUnitT = SelectedUnitE.SelUnit<LevelTC>().Level;

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
                        mainUnit_cur.Sprite = ResourceSpriteVEs.Sprite(corner_cur.IsCornered, selLevelUnitT).SpriteC.Sprite;
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
