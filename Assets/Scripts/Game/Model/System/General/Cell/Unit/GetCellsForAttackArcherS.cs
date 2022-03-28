using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetCellsForAttackArcherS : SystemModelGameAbs
    {
        internal GetCellsForAttackArcherS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            if (eMGame.UnitTC(cell_0).HaveUnit)
            {
                if (!eMGame.UnitEffectStunC(cell_0).IsStunned)
                {
                    if (eMGame.UnitStepC(cell_0).HaveAnySteps)
                    {
                        if (!eMGame.UnitTC(cell_0).IsMelee(eMGame.UnitMainTWE(cell_0).ToolWeaponTC.ToolWeapon))
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = eMGame.CellEs(cell_0).AroundCellsEs.AroundCellE(dir_1).IdxC.Idx;

                                var isRight_0 = eMGame.UnitIsRightArcherC(cell_0).IsRight;

                                if (eMGame.CellEs(idx_1).IsActiveParentSelf && !eMGame.MountainC(idx_1).HaveAnyResources)
                                {
                                    if (eMGame.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!eMGame.UnitPlayerTC(idx_1).Is(eMGame.UnitPlayerTC(cell_0).Player))
                                        {
                                            if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        eMGame.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                                    }
                                                    else eMGame.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        eMGame.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                                    }
                                                    else eMGame.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                eMGame.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = eMGame.CellEs(idx_1).AroundCellsEs.AroundCellE(dir_1).IdxC.Idx;


                                    if (eMGame.UnitTC(idx_2).HaveUnit && !eMGame.UnitTC(idx_2).IsAnimal
                                        && eMGame.UnitEs(idx_2).ForPlayer(eMGame.UnitPlayerTC(cell_0).Player).IsVisible
                                        && !eMGame.UnitPlayerTC(idx_2).Is(eMGame.UnitPlayerTC(cell_0).Player))
                                    {
                                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn) && eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    eMGame.UnitEs(cell_0).UniqueAttack.Add(idx_2);
                                                }

                                                else eMGame.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    eMGame.UnitEs(cell_0).UniqueAttack.Add(idx_2);
                                                }

                                                else eMGame.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            eMGame.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}