using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class GetCellsForAttackArcherS : SystemModelGameAbs
    {
        internal GetCellsForAttackArcherS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            if (e.UnitTC(cell_0).HaveUnit)
            {
                if (!e.UnitEffectStunC(cell_0).IsStunned)
                {
                    if (e.UnitStepC(cell_0).HaveAnySteps)
                    {
                        if (!e.UnitTC(cell_0).IsMelee(e.UnitMainTWE(cell_0).ToolWeaponTC.ToolWeapon))
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = e.CellEs(cell_0).AroundCellsEs.AroundCellE(dir_1).IdxC.Idx;

                                var isRight_0 = e.UnitIsRightArcherC(cell_0).IsRight;

                                if (e.CellEs(idx_1).IsActiveParentSelf && !e.MountainC(idx_1).HaveAnyResources)
                                {
                                    if (e.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_0).Player))
                                        {
                                            if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        e.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                                    }
                                                    else e.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        e.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                                    }
                                                    else e.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                e.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = e.CellEs(idx_1).AroundCellsEs.AroundCellE(dir_1).IdxC.Idx;


                                    if (e.UnitTC(idx_2).HaveUnit && !e.UnitTC(idx_2).IsAnimal
                                        && e.UnitEs(idx_2).ForPlayer(e.UnitPlayerTC(cell_0).Player).IsVisible
                                        && !e.UnitPlayerTC(idx_2).Is(e.UnitPlayerTC(cell_0).Player))
                                    {
                                        if (e.UnitTC(cell_0).Is(UnitTypes.Pawn) && e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    e.UnitEs(cell_0).UniqueAttack.Add(idx_2);
                                                }

                                                else e.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    e.UnitEs(cell_0).UniqueAttack.Add(idx_2);
                                                }

                                                else e.UnitEs(cell_0).SimpleAttack.Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            e.UnitEs(cell_0).SimpleAttack.Add(idx_2);
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