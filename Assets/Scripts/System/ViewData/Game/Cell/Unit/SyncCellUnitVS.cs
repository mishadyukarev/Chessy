using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellVEs;

namespace Game.Game
{
    struct SyncCellUnitVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var levelUnit_0 = ref Unit<LevelTC>(idx_0);

                ref var corner_0 = ref Unit<IsCornedArcherC>(idx_0);

                ref var tw_0 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref CellUnitTWE.UnitTW<LevelTC>(idx_0);

                ref var mainUnit_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extraUnit_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);


                mainUnit_0.Disable();
                extraUnit_0.Disable();

                if (unit_0.Have)
                {
                    if (Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled)
                    {
                        mainUnit_0.Enable();

                        if (unit_0.Is(UnitTypes.Pawn))
                        {
                            SetMainSprite(idx_0, unit_0.Unit, levelUnit_0.Level, false);

                            if (tw_0.HaveTW)
                            {
                                extraUnit_0.Enable();
                                switch (tw_0.ToolWeapon)
                                {
                                    case TWTypes.None: throw new Exception();
                                    case TWTypes.Pick:
                                        switch (twLevel_0.Level)
                                        {
                                            case LevelTypes.None: throw new Exception();
                                            case LevelTypes.First: throw new Exception();
                                            case LevelTypes.Second: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.PickWood); return;
                                            default: throw new Exception();
                                        }
                                    case TWTypes.Sword:
                                        switch (twLevel_0.Level)
                                        {
                                            case LevelTypes.None: throw new Exception();
                                            case LevelTypes.First: throw new Exception();
                                            case LevelTypes.Second: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.SwordIron); return;
                                            default: throw new Exception();
                                        }
                                    case TWTypes.Shield:
                                        switch (twLevel_0.Level)
                                        {
                                            case LevelTypes.None: throw new Exception();
                                            case LevelTypes.First: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.ShieldWood); return;
                                            case LevelTypes.Second: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.ShieldIron); return;
                                            default: throw new Exception();
                                        }
                                    default: throw new Exception();
                                }
                            }
                        }

                        else if (unit_0.Is(UnitTypes.Archer))
                        {
                            SetMainSprite(idx_0, unit_0.Unit, levelUnit_0.Level, corner_0.IsCornered);
                        }

                        else
                        {
                            SetMainSprite(idx_0, unit_0.Unit, levelUnit_0.Level, false);
                        }


                        //mainUnit_0.SetAlpha(Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI), idx_0).IsVisibled);
                        //extraUnit_0.SetAlpha(Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI), idx_0).IsVisibled);
                    }
                }
            }
        }


        void SetMainSprite(in byte idx, in UnitTypes unit, in LevelTypes level, bool isCornered)
        {
            ref var mainUnit_cur = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx);

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
        

        //public void SetAlpha(bool isVisible)
        //{
        //    if (isVisible) _extraUnit.color = new Color(_extraUnit.color.r, _extraUnit.color.g, _extraUnit.color.b, 1);
        //    else _extraUnit.color = new Color(_extraUnit.color.r, _extraUnit.color.g, _extraUnit.color.b, 0.8f);
        //}
        //public void SetFlipX(bool isFliped) => _extraUnit.flipX = isFliped;
    }
}
