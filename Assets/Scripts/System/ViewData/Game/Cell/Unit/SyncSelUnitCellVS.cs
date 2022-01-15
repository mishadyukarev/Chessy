using System;
using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SyncSelUnitCellVS : IEcsRunSystem
    {
        public void Run()
        {
            if (ClickerObject<CellClickC>().Is(CellClickTypes.SetUnit))
            {
                ref var unit_cur = ref Unit<UnitTC>(CurIdx<IdxC>().Idx);

                ref var mainUnit_cur = ref CellUnitVEs.UnitMain<SpriteRendererVC>(CurIdx<IdxC>().Idx);
                ref var mainUnit_pre = ref CellUnitVEs.UnitExtra<SpriteRendererVC>(PreVisIdx<IdxC>().Idx);


                if (unit_cur.Have)
                {
                    if (Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, CurIdx<IdxC>().Idx).IsVisibled)
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
                SetMainSprite(SelectedUnitE.SelUnit<UnitTC>().Unit, SelectedUnitE.SelUnit<LevelTC>().Level, false);
            }
        }

        static void SetMainSprite(in UnitTypes unit, in LevelTypes level, bool isCornered)
        {
            ref var mainUnit_cur = ref CellUnitVEs.UnitMain<SpriteRendererVC>(CurIdx<IdxC>().Idx);

            switch (unit)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.King);
                    break;

                case UnitTypes.Pawn:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.PawnWood); break;
                        case LevelTypes.Second: mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.PawnIron); break;
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Archer:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First:
                            {
                                if (isCornered) mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.RookBow);
                                else mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.BishopBow);
                            }
                            break;
                        case LevelTypes.Second:
                            {
                                if (isCornered) mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.RookCrossbow);
                                else mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.BishopCrossbow);
                            }
                            break;
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Scout:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.Scout); break;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Elfemale:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: mainUnit_cur.Sprite = SpritesResC.Sprite(SpriteTypes.Elfemale); break;
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
