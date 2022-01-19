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
                    if (CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                    {
                        mainUnit_0.Enable();

                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None:
                                throw new Exception();

                            case UnitTypes.King:
                                mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.King);
                                break;

                            case UnitTypes.Pawn:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.PawnWood); break;
                                    case LevelTypes.Second: mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.PawnIron); break;
                                    default: throw new Exception();
                                }

                                if (tw_0.HaveTW)
                                {
                                    extraUnit_0.Enable();
                                    switch (tw_0.ToolWeapon)
                                    {
                                        case ToolWeaponTypes.None: throw new Exception();
                                        case ToolWeaponTypes.Pick:
                                            switch (twLevel_0.Level)
                                            {
                                                case LevelTypes.None: throw new Exception();
                                                case LevelTypes.First: throw new Exception();
                                                case LevelTypes.Second: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.PickWood); break;
                                                default: throw new Exception();
                                            }
                                            break;
                                        case ToolWeaponTypes.Sword:
                                            switch (twLevel_0.Level)
                                            {
                                                case LevelTypes.None: throw new Exception();
                                                case LevelTypes.First: throw new Exception();
                                                case LevelTypes.Second: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.SwordIron); break;
                                                default: throw new Exception();
                                            }
                                            break;
                                        case ToolWeaponTypes.Shield:
                                            switch (twLevel_0.Level)
                                            {
                                                case LevelTypes.None: throw new Exception();
                                                case LevelTypes.First: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.ShieldWood); break;
                                                case LevelTypes.Second: extraUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.ShieldIron); break;
                                                default: throw new Exception();
                                            }
                                            break;
                                        default: throw new Exception();
                                    }
                                }

                                break;
                            case UnitTypes.Archer:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First:
                                        {
                                            if (corner_0.IsCornered) mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.RookBow);
                                            else mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.BishopBow);
                                        }
                                        break;
                                    case LevelTypes.Second:
                                        {
                                            if (corner_0.IsCornered) mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.RookCrossbow);
                                            else mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.BishopCrossbow);
                                        }
                                        break;
                                    default: throw new Exception();
                                }
                                break;
                            case UnitTypes.Scout:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.Scout); break;
                                    case LevelTypes.Second: throw new Exception();
                                    default: throw new Exception();
                                }
                                break;
                            case UnitTypes.Elfemale:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = SpritesResC.Sprite(SpriteTypes.Elfemale); break;
                                    case LevelTypes.Second: throw new Exception();
                                    default: throw new Exception();
                                }
                                break;
                            default:
                                throw new Exception();
                        }

                        if (CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible)
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible)
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
