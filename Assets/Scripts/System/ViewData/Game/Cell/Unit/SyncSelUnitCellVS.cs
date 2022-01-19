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
                var idx_cur = CurrentIdxE.IdxC.Idx;

                ref var unitC_cur = ref Unit<UnitTC>(idx_cur);
                ref var levUnitC_cur = ref Unit<LevelTC>(idx_cur);

                ref var corner_cur = ref Unit<IsCornedArcherC>(idx_cur);

                ref var mainUnit_cur = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_cur);
                ref var mainUnit_pre = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(PreVisIdx<IdxC>().Idx);


                if (unitC_cur.Have)
                {
                    if (CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_cur).IsVisible)
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

                switch (SelectedUnitE.SelUnit<UnitTC>().Unit)
                {
                    case UnitTypes.None:
                        throw new Exception();

                    case UnitTypes.King:
                        mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.King).Sprite;
                        break;

                    case UnitTypes.Pawn:
                        switch (SelectedUnitE.SelUnit<LevelTC>().Level)
                        {
                            case LevelTypes.None: throw new Exception();
                            case LevelTypes.First: mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.PawnWood).Sprite; break;
                            case LevelTypes.Second: mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.PawnIron).Sprite; break;
                            default: throw new Exception();
                        }
                        break;
                    case UnitTypes.Archer:
                        switch (SelectedUnitE.SelUnit<LevelTC>().Level)
                        {
                            case LevelTypes.None: throw new Exception();
                            case LevelTypes.First:
                                {
                                    if (corner_cur.IsCornered) mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.RookBow).Sprite;
                                    else mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.BishopBow).Sprite;
                                }
                                break;
                            case LevelTypes.Second:
                                {
                                    if (corner_cur.IsCornered) mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.RookCrossbow).Sprite;
                                    else mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.BishopCrossbow).Sprite;
                                }
                                break;
                            default: throw new Exception();
                        }
                        break;
                    case UnitTypes.Scout:
                        switch (SelectedUnitE.SelUnit<LevelTC>().Level)
                        {
                            case LevelTypes.None: throw new Exception();
                            case LevelTypes.First: mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.Scout).Sprite; break;
                            case LevelTypes.Second: throw new Exception();
                            default: throw new Exception();
                        }
                        break;
                    case UnitTypes.Elfemale:
                        switch (levUnitC_cur.Level)
                        {
                            case LevelTypes.None: throw new Exception();
                            case LevelTypes.First: mainUnit_cur.Sprite = ResourcesSpriteVEs.SpriteVC(SpriteTypes.Elfemale).Sprite; break;
                            case LevelTypes.Second: throw new Exception();
                            default: throw new Exception();
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
    }
}
