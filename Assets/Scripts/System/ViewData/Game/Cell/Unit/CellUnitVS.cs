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
                ref var unit_0 = ref Unit(idx_0);
                ref var levelUnit_0 = ref CellUnitElseEs.Level(idx_0);

                ref var corner_0 = ref CellUnitElseEs.Corned(idx_0);

                ref var tw_0 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref CellUnitTWE.UnitTW<LevelTC>(idx_0);

                ref var mainUnit_0 = ref UnitCellVEs.UnitMain<SpriteRendererVC>(idx_0);
                ref var extraUnit_0 = ref UnitCellVEs.UnitExtra<SpriteRendererVC>(idx_0);


                mainUnit_0.Disable();
                extraUnit_0.Disable();

                if (unit_0.Have)
                {
                    if (CellUnitVisibleEs.Visible(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                    {
                        mainUnit_0.Enable();

                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None:
                                throw new Exception();

                            case UnitTypes.King:
                                mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.King).Sprite;
                                break;

                            case UnitTypes.Pawn:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.PawnWood).Sprite; break;
                                    case LevelTypes.Second: mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.PawnIron).Sprite; break;
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
                                                case LevelTypes.Second: extraUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.PickWood).Sprite; break;
                                                default: throw new Exception();
                                            }
                                            break;
                                        case ToolWeaponTypes.Sword:
                                            switch (twLevel_0.Level)
                                            {
                                                case LevelTypes.None: throw new Exception();
                                                case LevelTypes.First: throw new Exception();
                                                case LevelTypes.Second: extraUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.SwordIron).Sprite; break;
                                                default: throw new Exception();
                                            }
                                            break;
                                        case ToolWeaponTypes.Shield:
                                            switch (twLevel_0.Level)
                                            {
                                                case LevelTypes.None: throw new Exception();
                                                case LevelTypes.First: extraUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.ShieldWood).Sprite; break;
                                                case LevelTypes.Second: extraUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.ShieldIron).Sprite; break;
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
                                            if (corner_0.IsCornered) mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.RookBow).Sprite;
                                            else mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.BishopBow).Sprite;
                                        }
                                        break;
                                    case LevelTypes.Second:
                                        {
                                            if (corner_0.IsCornered) mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.RookCrossbow).Sprite;
                                            else mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(SpriteTypes.BishopCrossbow).Sprite;
                                        }
                                        break;
                                    default: throw new Exception();
                                }
                                break;
                            case UnitTypes.Scout:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(UnitTypes.Scout).Sprite; break;
                                    case LevelTypes.Second: throw new Exception();
                                    default: throw new Exception();
                                }
                                break;
                            case UnitTypes.Elfemale:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(UnitTypes.Elfemale).Sprite; break;
                                    case LevelTypes.Second: throw new Exception();
                                    default: throw new Exception();
                                }
                                break;
                            case UnitTypes.Snowy:
                                switch (levelUnit_0.Level)
                                {
                                    case LevelTypes.None: throw new Exception();
                                    case LevelTypes.First: mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(UnitTypes.Snowy).Sprite; break;
                                    case LevelTypes.Second: throw new Exception();
                                    default: throw new Exception();
                                }
                                break;
                            case UnitTypes.Camel:
                                mainUnit_0.Sprite = ResourcesSpriteVEs.Sprite(UnitTypes.Camel).Sprite; break;
                                throw new Exception();
                        }

                        if (CellUnitVisibleEs.Visible(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible)
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 1);
                        }
                        else
                        {
                            mainUnit_0.Color = new Color(mainUnit_0.Color.r, mainUnit_0.Color.g, mainUnit_0.Color.b, 0.6f);
                        }

                        if (CellUnitVisibleEs.Visible(WhoseMoveE.NextPlayerFrom(WhoseMoveE.CurPlayerI), idx_0).IsVisible)
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
